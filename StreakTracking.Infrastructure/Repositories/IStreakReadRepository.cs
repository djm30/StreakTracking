using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Infrastructure.Repositories;

public interface IStreakReadRepository
{
    Task<IEnumerable<Streak>> GetStreaks();
    Task<Streak> GetStreakById(string id);
    Task<CurrentStreak> GetCurrent(string id);
}

