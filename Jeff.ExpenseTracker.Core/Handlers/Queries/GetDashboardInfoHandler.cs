using AutoMapper;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Contracts.Data.Entities;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Contracts.Enums;
using MediatR;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Jeff.ExpenseTracker.Core.Handlers.Queries
{
    public class GetDashboardInfo : IRequest<DashboardInfoDTO>
    {
        public GetDashboardInfo(TimeSpecificDTO model)
        {
            Model = model;
        }

        public TimeSpecificDTO Model { get; }
    }

    public class GetDashboardInfoHandler : IRequestHandler<GetDashboardInfo, DashboardInfoDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetDashboardInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<DashboardInfoDTO> Handle(GetDashboardInfo request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var thisMonth = model.UpdatedOn;

            // 
            var recTransactions = await Task.FromResult(this.unitOfWork.RecurringTransactionRepository.GetByEmailId(request.Model.EmailId!));
            var salaryRecords = recTransactions.Where(x => x.Type == RecurringTransactionType.Salary);

            // get current month transactions
            var transactions = this.unitOfWork.TransactionRepository.GetQueryable();
            var currMonthTransactions = transactions.Where(x => x.UpdatedBy == model.EmailId && x.TransactionOn.Month == model.UpdatedOn.Month && x.TransactionOn.Year == model.UpdatedOn.Year).ToList();

            // recent transactions
            var recentTransactions = currMonthTransactions.OrderByDescending(x => x.TransactionOn).Take(5);

            // top transactions
            var topTransactions = currMonthTransactions.OrderByDescending(x => x.Cost).Take(5);

            // monthly salary
            var monthlySalary = GetMonthlySalary(salaryRecords, thisMonth);
            var usableMonthlySalary = GetUsableMonthlySalary(monthlySalary, recTransactions, thisMonth);
            var remainingUsableMonthlySalary = GetRemainingUsableMonthlySalary(usableMonthlySalary, currMonthTransactions, thisMonth);

            return new DashboardInfoDTO()
            {
                MonthlySalary = decimal.Round(monthlySalary, 2, MidpointRounding.AwayFromZero),
                UsableMonthlySalary = decimal.Round(usableMonthlySalary, 2, MidpointRounding.AwayFromZero),
                RemainingUsableMonthlySalary = decimal.Round(remainingUsableMonthlySalary, 2, MidpointRounding.AwayFromZero),
                RecentTransactions = this.mapper.Map<IEnumerable<TransactionDTO>>(recentTransactions),
                TopTransactions = this.mapper.Map<IEnumerable<TransactionDTO>>(topTransactions),
                TopTransactionCategories = GetTopTransactionCategories(currMonthTransactions)
            };
        }

        private IEnumerable<TopTransactionCategoryDTO> GetTopTransactionCategories(IEnumerable<TransactionEntity> currMonthTransactions)
        {
            var groupedTransactions = currMonthTransactions.GroupBy(x => x.Type);
            var topTransCategories = new List<TopTransactionCategoryDTO>();
            foreach (var group in groupedTransactions)
            {
                var totalCost = group.Sum(x => x.Cost);
                topTransCategories.Add(new TopTransactionCategoryDTO() 
                {
                    CategoryName = group.Key.ToString(),
                    TotalCost = totalCost
                });
            }

            return topTransCategories.OrderByDescending(x => x.TotalCost).Take(5);
        }

        private decimal GetRemainingUsableMonthlySalary(decimal usableMonthlySalary, IEnumerable<TransactionEntity> currMonthTransactions, DateTime currMonth)
        {
            decimal expenses = 0;
            foreach (var transaction in currMonthTransactions)
            {
                expenses += transaction.Cost;
            }
            return usableMonthlySalary - expenses;
        }

        private decimal GetUsableMonthlySalary(decimal monthlySalary, IEnumerable<RecurringTransactionEntity> recurringTransactions, DateTime currMonth)
        {
            decimal recurringExpenses = 0;
            foreach (var salary in recurringTransactions.Where(x => x.Type != RecurringTransactionType.Salary))
            {
                recurringExpenses += GetMonthlyCostFromFrequency(salary.Cost, salary.Frequency, currMonth);
            }
            return monthlySalary - recurringExpenses;
        }

        private decimal GetMonthlySalary(IEnumerable<RecurringTransactionEntity> salaries, DateTime currMonth)
        {
            decimal total = 0;
            foreach (var salary in salaries.Where(x => x.EffectiveFrom <= currMonth && x.EffectiveTo >= currMonth))
            {
                total += GetMonthlyCostFromFrequency(salary.Cost, salary.Frequency, currMonth);
            }
            return total;
        }

        private decimal GetMonthlyCostFromFrequency(decimal cost, FrequencyType frequency, DateTime currMonth)
        {
            decimal amount = 0;

            switch (frequency)
            {
                case FrequencyType.Daily:
                    amount = cost * DateTime.DaysInMonth(currMonth.Year, currMonth.Month);
                    break;
                case FrequencyType.Monthly:
                    amount = cost;
                    break;
                case FrequencyType.Quarterly:
                    amount = cost / 3;
                    break;
                case FrequencyType.HalfYearly:
                    amount = cost / 6;
                    break;
                case FrequencyType.Yearly:
                    amount = cost / 12;
                    break;
            }

            return amount;
        }
    }
}
