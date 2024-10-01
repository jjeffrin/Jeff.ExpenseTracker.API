using FluentValidation;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Contracts.Data.Entities;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Commands
{
    public class CreateTransaction : IRequest<int>
    {
        public CreateTransaction(CreateTransactionDTO model)
        {
            Model = model;
        }

        public CreateTransactionDTO Model { get; }
    }

    public class CreateTransactionHandler : IRequestHandler<CreateTransaction, int>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<CreateTransactionDTO> validator;

        public CreateTransactionHandler(IUnitOfWork unitOfWork, IValidator<CreateTransactionDTO> validator)
        {
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        public async Task<int> Handle(CreateTransaction request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var result = await this.validator.ValidateAsync(new ValidationContext<CreateTransactionDTO>(model));

            if (!result.IsValid)
            {
                throw new InvalidRequestException()
                {
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToArray()
                };
            }

            var newTransaction = new TransactionEntity() 
            {
                Content = model.Content,
                Cost = model.Cost,
                Type = model.Type,
                TransactionOn = model.TransactionOn,
            };
            
            await this.unitOfWork.TransactionRepository.Add(newTransaction, model.EmailId!);
            await this.unitOfWork.CommitChangesAsync();
            return newTransaction.Id;                       
        }
    }
}
