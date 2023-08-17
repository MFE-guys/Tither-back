using OperationResult;
using Tither.Domain.Models;

namespace Tither.Domain.Repositories
{
    public interface IMemberRepository
    {
        public Task<Result<IQueryable<Member>>> GetAll();
        public Task<Result> RegisterMember(Member member);
        public Task<Result> UpdateMember(string id, Member member);
    }
}
