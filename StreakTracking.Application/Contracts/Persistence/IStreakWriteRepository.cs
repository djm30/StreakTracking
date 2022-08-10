using System.Threading.Tasks;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Application.Contracts.Persistance;

public interface IStreakWriteRepository
    {
        public Task Create(Streak streak);
        public Task Update(Streak streak);
        public Task Delete(string id);
        
    }
