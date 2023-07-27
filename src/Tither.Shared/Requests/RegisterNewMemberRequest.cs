using MediatR;
using OperationResult;
using Tither.Shared.Validators;
using Tither.Shared.ViewModels;

namespace Tither.Shared.Requests
{
    public class RegisterNewMemberRequest : MemberRequestBase, IRequest<Result<RegisterNewMemberViewModel>>, IValidatable
    {
        public RegisterNewMemberRequest(string name, string memberType, string email, string phone, bool status, string historic)
        : base(name, memberType, email, phone, status, historic) { }
    }
}
