using StreakTracking.Events.Events;
using StreakTracking.API.Models;

namespace StreakTracking.API.Services;

public interface IEventPublishingService
{
    public ResponseMessage PublishCreateStreak(AddStreakEvent addStreakEvent);
    public ResponseMessage PublishUpdateStreak(UpdateStreakEvent updateStreakEvent);
    public ResponseMessage PublishDeleteStreak(DeleteStreakEvent deleteStreakEvent);
    public ResponseMessage PublishStreakComplete(StreakComplete streakCompleteEvent);
}