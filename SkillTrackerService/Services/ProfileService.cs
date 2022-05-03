using System.Collections.Generic;
using System.Linq;
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

        public List<Profile> Get()
        {
            List<Profile> profiles;
            profiles = _profiles.Find(emp => true).ToList();
            return profiles;
        }

        public Profile Get(string id) =>
            _profiles.Find<Profile>(profile => profile.Id == id).FirstOrDefault();

        public void Create(Profile profile) =>
            _profiles.InsertOne(profile);

        public void Update(string id, Profile profile) =>
            _profiles.ReplaceOne(x => x.Id == id, profile);

        public void Remove(string id) =>
            _profiles.DeleteOne(x => x.Id == id);

    }
}
