using MongoDB.Driver;
using OperationResult;
using Tither.Data.Context;
using Tither.Domain.Models;
using Tither.Domain.Repositories;

namespace Tither.Data.Repositories
{
    public sealed class MemberRepository : IMemberRepository
    {
        private readonly IMongoCollection<Member> _titherCollection;

        public MemberRepository(TitherContext context)
            => _titherCollection = context.GetCollection<Member>("members");

        public Task<Result<IQueryable<Member>>> GetAll()
        {
            try
            {
                var query = _titherCollection.AsQueryable();

                return Result.Success<IQueryable<Member>>(query);
            }
            catch (Exception ex)
            {
                return Result.Error<IQueryable<Member>>(ex);
            }
        }

        public async Task<Result> RegisterMember(Member member)
        {
            try
            {
                var filter = Builders<Member>.Filter.Eq(x => x.Email, member.Email);
                var existingTither = await _titherCollection.Find(filter).FirstOrDefaultAsync();

                if (existingTither != null)
                    return Result.Error(new Exception("There is already a decimist registered with this e-mail"));

                member.IncludeDate = DateTime.UtcNow;
                member.LastModified = DateTime.UtcNow;

                await _titherCollection.InsertOneAsync(member).ConfigureAwait(false);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        public async Task<Result> UpdateMember(string id, Member member)
        {
            try
            {
                var filter = Builders<Member>.Filter.Eq("_id", member.Id);
                var updateDefinitionBuilder = Builders<Member>.Update;

                member.LastModified = DateTime.UtcNow;

                var updateDefinition = updateDefinitionBuilder.Combine(GetUpdateDefinitions(filter, member));

                await _titherCollection.UpdateOneAsync(filter, updateDefinition);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        private List<UpdateDefinition<Member>> GetUpdateDefinitions(FilterDefinition<Member> filter, Member member)
        {
            var updateDefinitions = new List<UpdateDefinition<Member>>();
            var updateDefinitionBuilder = Builders<Member>.Update;

            var currentStatus = _titherCollection.Find(filter).Project(x => x.Status).FirstOrDefault();

            if (!string.IsNullOrEmpty(member.Name))
                updateDefinitions.Add(updateDefinitionBuilder.Set(x => x.Name, member.Name));

            if (!string.IsNullOrEmpty(member.Email))
                updateDefinitions.Add(updateDefinitionBuilder.Set(x => x.Email, member.Email));

            if (!string.IsNullOrEmpty(member.MemberType))
                updateDefinitions.Add(updateDefinitionBuilder.Set(x => x.MemberType, member.MemberType));

            if (!string.IsNullOrEmpty(member.Phone))
                updateDefinitions.Add(updateDefinitionBuilder.Set(x => x.Phone, member.Phone));

            if (!string.IsNullOrEmpty(member.Historic))
                updateDefinitions.Add(updateDefinitionBuilder.Set(x => x.Historic, member.Historic));

            updateDefinitions.Add(updateDefinitionBuilder.Set(x => x.Status, member.Status));
            updateDefinitions.Add(updateDefinitionBuilder.Set(x => x.LastModified, member.LastModified));

            return updateDefinitions;
        }
    }
}
