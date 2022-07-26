using StreakTracking.API.Models;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;


namespace StreakTracking.API.Services;

public interface IStreakReadingService
{
    public Task<ResponseMessageContent<Streak>> GetStreakById(string id);
    public Task<ResponseMessageContent<IEnumerable<Streak>>> GetStreaks();
    public Task<ResponseMessageContent<CurrentStreak>> GetCurrentStreak(string id);
}