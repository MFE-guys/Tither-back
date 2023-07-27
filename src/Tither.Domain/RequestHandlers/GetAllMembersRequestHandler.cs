using AutoMapper;
using MediatR;
using OperationResult;
using Tither.Domain.Repositories;
using Tither.Shared.Requests;
using Tither.Shared.ViewModels;

namespace Tither.Domain.RequestHandlers
{
    public sealed class GetAllMembersRequestHandler : IRequestHandler<GetAllMembersRequest, Result<IQueryable<MemberViewModel>>>
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMembersRequestHandler(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IQueryable<MemberViewModel>>> Handle(GetAllMembersRequest request, CancellationToken cancellationToken)
        {
            var (isSuccess, result, exception) = await _repository.GetAll().ConfigureAwait(false);

            return isSuccess ? 
                Result.Success(_mapper.ProjectTo<MemberViewModel>(result)) : 
                Result.Error<IQueryable<MemberViewModel>>(exception!);
        }
    }
}