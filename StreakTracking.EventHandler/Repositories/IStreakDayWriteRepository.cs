using System.Threading.Tasks;
using StreakTracking.EventHandler.Models;

namespace StreakTracking.EventHandler.Repositories;

public interface IStreakDayWriteRepository
{
    public Task Create(StreakDay day);
    public Task Update (StreakDay day);
    public Task Delete(StreakDay day);
    public Task DeleteAll(string StreakId);
}