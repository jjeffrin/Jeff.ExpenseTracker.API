using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Commands
{
    public class DeleteTransaction : IRequest<int>
    {
        public DeleteTransaction(DeleteEntityDTO model)
        {
            Model = model;
        }

        public DeleteEntityDTO Model { get; }
    }

    public class DeleteTransactionHandler : IRequestHandler<DeleteTransaction, int>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTransactionHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteTransaction request, CancellationToken cancellationToken)
        {
            var entity = await this.unitOfWork.TransactionRepository.GetById(request.Model.Id);
            if (entity == null) throw new EntityNotFoundException()
            {
                Error = $"Transaction entity with ID {request.Model.Id} is not found"
            };

            var id = this.unitOfWork.TransactionRepository.Delete(entity);
            await this.unitOfWork.CommitChangesAsync();
            return id;
        }
    }
}
