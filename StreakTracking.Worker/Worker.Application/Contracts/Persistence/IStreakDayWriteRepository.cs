using StreakTracking.Domain.Entities;

namespace StreakTracking.Worker.Application.Contracts.Persistence;

public interface IStreakDayWriteRepository
{
    public Task Create(StreakDay day);
    public Task Update (StreakDay day);
    public Task Delete(StreakDay day);
    public Task DeleteAll(string StreakId);
}