using System.Threading.Tasks;
using StreakTracking.EventHandler.Models;

namespace StreakTracking.EventHandler
{
    public interface IStreakWriteRepository
    {
        public Task Create(Streak streak);
        public Task Update(Streak streak);
        public Task Delete(Streak streak);
    }
}