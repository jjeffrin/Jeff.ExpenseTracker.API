using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Contracts.Enums;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Queries
{
    public class GetAllFrequencyTypes : IRequest<IEnumerable<GetAllFrequencyTypesDTO>>
    {

    }

    public class GetAllFrequencyTypesHandler : IRequestHandler<GetAllFrequencyTypes, IEnumerable<GetAllFrequencyTypesDTO>>
    {
        public async Task<IEnumerable<GetAllFrequencyTypesDTO>> Handle(GetAllFrequencyTypes request, CancellationToken cancellationToken)
        {
            var result = new List<GetAllFrequencyTypesDTO>();
            var typesArray = await Task.FromResult(Enum.GetValues(typeof(FrequencyType)));
            for (int index = 0; index < typesArray.Length; index++)
            {
                result.Add(new GetAllFrequencyTypesDTO() { SeqNo = index, Type = typesArray.GetValue(index)!.ToString()! });
            }
            return result;
        }
    }
}
