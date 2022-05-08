using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Moq;
using SkillTrackerService.DbContext;
using SkillTrackerService.Models;
using SkillTrackerService.Services;
using Xunit;

namespace SkillTrackerService.Tests.Services
{
    public class ProfileServiceTest
    {
        private Mock<IMongoProfileDBContext> _mockDBContext;
        private Mock<IMongoCollection<Profile>> _mockCollection;
        private ProfileService _profileService;
        List<Profile> _profiles;

        public ProfileServiceTest()
        {
                       
        }

        [Fact]
        public async void GetAsync_Result_PositiveTest()
        {
            //Arrange
            string id = "5dc1039a1521eaa36835e541";

            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };
            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

             _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                                It.IsAny<FindOptions<Profile, Profile>>(),
                                 It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            var output = await _profileService.GetAsync();
            //Assert
            Assert.NotNull(output);
        }

        [Fact]
        public async void GetAsync_SearchByName_Result_PositiveTest()
        {
            //Arrange
            string id = "5dc1039a1521eaa36835e541";

            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };
            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

            _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                               It.IsAny<FindOptions<Profile, Profile>>(),
                                It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            var output = await _profileService.GetAsync("Name", "Test");
            //Assert
            Assert.NotNull(output);
        }

        [Fact]
        public async void GetAsync_SearchByAssociateId_Result_PositiveTest()
        {
            //Arrange
            string id = "5dc1039a1521eaa36835e541";

            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };
            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

            _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                               It.IsAny<FindOptions<Profile, Profile>>(),
                                It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            var output = await _profileService.GetAsync("AssociateId", "12345");
            //Assert
            Assert.NotNull(output);
        }

        [Fact]
        public async void GetAsync_SearchByEmail_Result_PositiveTest()
        {
            //Arrange
            string id = "5dc1039a1521eaa36835e541";

            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };
            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

            _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                               It.IsAny<FindOptions<Profile, Profile>>(),
                                It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            var output = await _profileService.GetAsync("Email", "Test@gmail.com");
            //Assert
            Assert.NotNull(output);
        }

        [Fact]
        public async void GetAsync_SearchByMobile_Result_PositiveTest()
        {
            //Arrange
            string id = "5dc1039a1521eaa36835e541";

            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };
            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

            _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                               It.IsAny<FindOptions<Profile, Profile>>(),
                                It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            var output = await _profileService.GetAsync("Mobile", "9876543211");
            //Assert
            Assert.NotNull(output);
        }

        [Fact]
        public async void GetAsync_SearchById_Result_PositiveTest()
        {
            //Arrange
            string id = "5dc1039a1521eaa36835e541";

            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };
            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

            _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                               It.IsAny<FindOptions<Profile, Profile>>(),
                                It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            var output = await _profileService.GetAsync("Id", "5dc1039a1521eaa36835e541");
            //Assert
            Assert.NotNull(output);
        }

        [Fact]
        public async void CreateAsync_Result_PositiveTest()
        {
            //Arrange
            string id = "5dc1039a1521eaa36835e541";

            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };

            var newProfile = new Profile
            {
                Name = "Test1",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };

            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

            _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                               It.IsAny<FindOptions<Profile, Profile>>(),
                                It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockCollection.
                Setup(x => x.InsertOne(newProfile, null, default(CancellationToken)));

            _mockCollection.
                Setup(x => x.InsertOneAsync(newProfile, null, default(CancellationToken)))
                .Returns(Task.CompletedTask);

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            await _profileService.CreateAsync(newProfile);
            //Assert
            _mockCollection.
               Verify(x => x.InsertOneAsync(newProfile, null, default(CancellationToken)), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_Result_PositiveTest()
        {
            //Arrange
            string id = "5dc1039a1521eaa36835e541";

            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };

            var newProfile = new Profile
            {
                Name = "Test1",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = id
            };

            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

            _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                               It.IsAny<FindOptions<Profile, Profile>>(),
                                It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockCollection.
                Setup(x => x.ReplaceOne(It.IsAny<FilterDefinition<Profile>>(), newProfile,(ReplaceOptions)null, default(CancellationToken)));
            var replaceOneResult = new ReplaceOneResult.Acknowledged(1, 1, null);
            _mockCollection.
                Setup(x => x.ReplaceOneAsync(It.IsAny<FilterDefinition<Profile>>(), newProfile, (ReplaceOptions)null, default(CancellationToken)))
                .ReturnsAsync(replaceOneResult);

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            await _profileService.UpdateAsync(profile.Id, newProfile);
            //Assert
            //Assert
            _mockCollection.
                Verify(x => x.ReplaceOneAsync(It.IsAny<FilterDefinition<Profile>>(), newProfile, (ReplaceOptions)null, default(CancellationToken)), Times.Once);
        }

        [Fact]
        public async void RemoveAsync_Result_PositiveTest()
        {
            //Arrange
            var profile = new Profile
            {
                Name = "Test",
                AssociateId = "12345",
                Email = "Test@gmail.com",
                Mobile = "9876543211",
                Id = "5dc1039a1521eaa36835e541"
            };

            _profiles = new List<Profile>
            {
                profile
            };
            _mockCollection = new Mock<IMongoCollection<Profile>>();
            _mockCollection.Object.InsertOne(profile);
            _mockDBContext = new Mock<IMongoProfileDBContext>();
            Mock<IAsyncCursor<Profile>> _profileCursor = new Mock<IAsyncCursor<Profile>>();
            _profileCursor.Setup(_ => _.Current).Returns(_profiles);
            _profileCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _profileCursor
               .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(true))
               .Returns(Task.FromResult(false));

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Profile>>(),
                                    It.IsAny<FindOptions<Profile, Profile>>(),
                                    It.IsAny<CancellationToken>())).Returns(_profileCursor.Object);

            _mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<Profile>>(),
                               It.IsAny<FindOptions<Profile, Profile>>(),
                                It.IsAny<CancellationToken>())).ReturnsAsync(_profileCursor.Object);

            _mockCollection.
                Setup(x => x.DeleteOne(It.IsAny<FilterDefinition<Profile>>(), default(CancellationToken)));
            _mockCollection.
                Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Profile>>(), default(CancellationToken)));

            _mockDBContext.Setup(x => x.GetCollection<Profile>("Profiles")).Returns(_mockCollection.Object);

            _profileService = new ProfileService(_mockDBContext.Object);
            //Act
            await _profileService.RemoveAsync(profile.Id);
            //Assert
            //Assert
            _mockCollection.
                Verify(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Profile>>(), default(CancellationToken)), Times.Once);
        }
    }
}
