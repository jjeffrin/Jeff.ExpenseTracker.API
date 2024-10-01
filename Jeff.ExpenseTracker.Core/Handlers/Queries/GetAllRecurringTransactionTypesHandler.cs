using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Contracts.Enums;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Queries
{
    public class GetAllRecurringTransactionTypes : IRequest<IEnumerable<GetAllRecurringTransactionTypesDTO>>
    {
    }

    public class GetAllRecurringTransactionTypesHandler : IRequestHandler<GetAllRecurringTransactionTypes, IEnumerable<GetAllRecurringTransactionTypesDTO>>
    {
        public async Task<IEnumerable<GetAllRecurringTransactionTypesDTO>> Handle(GetAllRecurringTransactionTypes request, CancellationToken cancellationToken)
        {
            var result = new List<GetAllRecurringTransactionTypesDTO>();
            var typesArray = await Task.FromResult(Enum.GetValues(typeof(RecurringTransactionType)));
            for (int index = 0; index < typesArray.Length; index++)
            {
                result.Add(new GetAllRecurringTransactionTypesDTO() { SeqNo = index, Type = typesArray.GetValue(index)!.ToString()! });
            }
            return result;
        }
    }
}
