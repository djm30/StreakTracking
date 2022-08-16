using StreakTracking.Endpoints.Application.Streaks.Commands.AddStreak;
using StreakTracking.Endpoints.Application.Streaks.Commands.DeleteStreak;
using StreakTracking.Endpoints.Application.Streaks.Commands.StreakComplete;
using StreakTracking.Endpoints.Application.Streaks.Commands.UpdateStreak;

namespace StreakTracking.Endpoints.Application.Contracts.Business;

public interface IEventPublishingService
{
    public Task PublishCreateStreak(AddStreakCommand addStreakCommand);
    public Task PublishUpdateStreak(UpdateStreakCommand updateStreakCommand);
    public Task PublishDeleteStreak(DeleteStreakCommand deleteStreakCommand);
    public Task PublishStreakComplete(StreakCompleteCommand streakCompleteCommand);
}