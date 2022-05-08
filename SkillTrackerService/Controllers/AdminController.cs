using System;
using System.Collections.Generic;
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
            _logger.LogInformation("Receieved Search Result Successfully");
            return await _profileService.GetAsync();
        }

        [HttpGet("{criteria}/{criteriaValue}")]
        public async Task<ActionResult<Profile>> Get(string criteria, string criteriaValue)
        {
            if (string.IsNullOrWhiteSpace(criteria) || string.IsNullOrWhiteSpace(criteriaValue))
            {
                _logger.LogInformation("Input Parameter is not valid");
                return BadRequest();
            }

            var cacheKey = $"{criteria.ToLower()}_{criteriaValue.ToLower()}";
            if (!_memoryCache.TryGetValue(cacheKey, out Profile profile))
            {
                profile = await _profileService.GetAsync(criteria, criteriaValue);
                var cacheExpirationOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
                if(profile != null)
                 _memoryCache.Set(cacheKey, profile, cacheExpirationOptions);
            }

            if (profile is null)
            {
                _logger.LogInformation("Search Result is Empty");
                return NotFound();
            }
            _logger.LogInformation("Receieved Search Result Successfully");

            return profile;
        }
    }
}
