using StreakTracking.Domain.Entities;

namespace StreakTracking.Worker.Application.Contracts.Persistence;

public interface IStreakDayWriteRepository
{
    public Task<DatabaseWriteResult> Create(StreakDay day);
    public Task<DatabaseWriteResult> Update (StreakDay day);
    public Task<DatabaseWriteResult> Delete(StreakDay day);
    public Task<DatabaseWriteResult> DeleteAll(string StreakId);
}