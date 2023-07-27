using System;
using AutoMapper;
using MediatR;
using OperationResult;
using Tither.Domain.Models;
using Tither.Domain.Repositories;
using Tither.Shared.Requests;
using Tither.Shared.ViewModels;

namespace Tither.Domain.RequestHandlers
{
    public sealed class UpdateMemberRequestHandler : IRequestHandler<UpdateMemberRequest, Result>
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMemberRequestHandler(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateMemberRequest request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Member>(request);

            var (IsSuccess, exception) = await _repository.UpdateMember(request.Id, member);

            return IsSuccess ?
                Result.Success() :
                Result.Error(exception!);
        }
    }
}
