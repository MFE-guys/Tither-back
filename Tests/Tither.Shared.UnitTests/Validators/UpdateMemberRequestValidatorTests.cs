using FluentValidation.TestHelper;
using Tither.Shared.Requests;
using Xunit;

namespace Tither.Shared.Validators
{
    public class UpdateMemberRequestValidatorTests
    {
        private readonly UpdateMemberRequestValidator _validator;

        public UpdateMemberRequestValidatorTests()
            => _validator = new UpdateMemberRequestValidator();

        [Fact]
        public void Validate_WhenIdIsEmpty_InvalidatesId()
        {
            // ARRANGE
            var request = new UpdateMemberRequest(string.Empty, "name", "Tither", "Jon@gmail.com", "877743843", true, "test");

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Id must not be empty");
        }

        [Fact]
        public void Validate_WhenIdIsNull_InvalidatesId()
        {
            // ARRANGE
            var request = new UpdateMemberRequest(null, "name", "Tither", "Jon@gmail.com", "877743843", true, "test");


            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Id must be valid");
        }
    }
}
