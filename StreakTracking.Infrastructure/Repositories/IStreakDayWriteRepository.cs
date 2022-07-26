using StreakTracking.Domain.Entities;

namespace StreakTracking.Infrastructure.Repositories;

public interface IStreakDayWriteRepository
{
    public Task Create(StreakDay day);
    public Task Update (StreakDay day);
    public Task Delete(StreakDay day);
    public Task DeleteAll(string StreakId);
}