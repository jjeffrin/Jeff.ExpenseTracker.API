using FluentValidation;
using Jeff.ExpenseTracker.Contracts.DTOs;

namespace Jeff.ExpenseTracker.Core.Validators
{
    public class UpdateRecurringTransactionDTOValidator : AbstractValidator<UpdateRecurringTransactionDTO>
    {
        public UpdateRecurringTransactionDTOValidator()
        {
            var message = "Please provide a valid";
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.Id)}");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.Content)}");
            RuleFor(x => x.Cost).NotNull().NotEmpty().WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.Cost)}");
            RuleFor(x => x.Type).IsInEnum().WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.Type)}");
            RuleFor(x => x.Frequency).IsInEnum().WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.Frequency)}");
            RuleFor(x => x.EmailId).NotNull().WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.EmailId)}");
            RuleFor(x => x.EffectiveFrom).NotNull().NotEqual(x => DateTime.MinValue).WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.EffectiveFrom)}");
            RuleFor(x => x.EffectiveTo).NotNull().NotEqual(x => DateTime.MinValue).WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.EffectiveTo)}");
            RuleFor(x => x.EffectiveFrom).LessThanOrEqualTo(x => x.EffectiveTo).WithMessage($"{message} {nameof(UpdateRecurringTransactionDTO.EffectiveTo)}");
        }
    }
}
