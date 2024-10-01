using AutoMapper;
using FluentValidation;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Commands
{
    public class UpdateTransaction : IRequest<TransactionDTO>
    {
        public UpdateTransaction(UpdateTransactionDTO model)
        {
            Model = model;
        }

        public UpdateTransactionDTO Model { get; }
    }

    public class UpdateTransactionHandler : IRequestHandler<UpdateTransaction, TransactionDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<UpdateTransactionDTO> validator;
        private readonly IMapper mapper;

        public UpdateTransactionHandler(IUnitOfWork unitOfWork, IValidator<UpdateTransactionDTO> validator, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<TransactionDTO> Handle(UpdateTransaction request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var repository = unitOfWork.TransactionRepository;
            var validationContext = new ValidationContext<UpdateTransactionDTO>(model);
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
                    Error = $"Transaction entity with {model.Id} does not exist"
                };
            }
            entity.Cost = model.Cost;
            entity.Content = model.Content;
            entity.Type = model.Type;
            entity.TransactionOn = model.TransactionOn;
            repository.UpdateBaseEntity(entity, model.EmailId!);
            await unitOfWork.CommitChangesAsync();
            return this.mapper.Map<TransactionDTO>(entity);
        }
    }
}
