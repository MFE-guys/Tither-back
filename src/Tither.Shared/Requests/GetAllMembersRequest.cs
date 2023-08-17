using MediatR;
using OperationResult;
using Tither.Shared.Validators;
using Tither.Shared.ViewModels;

namespace Tither.Shared.Requests
{
    public class GetAllMembersRequest : IRequest<Result<IQueryable<MemberViewModel>>>, IValidatable
    {}
}
