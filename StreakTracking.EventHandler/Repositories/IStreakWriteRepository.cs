using System.Threading.Tasks;
using StreakTracking.EventHandler.Models;

namespace StreakTracking.EventHandler.Repositories
{
    public interface IStreakWriteRepository
    {
        public Task Create(Streak streak);
        public Task Update(Streak streak);
        public Task Delete(string id);
        
    }
}