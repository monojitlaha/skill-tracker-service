using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillTrackerService.Models;
using SkillTrackerService.Services;

namespace SkillTrackerService.Controllers
{
    [Route("skill-tracker/api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, ProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        [HttpGet]
        public ActionResult<List<Profile>> Get()
        {
            return _profileService.Get();
        }

        [HttpGet("{criteria}/{criteriaValue}")]
        public ActionResult<Profile> Get(string criteria, string criteriaValue)
        {
            string id = string.Empty;
            if(criteria == "Id")
            {
                id = criteriaValue;
            }
            var profile = _profileService.Get(id);

            if (profile is null)
            {
                return NotFound();
            }

            return profile;
        }
    }
}
