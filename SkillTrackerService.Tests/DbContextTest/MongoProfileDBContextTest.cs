using MongoDB.Driver;
using Moq;
using SkillTrackerService.DbContext;
using SkillTrackerService.Models;
using Xunit;

namespace SkillTrackerService.Tests.DbContextTest
{
    public class MongoProfileDBContextTest
    {
        private Mock<IMongoDatabase> _mockDB;
        private Mock<IMongoClient> _mockClient;

        public MongoProfileDBContextTest()
        {
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public void MongoProfileDBContext_Constructor_Success()
        {
            var settings = new EngineerProfileDatabaseSettings()
            {
                ConnectionString = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

           _mockClient.Setup(c => c
            .GetDatabase(settings.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new MongoProfileDBContext(settings);

            //Assert 
            Assert.NotNull(context);
        }

        [Fact]
        public void MongoProfileDBContext_GetCollection_NameEmpty_Failure()
        {

            //Arrange
            var settings = new EngineerProfileDatabaseSettings()
            {
                ConnectionString = "mongodb://tes123",
                DatabaseName = "TestDB"
            };

            _mockClient.Setup(c => c
            .GetDatabase(settings.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new MongoProfileDBContext(settings);
            var myCollection = context.GetCollection<Profile>("");

            //Assert 
            Assert.Null(myCollection);
        }

        [Fact]
        public void MongoProfileDBContext_GetCollection_ValidName_Success()
        {
            //Arrange
            var settings = new EngineerProfileDatabaseSettings()
            {
                ConnectionString = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

           _mockClient.Setup(c => c.GetDatabase(settings.DatabaseName, null)).Returns(_mockDB.Object);

            //Act 
            var context = new MongoProfileDBContext(settings);
            var myCollection = context.GetCollection<Profile>("Profiles");

            //Assert 
            Assert.NotNull(myCollection);
        }
    }
}
