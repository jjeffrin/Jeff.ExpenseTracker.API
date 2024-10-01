using AutoMapper;
using Jeff.ExpenseTracker.Contracts.Data.Entities;
using Jeff.ExpenseTracker.Contracts.DTOs;

namespace Jeff.ExpenseTracker.Core.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionEntity, TransactionDTO>();
            CreateMap<RecurringTransactionEntity, RecurringTransactionDTO>();
        }
    }
}
