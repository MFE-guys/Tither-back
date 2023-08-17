using AutoMapper;
using FluentAssertions;
using Moq;
using OperationResult;
using Tither.Domain.Models;
using Tither.Domain.Repositories;
using Tither.Shared.Requests;
using Tither.Shared.ViewModels;
using Xunit;

namespace Tither.Domain.RequestHandlers
{
    public sealed class GetAllMembersRequestHandlerTests
    {
        private readonly Mock<IMemberRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetAllMembersRequestHandlerTests()
        {
            _repositoryMock = new Mock<IMemberRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_WithSuccess_ReturnsSuccessResultWithMappedData()
        {
            // ARRANGE
            var request = new GetAllMembersRequest();
            var members = new List<Member>();
            var memberViewModels = new List<MemberViewModel>();

            _repositoryMock.Setup(repo => repo.GetAll())
                .ReturnsAsync(Result.Success(members.AsQueryable()));
            _mapperMock.Setup(mapper => mapper.ProjectTo<MemberViewModel>(It.IsAny<IQueryable<Member>>(), null))
                .Returns(memberViewModels.AsQueryable());

            var handler = new GetAllMembersRequestHandler(
                _repositoryMock.Object,
                _mapperMock.Object
            );

            // ACT
            var result = await handler.Handle(request, new CancellationToken());

            // ASSERT
            result.IsSuccess.Should().BeTrue(); 
        }

        [Fact]
        public async Task Handle_WithError_ReturnsErrorResultWithException()
        {
            // ARRANGE
            var request = new GetAllMembersRequest();
            var exception = new Exception("Some error message");

            _repositoryMock.Setup(repo => repo.GetAll())
                .ReturnsAsync(Result.Error<IQueryable<Member>>(exception));

            var handler = new GetAllMembersRequestHandler(
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
