using FluentValidation;
using Tither.Shared.Requests;

namespace Tither.Shared.Validators
{
    public class UpdateMemberRequestValidator : AbstractValidator<UpdateMemberRequest> 
    {
        public UpdateMemberRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} must not be empty")
                .NotNull().WithMessage("{PropertyName} must be valid");

            Include(new MemberRequestBaseValidator());
        }
    }
}
