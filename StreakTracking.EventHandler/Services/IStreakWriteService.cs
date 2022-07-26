using System.Threading.Tasks;
using StreakTracking.Events.Events;

namespace StreakTracking.EventHandler.Services;

public interface IStreakWriteService
{
    public Task CreateStreak(AddStreakEvent addStreakEvent );
    public Task UpdateStreak(UpdateStreakEvent updateStreakEvent);
    public Task MarkComplete(StreakCompleteEvent streakCompleteEvent);
    public Task DeleteStreak(DeleteStreakEvent deleteStreakEvent);
}