using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkillTrackerService.Models
{
    public class Profile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

       public string Name { get; set; } = null!;

        public decimal AssociateId { get; set; }

        public string Email { get; set; } = null!;

        public string Mobile { get; set; } = null!;
    }
}
