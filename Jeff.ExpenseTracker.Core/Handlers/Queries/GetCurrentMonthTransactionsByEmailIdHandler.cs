using AutoMapper;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeff.ExpenseTracker.Core.Handlers.Queries
{
    public class GetCurrentMonthTransactionsByEmailId : IRequest<IEnumerable<TransactionDTO>>
    {
        public GetCurrentMonthTransactionsByEmailId(TimeSpecificDTO model)
        {
            Model = model;
        }

        public TimeSpecificDTO Model { get; }
    }

    public class GetCurrentMonthTransactionsByEmailIdHandler : IRequestHandler<GetCurrentMonthTransactionsByEmailId, IEnumerable<TransactionDTO>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCurrentMonthTransactionsByEmailIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDTO>> Handle(GetCurrentMonthTransactionsByEmailId request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            if (model.UpdatedOn == DateTime.MinValue) throw new InvalidRequestException()
            {
                Errors = ["Please provide a valid month."]
            };
            var query = this.unitOfWork.TransactionRepository.GetQueryable();
            var records = await Task.FromResult(query.Where(x => x.UpdatedBy == model.EmailId && x.TransactionOn.Month == model.UpdatedOn.Month && x.TransactionOn.Year == model.UpdatedOn.Year).OrderByDescending(x => x.TransactionOn).ToList());
            return mapper.Map<IEnumerable<TransactionDTO>>(records);
        }
    }
}
