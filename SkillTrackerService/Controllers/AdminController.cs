using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SkillTrackerService.Models;
using SkillTrackerService.Services;

namespace SkillTrackerService.Controllers
{
    [Route("skill-tracker/api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<AdminController> _logger;
        private readonly IMemoryCache _memoryCache;

        public AdminController(ILogger<AdminController> logger, IProfileService profileService,
                               IMemoryCache memoryCache)
        {
            _logger = logger;
            _profileService = profileService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<List<Profile>>> Get()
        {
            try
            {
                _logger.LogInformation("Invoking GET method");
                var result = await _profileService.GetAsync();
                if (result == null || !result.Any())
                    return NotFound();
                _logger.LogInformation("Receieved Search Result Successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in get-profile:{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("{criteria}/{criteriaValue}")]
        public async Task<ActionResult<List<Profile>>> Get(string criteria, string criteriaValue)
        {
            if (string.IsNullOrWhiteSpace(criteria) || string.IsNullOrWhiteSpace(criteriaValue))
            {
                _logger.LogInformation("AdminController Index executed at {date}", DateTime.UtcNow);
                _logger.LogInformation("Input Parameter is not valid");
                return BadRequest();
            }

            try
            {
                _logger.LogInformation("Invoking GET method by passing Search Criteria");
                var cacheKey = $"{criteria.ToLower()}_{criteriaValue.ToLower()}";
                if (!_memoryCache.TryGetValue(cacheKey, out List<Profile> profiles))
                {
                    profiles = await _profileService.GetAsync(criteria, criteriaValue);
                    var cacheExpirationOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(6),
                        Priority = CacheItemPriority.Normal,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    };
                    if (profiles != null && profiles.Any())
                        _memoryCache.Set(cacheKey, profiles, cacheExpirationOptions);
                }

                _logger.LogInformation("Receieved Search Result Successfully");

                return Ok(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in get-profile-by-criteria:{ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
