using FluentValidation.TestHelper;
using Tither.Shared.Requests;
using Xunit;

namespace Tither.Shared.Validators
{
    public class MemberRequestBaseValidatorTests
    {
        private readonly MemberRequestBaseValidator _validator;

        public MemberRequestBaseValidatorTests() 
            => _validator = new MemberRequestBaseValidator();

        [Fact]
        public void Validate_WhenEmailIsProvided_ValidatesEmail()
        {
            // ARRANGE
            var request = new TestMemberRequest("Jon", "Tither", "invalid-email", "87886634", true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Please provide a valid Email address. Example: example@example.com.");
        }

        [Fact]
        public void Validate_WhenEmailIsNotProvided_DoesNotValidateEmail()
        {
            // ARRANGE
            var request = new TestMemberRequest("Jon", "Tither", null, "87886634", true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Validate_WhenStatusIsBoolean_DoesNotInvalidateStatus(bool status)
        {
            // ARRANGE
            var request = new TestMemberRequest("Jon", "Tither", "jon@gmail.com", "87886634", status, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }
    }

    public class TestMemberRequest : MemberRequestBase
    {
        public TestMemberRequest(string name, string memberType, string email, string phone, bool status, string historic) 
            : base(name, memberType, email, phone, status, historic)
        { }
    }
}
