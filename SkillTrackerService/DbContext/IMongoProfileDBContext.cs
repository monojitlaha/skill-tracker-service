using MongoDB.Driver;

namespace SkillTrackerService.DbContext
{
    public interface IMongoProfileDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
