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
    public sealed class RegisterNewMemberRequestHandlerTests
    {
        private readonly Mock<IMemberRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public RegisterNewMemberRequestHandlerTests()
        {
            _repositoryMock = new Mock<IMemberRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_WithSuccess_ReturnsSuccessResult()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest("Jon","Tither","jon@gmail.com","8699879987", true, "test");

            var member = new Member();
            var viewModel = new RegisterNewMemberViewModel("The member was successfully registed");

            _mapperMock.Setup(mapper => mapper.Map<Member>(request))
                .Returns(member);
            _repositoryMock.Setup(repo => repo.RegisterMember(member))
                .ReturnsAsync(Result.Success());

            var handler = new RegisterNewMemberRequestHandler(
                _repositoryMock.Object,
                _mapperMock.Object
            );

            // ACT
            var result = await handler.Handle(request, new CancellationToken());

            // ASSERT
            result.IsSuccess.Should().BeTrue();
            result.Value?.Message.Should().Be(viewModel.Message);
        }

        [Fact]
        public async Task Handle_WithError_ReturnsErrorResultWithException()
        {
            // ARRANGE
            var request = new RegisterNewMemberRequest(null, null, null, null, true, null);
            var member = new Member(); 
            var exception = new Exception("Some error message");

            _mapperMock.Setup(mapper => mapper.Map<Member>(request))
                .Returns(member);
            _repositoryMock.Setup(repo => repo.RegisterMember(member))
                .ReturnsAsync(Result.Error(exception));

            var handler = new RegisterNewMemberRequestHandler(
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
