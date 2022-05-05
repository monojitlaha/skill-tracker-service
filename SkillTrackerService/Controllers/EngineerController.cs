﻿using System.Threading.Tasks;
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
        private readonly IProfileService _profileService;
        private readonly ILogger<EngineerController> _logger;

        public EngineerController(ILogger<EngineerController> logger, IProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        [HttpPost]
        [ActionName("add-profile")]
        public async Task<ActionResult<Profile>> Post(Profile newProfile)
        {
            if (newProfile is null)
            {
                _logger.LogInformation("Input Parameter is not valid");
                return BadRequest();
            }

            await _profileService.CreateAsync(newProfile);
            _logger.LogInformation("Created Profile Successfully");
            return newProfile;
        }

        [HttpPut("{id:length(24)}")]
        [ActionName("update-profile")]
        public async Task<ActionResult> Update(string id, Profile newProfile)
        {
            if(string.IsNullOrWhiteSpace(id) || newProfile is null)
            {
                _logger.LogInformation("Input Parameters is not valid");
                return BadRequest();
            }

            var book = await _profileService.GetAsync("Id", id);

            if (book is null)
            {
                _logger.LogInformation("Profile Information not found");
                return NotFound();
            }

            newProfile.Id = book.Id;

            await _profileService.UpdateAsync(id, newProfile);
            _logger.LogInformation("Updated Profile Successfully");
            return NoContent();
        }
    }
}
