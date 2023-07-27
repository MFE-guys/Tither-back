using AutoMapper;
using FluentAssertions;
using Moq;
using OperationResult;
using Tither.Domain.Models;
using Tither.Domain.Repositories;
using Tither.Shared.Requests;
using Xunit;

namespace Tither.Domain.RequestHandlers
{
    public sealed class UpdateMemberRequestHandlerTests
    {
        private readonly Mock<IMemberRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public UpdateMemberRequestHandlerTests()
        {
            _repositoryMock = new Mock<IMemberRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_WithSuccess_ReturnsSuccessResult()
        {
            // ARRANGE
            var request = new UpdateMemberRequest("1","name", "Tither", "Jon@gmail.com", "877743843", true, "test");

            var cancellationToken = new CancellationToken();
            var member = new Member(); // Replace with actual member data

            _mapperMock.Setup(mapper => mapper.Map<Member>(request))
                .Returns(member);
            _repositoryMock.Setup(repo => repo.UpdateMember(request.Id, member))
                .ReturnsAsync(Result.Success());

            var handler = new UpdateMemberRequestHandler(
                _repositoryMock.Object,
                _mapperMock.Object
            );

            // ACT
            var result = await handler.Handle(request, cancellationToken);

            // ASSERT
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_WithError_ReturnsErrorResultWithException()
        {
            // ARRANGE
            var request = new UpdateMemberRequest(null, null, null,null, null, true, null);

            var member = new Member();
            var exception = new Exception("Some error message");

            _mapperMock.Setup(mapper => mapper.Map<Member>(request))
                .Returns(member);
            _repositoryMock.Setup(repo => repo.UpdateMember(request.Id, member))
                .ReturnsAsync(Result.Error(exception));

            var handler = new UpdateMemberRequestHandler(
                _repositoryMock.Object,
                _mapperMock.Object
            );

            // ACT
            var result = await handler.Handle(request, new CancellationToken());

            // ASSERT
            result.IsSuccess.Should().BeFalse();
            result.Exception.Should().Be(exception);
        }
    }
}
