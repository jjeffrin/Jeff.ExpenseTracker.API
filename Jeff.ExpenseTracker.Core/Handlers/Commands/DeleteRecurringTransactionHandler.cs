using Jeff.ExpenseTracker.Contracts.DTOs;
using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Core.Exceptions;
using MediatR;

namespace Jeff.ExpenseTracker.Core.Handlers.Commands
{
    public class DeleteRecurringTransaction : IRequest<int>
    {
        public DeleteRecurringTransaction(DeleteEntityDTO model)
        {
            Model = model;
        }

        public DeleteEntityDTO Model { get; }
    }

    public class DeleteRecurringTransactionHandler : IRequestHandler<DeleteRecurringTransaction, int>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteRecurringTransactionHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteRecurringTransaction request, CancellationToken cancellationToken)
        {
            var entity = await this.unitOfWork.RecurringTransactionRepository.GetById(request.Model.Id);
            if (entity == null) throw new EntityNotFoundException()
            {
                Error = $"RecurringTransaction entity with ID {request.Model.Id} is not found"
            };

            var id = this.unitOfWork.RecurringTransactionRepository.Delete(entity);
            await this.unitOfWork.CommitChangesAsync();
            return id;
        }
    }
}
