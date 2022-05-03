namespace SkillTrackerService.Models
{
    public class EngineerProfileDatabaseSettings:IEngineerProfileDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string ProfilesCollectionName { get; set; }
    }
}
