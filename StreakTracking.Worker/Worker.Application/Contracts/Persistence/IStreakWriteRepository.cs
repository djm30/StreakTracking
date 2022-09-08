using StreakTracking.Domain.Entities;

namespace StreakTracking.Worker.Application.Contracts.Persistence;

public interface IStreakWriteRepository
    {
        public Task<DatabaseWriteResult> Create(Streak streak);
        public Task<DatabaseWriteResult> Update(Streak streak);
        public Task<DatabaseWriteResult> Delete(string id);
        
    }
