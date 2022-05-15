using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SkillTrackerService.Models;
using SkillTrackerService.Services;
using StockMarketService.Controllers;
using Xunit;

namespace SkillTrackerService.Tests.ControllerTests
{
    public class EngineerControllerTest
    {
        private readonly Mock<IProfileService> _mockProfileService;
        private readonly Mock<ILogger<EngineerController>> _mockLogger;
        private EngineerController _engineerController;

        public EngineerControllerTest()
        {
            _mockProfileService = new Mock<IProfileService>();
            _mockLogger = new Mock<ILogger<EngineerController>>();
            _engineerController = new EngineerController(_mockLogger.Object, _mockProfileService.Object);
        }

        [Fact]
        public async void Post_Result_PositiveTest()
        {
            //Arrange
            Profile inputValue = new Profile { Name = "Test", AssociateId = "1234", Email = "Test@gmail.com", Mobile = "9876543212" };
            _mockProfileService.Setup(x => x.CreateAsync(It.IsAny<Profile>()));
            //Act
            var output = await _engineerController.Post(inputValue);
            //Assert
            Assert.NotNull(output.Value);
            Assert.Equal("Test", output.Value.Name);
        }

        [Fact]
        public async void Post_Result_NegativeTest()
        {
            //Arrange
            Profile inputValue = null;
            _mockProfileService.Setup(x => x.CreateAsync(It.IsAny<Profile>()));
            //Act
            var output = await _engineerController.Post(inputValue);
            // Assert
            Assert.Equal(400, ((StatusCodeResult)output.Result).StatusCode);
        }

        [Fact]
        public async void Update_Result_PositiveTest()
        {
            //Arrange
            Profile profile = new Profile { Id = "1", Name = "Test", AssociateId = "1234", Email = "Test@gmail.com", Mobile = "9876543212" };
            List<Profile> profiles = new List<Profile>
            {
                profile
            };
            _mockProfileService.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(profiles);
            _mockProfileService.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Profile>()));
            //Act
            var output = await _engineerController.Update("1", profile);
            //Assert
            _mockProfileService.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockProfileService.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Profile>()), Times.Once);
        }

        [Fact]
        public async void Update_Result_NegativeTest_Profile_Is_Null()
        {
            //Arrange
            Profile inputValue = null;
            //Act
            var output = await _engineerController.Update("1", inputValue);
            // Assert
            Assert.Equal(400, ((StatusCodeResult)output).StatusCode);
        }

        [Fact]
        public async void Update_Result_NegativeTest_Id_Is_Null()
        {
            //Arrange
            Profile profile = new Profile { Id = "1", Name = "Test", AssociateId = "1234", Email = "Test@gmail.com", Mobile = "9876543212" };
            //Act
            var output = await _engineerController.Update(string.Empty, profile);
            // Assert
            Assert.Equal(400, ((StatusCodeResult)output).StatusCode);
        }

        [Fact]
        public async void Update_Result_No_Profile_Exists()
        {
            //Arrange
            Profile newProfile = new Profile { Id = "1", Name = "Test", AssociateId = "1234", Email = "Test@gmail.com", Mobile = "9876543212" };
            Profile profile = null;
            List<Profile> profiles = new List<Profile>
            {
                profile
            };
            _mockProfileService.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(profiles);
            _mockProfileService.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Profile>()));
            //Act
            var output = await _engineerController.Update("2", newProfile);
            //Assert
            _mockProfileService.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockProfileService.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Profile>()), Times.Never);
        }
    }
}
