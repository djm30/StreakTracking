using StreakTracking.Domain.Entities;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Contracts.Persistence;


public interface IStreakReadRepository
{
    Task<IEnumerable<Streak>> GetStreaks();
    Task<Streak> GetStreakById(string id);
    Task<CurrentStreak> GetCurrent(string id);

    Task<IEnumerable<StreakDay>> GetCompletions(string id);
}

