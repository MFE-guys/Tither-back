using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Tither.Domain.Models
{
    public class Member
    {
        public Member()
        { }

        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string MemberType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public DateTime IncludeDate { get; set; }
        public DateTime LastModified { get; set; }
        public string Historic { get; set; }
    }
}
