using FluentValidation;
using Tither.Shared.Requests;

namespace Tither.Shared.Validators
{
    public class MemberRequestBaseValidator : AbstractValidator<MemberRequestBase> 
    {
        public MemberRequestBaseValidator() 
        {
            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email)
                    .Must(ValidatorBase.ValidateEmail).WithMessage("Please provide a valid {PropertyName} address. Example: example@example.com");
            });

            RuleFor(x => x.Status)
                .NotNull().WithMessage("{PropertyName} value must be provided.")
                .Must(x => x == true || x == false).WithMessage("The {PropertyName} value must be boolean.");
        }
    }
}
