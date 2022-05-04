using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<List<Profile>>> Get()
        {
            return await _profileService.GetAsync();
        }

        [HttpGet("{criteria}/{criteriaValue}")]
        public async Task<ActionResult<Profile>> Get(string criteria, string criteriaValue)
        {
            if (string.IsNullOrWhiteSpace(criteria) || string.IsNullOrWhiteSpace(criteriaValue))
            {
                return BadRequest();
            }
            
            var profile = await _profileService.GetAsync(criteria, criteriaValue);

            if (profile is null)
            {
                return NotFound();
            }

            return profile;
        }
    }
}
