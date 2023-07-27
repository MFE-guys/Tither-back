using FluentValidation.TestHelper;
using Tither.Shared.Requests;
using Xunit;

namespace Tither.Shared.Validators
{
    public sealed class RegisterNewMemberRequestValidatorTests
    {
        private readonly RegisterNewMemberRequestValidator _validator;

        public RegisterNewMemberRequestValidatorTests() 
            => _validator = new RegisterNewMemberRequestValidator();

        [Fact]
        public void Validate_WhenNameIsEmpty_InvalidatesName()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest(string.Empty, "Tither", "jon@gmail.com", "8699879987", true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name must not be empty");
        }

        [Fact]
        public void Validate_WhenNameIsNull_InvalidatesName()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest(null, "Tither", "jon@gmail.com", "8699879987", true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name must be valid");
        }

        [Fact]
        public void Validate_WhenPhoneIsEmpty_InvalidatesPhone()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon", "Tither", "jon@gmail.com", string.Empty, true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.Phone)
                .WithErrorMessage("Phone must not be empty");
        }

        [Fact]
        public void Validate_WhenPhoneIsNull_InvalidatesPhone()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon", "Tither", "jon@gmail.com", null, true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.Phone)
                .WithErrorMessage("Phone must be valid");
        }

        [Fact]
        public void Validate_WhenMemberTypeIsEmpty_InvalidatesMemberType()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon", string.Empty, "jon@gmail.com", "436634535", true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.MemberType)
                .WithErrorMessage("Member Type must not be empty");
        }

        [Fact]
        public void Validate_WhenMemberTypeIsNull_InvalidatesMemberType()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon", null, "jon@gmail.com", "436634535", true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.MemberType)
                .WithErrorMessage("Member Type must be valid");
        }
    }
}
