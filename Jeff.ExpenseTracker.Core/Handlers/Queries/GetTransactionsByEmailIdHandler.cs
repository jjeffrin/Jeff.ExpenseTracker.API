using AutoMapper;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Queries
{
    public class GetTransactionsByEmailId : IRequest<IEnumerable<TransactionDTO>>
    {
        public GetTransactionsByEmailId(UserSpecificDTO model)
        {
            Model = model;
        }

        public UserSpecificDTO Model { get; }
    }

    public class GetTransactionsByEmailIdHandler : IRequestHandler<GetTransactionsByEmailId, IEnumerable<TransactionDTO>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTransactionsByEmailIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDTO>> Handle(GetTransactionsByEmailId request, CancellationToken cancellationToken)
        {
            if (request.Model.EmailId != null)
            {
                var transactions = await Task.FromResult(this.unitOfWork.TransactionRepository.GetByEmailId(request.Model.EmailId));
                return this.mapper.Map<IEnumerable<TransactionDTO>>(transactions);
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
