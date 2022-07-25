using StreakTracking.API.Models;
using StreakTracking.Events.Events;

namespace StreakTracking.API.Services;

public class EventPublishingService : IEventPublishingService
{
    public async Task<ResponseMessage> PublishCreateStreak(AddStreakEvent addStreakEvent)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseMessage> PublishUpdateStreak(UpdateStreakEvent updateStreakEvent)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseMessage> PublishDeleteStreak(DeleteStreakEvent deleteStreakEvent)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseMessage> PublishStreakComplete(StreakComplete streakCompleteEvent)
    {
        throw new NotImplementedException();
    }
}