using FluentValidation;
using Jeff.ExpenseTracker.Contracts.DTOs;

namespace Jeff.ExpenseTracker.Core.Validators
{
    public class UpdateTransactionDTOValidator : AbstractValidator<UpdateTransactionDTO>
    {
        public UpdateTransactionDTOValidator()
        {
            var message = "Please provide a valid";
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage($"{message} {nameof(UpdateTransactionDTO.Id)}");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage($"{message} {nameof(UpdateTransactionDTO.Content)}");
            RuleFor(x => x.Cost).NotNull().NotEmpty().WithMessage($"{message} {nameof(UpdateTransactionDTO.Cost)}");
            RuleFor(x => x.Type).IsInEnum().WithMessage($"{message} {nameof(UpdateTransactionDTO.Type)}");
            RuleFor(x => x.EmailId).NotNull().WithMessage($"{message} {nameof(UpdateTransactionDTO.EmailId)}");
            RuleFor(x => x.TransactionOn).NotNull().NotEqual(x => DateTime.MinValue).WithMessage($"{message} {nameof(UpdateTransactionDTO.TransactionOn)}");
        }
    }
}
