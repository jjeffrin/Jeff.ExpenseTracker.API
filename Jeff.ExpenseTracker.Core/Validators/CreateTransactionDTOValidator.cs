using FluentValidation;
using Jeff.ExpenseTracker.Contracts.DTOs;

namespace Jeff.ExpenseTracker.Core.Validators
{
    public class CreateTransactionDTOValidator : AbstractValidator<CreateTransactionDTO>
    {
        public CreateTransactionDTOValidator()
        {
            var message = "Please provide a valid";
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage($"{message} {nameof(CreateTransactionDTO.Content)}");
            RuleFor(x => x.Cost).NotNull().NotEmpty().WithMessage($"{message} {nameof(CreateTransactionDTO.Cost)}");
            RuleFor(x => x.Type).IsInEnum().WithMessage($"{message} {nameof(CreateTransactionDTO.Type)}");
            RuleFor(x => x.EmailId).NotNull().WithMessage($"{message} {nameof(CreateTransactionDTO.EmailId)}");
            RuleFor(x => x.TransactionOn).NotNull().NotEqual(x => DateTime.MinValue).WithMessage($"{message} {nameof(CreateTransactionDTO.TransactionOn)}");
        }
    }
}
