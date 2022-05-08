using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SkillTrackerService.DbContext;
using SkillTrackerService.Models;

namespace SkillTrackerService.Services
{
    public class ProfileService: IProfileService
    {
        private readonly IMongoCollection<Profile> _profiles;

        public ProfileService(IMongoProfileDBContext mongoProfileDBContext)
        {
            _profiles = mongoProfileDBContext.GetCollection<Profile>("Profiles");
        }

        public async Task<List<Profile>> GetAsync() =>
         await _profiles.Find(Builders<Profile>.Filter.Empty, null).ToListAsync();

        public async Task<Profile> GetAsync(string criteria, string criteriaValue)
        {
            FilterDefinition<Profile> filter;
            if (criteria.Trim().ToLower() == "name")
            {
                filter = Builders<Profile>.Filter.Eq("Name", criteriaValue);
            }
            else if (criteria.Trim().ToLower() == "associateid")
            {
                filter = Builders<Profile>.Filter.Eq("AssociateId", criteriaValue);
            }
            else if (criteria.Trim().ToLower() == "email")
            {
                filter = Builders<Profile>.Filter.Eq("Email", criteriaValue);
            }
            else if (criteria.Trim().ToLower() == "mobile")
            {
                filter = Builders<Profile>.Filter.Eq("Mobile", criteriaValue);
            }
            else
            {
                var objectId = new ObjectId(criteriaValue);
                filter = Builders<Profile>.Filter.Eq("_id", objectId);
            }
            return await _profiles.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Profile profile) =>
         await _profiles.InsertOneAsync(profile);

        public async Task UpdateAsync(string id, Profile profile) =>
         await _profiles.ReplaceOneAsync(x => x.Id == id, profile);

        public async Task RemoveAsync(string id) =>
         await _profiles.DeleteOneAsync(x => x.Id == id);
    }
}
