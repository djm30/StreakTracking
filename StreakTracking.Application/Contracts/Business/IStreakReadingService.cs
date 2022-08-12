using StreakTracking.Application.Models;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;


namespace StreakTracking.Application.Contracts.Business;

public interface IStreakReadingService
{
    public Task<Streak> GetStreakById(string id);
    public Task<IEnumerable<Streak>> GetStreaks();
    public Task<CurrentStreak> GetCurrentStreak(string id);
    public Task<FullStreakInfo> GetFullStreakInfoById(string id);
    public Task<IEnumerable<FullStreakInfo>> GetFullStreakInfo();
}