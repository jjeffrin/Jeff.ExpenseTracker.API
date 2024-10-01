using MediatR;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Contracts.Enums;

namespace Jeff.ExpenseTracker.Core.Handlers.Queries
{
    public class GetAllTransactionTypes : IRequest<IEnumerable<GetAllTransactionTypesDTO>>
    {
    }

    public class GetAllTransactionTypesHandler : IRequestHandler<GetAllTransactionTypes, IEnumerable<GetAllTransactionTypesDTO>>
    {
        public async Task<IEnumerable<GetAllTransactionTypesDTO>> Handle(GetAllTransactionTypes request, CancellationToken cancellationToken)
        {
            var result = new List<GetAllTransactionTypesDTO>();
            var typesArray = await Task.FromResult(Enum.GetValues(typeof(TransactionType)));
            for (int index = 0; index < typesArray.Length; index++)
            {
                result.Add(new GetAllTransactionTypesDTO() { SeqNo = index, Type = typesArray.GetValue(index)!.ToString()! });
            }
            return result;
        }
    }
}
