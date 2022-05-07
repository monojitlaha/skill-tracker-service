using MongoDB.Driver;
using SkillTrackerService.Models;

namespace SkillTrackerService.DbContext
{
    public class MongoProfileDBContext : IMongoProfileDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }

        public MongoProfileDBContext(IEngineerProfileDatabaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _db = _mongoClient.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return _db.GetCollection<T>(name);
        }
    }
}
