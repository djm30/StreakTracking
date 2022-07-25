using StreakTracking.API.Models;
using StreakTracking.Events.Events;

namespace StreakTracking.API.Services;

public class EventPublishingService : IEventPublishingService
{
    public ResponseMessage PublishCreateStreak(AddStreakEvent addStreakEvent)
    {
        throw new NotImplementedException();
    }

    public ResponseMessage PublishUpdateStreak(UpdateStreakEvent updateStreakEvent)
    {
        throw new NotImplementedException();
    }

    public ResponseMessage PublishDeleteStreak(DeleteStreakEvent deleteStreakEvent)
    {
        throw new NotImplementedException();
    }

    public ResponseMessage PublishStreakComplete(StreakComplete streakCompleteEvent)
    {
        throw new NotImplementedException();
    }
}