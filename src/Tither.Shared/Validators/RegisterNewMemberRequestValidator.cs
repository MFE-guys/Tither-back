using FluentValidation;
using Tither.Shared.Requests;

namespace Tither.Shared.Validators
{
    public class RegisterNewMemberRequestValidator : AbstractValidator<RegisterNewMemberRequest> 
    {
        public RegisterNewMemberRequestValidator() 
        {
            Include(new MemberRequestBaseValidator());

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} must not be empty")
                .NotNull().WithMessage("{PropertyName} must be valid");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("{PropertyName} must not be empty")
                .NotNull().WithMessage("{PropertyName} must be valid");

            RuleFor(x => x.MemberType)
                .NotEmpty().WithMessage("{PropertyName} must not be empty")
                .NotNull().WithMessage("{PropertyName} must be valid");
        }
    }
}
