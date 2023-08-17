using AutoMapper;
using MediatR;
using OperationResult;
using Tither.Domain.Repositories;
using Tither.Shared.Requests;
using Tither.Shared.ViewModels;

namespace Tither.Domain.RequestHandlers
{
    public sealed class RegisterNewMemberRequestHandler : IRequestHandler<RegisterNewMemberRequest, Result<RegisterNewMemberViewModel>>
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public RegisterNewMemberRequestHandler(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<RegisterNewMemberViewModel>> Handle(RegisterNewMemberRequest request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Models.Member>(request);

            var (IsSuccess, exception) = await _repository.RegisterMember(member).ConfigureAwait(false);

            return IsSuccess ?
                Result.Success(new RegisterNewMemberViewModel("The member was successfully registed")) :
                Result.Error<RegisterNewMemberViewModel>(exception!);
        }
    }
}
