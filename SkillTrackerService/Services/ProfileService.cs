using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SkillTrackerService.Models;

namespace SkillTrackerService.Services
{
    public class ProfileService
    {
        private readonly IMongoCollection<Profile> _profiles;
        public ProfileService(IEngineerProfileDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _profiles = database.GetCollection<Profile>(settings.ProfilesCollectionName);
        }

        public async Task<List<Profile>> GetAsync() =>
         await _profiles.Find(_ => true).ToListAsync();

        public async Task<Profile> GetAsync(string id) =>
         await _profiles.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Profile profile) =>
         await _profiles.InsertOneAsync(profile);

        public async Task UpdateAsync(string id, Profile profile) =>
         await _profiles.ReplaceOneAsync(x => x.Id == id, profile);

        public async Task RemoveAsync(string id) =>
         await _profiles.DeleteOneAsync(x => x.Id == id);
    }
}
