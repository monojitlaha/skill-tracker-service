using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SkillTrackerService.Controllers;
using SkillTrackerService.Models;
using SkillTrackerService.Services;
using Xunit;

namespace SkillTrackerService.Tests.ControllerTests
{
    public class AdminControllerTest
    {
        private readonly Mock<IProfileService> _mockProfileService;
        private readonly Mock<ILogger<AdminController>> _mockLogger;
        private AdminController _adminController;

        public AdminControllerTest()
        {
            _mockProfileService = new Mock<IProfileService>();
            _mockLogger = new Mock<ILogger<AdminController>>();
            _adminController = new AdminController(_mockLogger.Object, _mockProfileService.Object);
        }
   
        [Fact]
        public async void Get_Result_PositiveTest()
        {
            //Arrange
            _mockProfileService.Setup(x => x.GetAsync()).ReturnsAsync(new List<Profile> { new Profile { Name = "Test", AssociateId = "1234", Email = "Test@gmail.com", Mobile = "9876543212" } });
            //Act
            var output = await _adminController.Get();
            //Assert
            Assert.NotNull(output.Value);
            Assert.Equal("Test", output.Value[0].Name);            
        }

        [Fact]
        public async void Get_Result_By_Criteria_PositiveTest()
        {
            //Arrange
            _mockProfileService.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Profile { Name = "Test", AssociateId = "1234", Email = "Test@gmail.com", Mobile = "9876543212" });
            //Act
            var output = await _adminController.Get("Name", "Test");
            //Assert
            Assert.NotNull(output.Value);
            Assert.Equal("Test", output.Value.Name);
        }

        [Fact]
        public async void Get_Result_By_Criteria_Result_Is_Null()
        {
            //Arrange
            Profile profile = null;
            _mockProfileService.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(profile);
            //Act
            var response = await _adminController.Get("Name", "Test1");
            // Assert
            Assert.Equal(404, ((StatusCodeResult)response.Result).StatusCode);
        }

        [Fact]
        public async void Get_Result_By_Criteria_Returns_BadRequestErrorMessageResult_When_Criteria_Is_Empty()
        {
            // Act
            var response = await _adminController.Get(string.Empty, string.Empty);

            // Assert
            Assert.Equal(400, ((StatusCodeResult)response.Result).StatusCode);
        }

        [Fact]
        public async void Get_Result_By_Criteria_Returns_BadRequestErrorMessageResult_When_Criteria_Value_Is_Empty()
        {
            // Act
            var response = await _adminController.Get("Name", string.Empty);

            // Assert
            Assert.Equal(400, ((StatusCodeResult)response.Result).StatusCode);
        }
    }
}
