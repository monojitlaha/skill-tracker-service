using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkillTrackerService.Models
{
    public class Profile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string AssociateId { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public List<Skill> TechnicalSkills { get; set; }

        public List<Skill> CommunicationSkills { get; set; }
    }
}
