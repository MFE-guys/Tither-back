using FluentAssertions;
using OperationResult;
using Tither.Shared.Requests;
using Tither.Shared.Validators;
using Tither.Shared.ViewModels;
using Xunit;

namespace Tither.Domain.Pipelines
{
    public sealed class FailFastRequestBehaviorTests
    {
        [Fact]
        public async Task Send_valid_Request_should_be_success()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon", "Tither", "jon@gmail.com", "987655", true, "test");
            var validator = new RegisterNewMemberRequestValidator();
            var failFast = new FailFastRequestBehavior<RegisterNewMemberRequest, Result<RegisterNewMemberViewModel>>(new[] { validator });

            Task<Result<RegisterNewMemberViewModel>> handle() => Result.Success(new RegisterNewMemberViewModel("The member was successfully registed")).AsTask;

            // ACT
            var result = await failFast.Handle(request, default, handle).ConfigureAwait(false);

            // ASSERT
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Send_invalid_Request_should_be_false()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon", "Tither", "jon", "987655", true, "test");
            var validator = new RegisterNewMemberRequestValidator();
            var failFast = new FailFastRequestBehavior<RegisterNewMemberRequest, Result<RegisterNewMemberViewModel>>(new[] { validator });

            // ACT
            var result = await failFast.Handle(request, default, null).ConfigureAwait(false);

            // ASSERT
            result.IsSuccess.Should().BeFalse();
        }
    }
}
