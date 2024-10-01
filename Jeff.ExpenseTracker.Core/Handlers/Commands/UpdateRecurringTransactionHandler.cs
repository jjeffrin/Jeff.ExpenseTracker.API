using AutoMapper;
using FluentValidation;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Commands
{
    public class UpdateRecurringTransaction : IRequest<RecurringTransactionDTO>
    {
        public UpdateRecurringTransaction(UpdateRecurringTransactionDTO model)
        {
            Model = model;
        }

        public UpdateRecurringTransactionDTO Model { get; }
    }

    public class UpdateRecurringTransactionHandler : IRequestHandler<UpdateRecurringTransaction, RecurringTransactionDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<UpdateRecurringTransactionDTO> validator;
        private readonly IMapper mapper;

        public UpdateRecurringTransactionHandler(IUnitOfWork unitOfWork, IValidator<UpdateRecurringTransactionDTO> validator, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<RecurringTransactionDTO> Handle(UpdateRecurringTransaction request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var repository = unitOfWork.RecurringTransactionRepository;
            var validationContext = new ValidationContext<UpdateRecurringTransactionDTO>(model);
            var result = validator.Validate(validationContext);
            if (!result.IsValid)
            {
                throw new InvalidRequestException()
                {
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToArray()
                };
            }
            var entity = await repository.GetById(model.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException()
                {
                    Error = $"RecurringTransaction entity with {model.Id} does not exist"
                };
            }
            entity.Cost = model.Cost;
            entity.Content = model.Content;
            entity.Type = model.Type;
            entity.Frequency = model.Frequency;
            entity.EffectiveFrom = model.EffectiveFrom;
            entity.EffectiveTo = model.EffectiveTo;
            repository.UpdateBaseEntity(entity, model.EmailId!);
            await unitOfWork.CommitChangesAsync();
            return this.mapper.Map<RecurringTransactionDTO>(entity);
        }
    }
}
