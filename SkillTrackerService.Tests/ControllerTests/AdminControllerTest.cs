using Microsoft.Extensions.Logging;
using Moq;
using SkillTrackerService.Controllers;
using SkillTrackerService.Services;
using System;
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
        public void Test1()
        {

        }
    }
}
