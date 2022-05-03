using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillTrackerService.Models;
using SkillTrackerService.Services;

namespace StockMarketService.Controllers
{
    [ApiController]
    [Route("skill-tracker/api/[controller]")]
    public class EngineerController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly ILogger<EngineerController> _logger;

        public EngineerController(ILogger<EngineerController> logger, ProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        [HttpPost]
        [ActionName("add-profile")]
        public ActionResult<Profile> Post(Profile newProfile)
        {
            _profileService.Create(newProfile);

            return newProfile;
        }

        [HttpPut("{id:length(24)}")]
        [ActionName("update-profile")]
        public IActionResult Update(string id, Profile newProfile)
        {
            var book = _profileService.Get(id);

            if (book is null)
            {
                return NotFound();
            }

            newProfile.Id = book.Id;

            _profileService.Update(id, newProfile);

            return NoContent();
        }
    }
}
