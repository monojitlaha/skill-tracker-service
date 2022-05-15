using System.Collections.Generic;
using System.Threading.Tasks;
using SkillTrackerService.Models;

namespace SkillTrackerService.Services
{
    public interface IProfileService
    {
        Task<List<Profile>> GetAsync();
        Task<List<Profile>> GetAsync(string criteria, string criteriaValue);
        Task CreateAsync(Profile profile);
        Task UpdateAsync(string id, Profile profile);
        Task RemoveAsync(string id);
    }
}
