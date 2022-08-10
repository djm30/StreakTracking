using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Application.Contracts.Persistance;


public interface IStreakReadRepository
{
    Task<IEnumerable<Streak>> GetStreaks();
    Task<Streak> GetStreakById(string id);
    Task<CurrentStreak> GetCurrent(string id);

    Task<IEnumerable<StreakDay>> GetCompletions(string id);
}

