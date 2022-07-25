using StreakTracking.Events.Events;
using StreakTracking.API.Models;

namespace StreakTracking.API.Services;

public interface IEventPublishingService
{
    public Task<ResponseMessage> PublishCreateStreak(AddStreakEvent addStreakEvent);
    public Task<ResponseMessage> PublishUpdateStreak(UpdateStreakEvent updateStreakEvent);
    public Task<ResponseMessage> PublishDeleteStreak(DeleteStreakEvent deleteStreakEvent);
    public Task<ResponseMessage> PublishStreakComplete(StreakComplete streakCompleteEvent);
}