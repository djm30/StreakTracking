using StreakTracking.Domain.Entities;

namespace StreakTracking.Worker.Application.Contracts.Persistence;

public interface IStreakWriteRepository
    {
        public Task Create(Streak streak);
        public Task Update(Streak streak);
        public Task Delete(string id);
        
    }
