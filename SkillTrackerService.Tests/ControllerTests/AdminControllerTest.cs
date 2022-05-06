using System.Collections.Generic;
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
            _mockProfileService.Setup(x => x.GetAsync()).ReturnsAsync(new List<Profile> { new Profile { Name = "Test", AssociateId = "1234", Email = "Test@gmail.com", Mobile = "9876543212" } });
            var output = await _adminController.Get();
            Assert.NotNull(output.Value);
            Assert.Equal("Test", output.Value[0].Name);            
        }
    }
}
