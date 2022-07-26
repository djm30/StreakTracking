using StreakTracking.API.Models;


namespace StreakTracking.API.Services;

public interface IStreakReadingService
{
    public Task<ResponseMessageContent<Streak>> GetStreakById(string id);
    public Task<ResponseMessageContent<IEnumerable<Streak>>> GetStreaks();
    public Task<ResponseMessageContent<CurrentStreak>> GetCurrentStreak(string id);
}