using System.Threading.Tasks;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Infrastructure.Repositories;
public interface IStreakWriteRepository
    {
        public Task Create(Streak streak);
        public Task Update(Streak streak);
        public Task Delete(string id);
        
    }
