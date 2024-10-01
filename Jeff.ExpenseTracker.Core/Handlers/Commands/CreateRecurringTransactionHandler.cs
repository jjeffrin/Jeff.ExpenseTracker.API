using FluentValidation;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Contracts.Data.Entities;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Commands
{
    public class CreateRecurringTransaction : IRequest<int>
    {
        public CreateRecurringTransaction(CreateRecurringTransactionDTO model)
        {
            Model = model;
        }

        public CreateRecurringTransactionDTO Model { get; }
    }

    public class CreateRecurringTransactionHandler : IRequestHandler<CreateRecurringTransaction, int>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<CreateRecurringTransactionDTO> validator;

        public CreateRecurringTransactionHandler(IUnitOfWork unitOfWork, IValidator<CreateRecurringTransactionDTO> validator)
        {
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
        public async Task<int> Handle(CreateRecurringTransaction request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var result = await this.validator.ValidateAsync(new ValidationContext<CreateRecurringTransactionDTO>(model));

            if (!result.IsValid)
            {
                throw new InvalidRequestException()
                {
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToArray()
                };
            }

            var newRecTransaction = new RecurringTransactionEntity()
            {
                Content = model.Content,
                Cost = model.Cost,
                Type = model.Type,
                Frequency = model.Frequency,
                EffectiveFrom = model.EffectiveFrom,
                EffectiveTo = model.EffectiveTo,
            };

            await this.unitOfWork.RecurringTransactionRepository.Add(newRecTransaction, model.EmailId!);
            await this.unitOfWork.CommitChangesAsync();
            return newRecTransaction.Id;
        }
    }
}
