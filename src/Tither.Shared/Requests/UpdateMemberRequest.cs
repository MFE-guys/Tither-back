using MediatR;
using OperationResult;
using Tither.Shared.Validators;

namespace Tither.Shared.Requests
{
    public class UpdateMemberRequest : MemberRequestBase, IRequest<Result>, IValidatable
    {
        public UpdateMemberRequest(string id, string name, string memberType, string email, string phone, bool status, string historic)
         : base(name, memberType, email, phone, status, historic)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
