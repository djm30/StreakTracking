using StreakTracking.Application.Streaks.Commands.AddStreak;
using StreakTracking.Application.Streaks.Commands.DeleteStreak;
using StreakTracking.Application.Streaks.Commands.StreakComplete;
using StreakTracking.Application.Streaks.Commands.UpdateStreak;

namespace StreakTracking.Application.Contracts.Business;

public interface IEventPublishingService
{
    public Task PublishCreateStreak(AddStreakCommand addStreakCommand);
    public Task PublishUpdateStreak(UpdateStreakCommand updateStreakCommand);
    public Task PublishDeleteStreak(DeleteStreakCommand deleteStreakCommand);
    public Task PublishStreakComplete(StreakCompleteCommand streakCompleteCommand);
}