using StreakTracking.API.Models;
using CurrentStreak = StreakTracking.Domain.Calculated.CurrentStreak;

namespace StreakTracking.API.Services;

public interface IStreakReadingService
{
    public ResponseMessageContent<Streak> GetStreakById(string Id);
    public ResponseMessageContent<IEnumerable<Streak>> GetStreaks();
    public ResponseMessageContent<CurrentStreak> GetCurrentStreak();
}