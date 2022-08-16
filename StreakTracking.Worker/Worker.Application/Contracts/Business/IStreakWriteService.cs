using StreakTracking.Events.Events;

namespace StreakTracking.Worker.Application.Contracts.Business;

public interface IStreakWriteService
{
    public Task CreateStreak(AddStreakEvent addStreakEvent );
    public Task UpdateStreak(UpdateStreakEvent updateStreakEvent);
    public Task MarkComplete(StreakCompleteEvent streakCompleteEvent);
    public Task DeleteStreak(DeleteStreakEvent deleteStreakEvent);
}