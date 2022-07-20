using StreakTracking.Models;

namespace StreakTracking.Repositories;

public interface IStreakReadRepository
{
    Task<IEnumerable<Streak>> GetStreaks();
    Task<Streak> GetStreakById(string id);
    Task<int> GetCurrent(string id);
}

