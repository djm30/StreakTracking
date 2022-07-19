using StreakTracking.Streaks.Models;

namespace StreakTracking.Streaks;

public interface IStreakReadRepository
{
    Task<IEnumerable<Streak>> GetStreaks();
    Task<Streak> GetStreakById(string id);
}

