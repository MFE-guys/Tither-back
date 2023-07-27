using FluentAssertions;
using NSubstitute;
using OperationResult;
using Serilog;
using Tither.Shared.Requests;
using Tither.Shared.ViewModels;
using Xunit;

namespace Tither.Domain.Pipelines
{
    public sealed class ExceptionPipelineBehaviorTests
    {
        private readonly ILogger _logger;
        private readonly ExceptionPipelineBehavior<RegisterNewMemberRequest, Result<RegisterNewMemberViewModel>> _sut;

        public ExceptionPipelineBehaviorTests()
        {
            _logger = Substitute.For<ILogger>();
            _sut = new ExceptionPipelineBehavior<RegisterNewMemberRequest, Result<RegisterNewMemberViewModel>>(_logger);
        }

        [Fact]
        public async Task ExceptionPipelineBehavior_WhenHandleIsSuccessful_ShouldBeTrue()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon", "Tither", "jon@gmail.com", "987655", true, "test");

            Task<Result<RegisterNewMemberViewModel>> handle() => Result.Success(new RegisterNewMemberViewModel("The member was successfully registed")).AsTask;

            // ACT
            var result = await _sut.Handle(request, default, handle).ConfigureAwait(false);

            // ASSERT
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ExceptionPipelineBehavior_WhenHandleThrowsAnException_ShouldBeFalse()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon", "Tither", "jon", "987655", true, "test");

            Task<Result<RegisterNewMemberViewModel>> handle() => Result.Error<RegisterNewMemberViewModel>(new Exception()).AsTask;

            // ACT
            var result = await _sut.Handle(request, default, handle).ConfigureAwait(false);

            // ASSERT
            result.IsSuccess.Should().BeFalse();
        }
    }
}
