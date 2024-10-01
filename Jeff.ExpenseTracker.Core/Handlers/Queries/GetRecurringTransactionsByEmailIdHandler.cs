using AutoMapper;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Queries
{
    public class GetRecurringTransactionsByEmailId : IRequest<IEnumerable<RecurringTransactionDTO>>
    {
        public GetRecurringTransactionsByEmailId(UserSpecificDTO model)
        {
            Model = model;
        }

        public UserSpecificDTO Model { get; }
    }

    public class GetRecurringTransactionsByEmailIdHandler : IRequestHandler<GetRecurringTransactionsByEmailId, IEnumerable<RecurringTransactionDTO>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetRecurringTransactionsByEmailIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RecurringTransactionDTO>> Handle(GetRecurringTransactionsByEmailId request, CancellationToken cancellationToken)
        {
            if (request.Model.EmailId != null)
            {
                var RecurringTransactions = await Task.FromResult(this.unitOfWork.RecurringTransactionRepository.GetByEmailId(request.Model.EmailId));
                return this.mapper.Map<IEnumerable<RecurringTransactionDTO>>(RecurringTransactions);
            }
            else
            {
                throw new InvalidRequestException()
                {
                    Errors = new string[] { "Email ID should be provided" }
                };
            }
        }
    }
}
