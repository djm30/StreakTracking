using StreakTracking.Events.Events;
using StreakTracking.API.Models;

namespace StreakTracking.API.Services;

public interface IEventPublishingService
{
    public Task<ResponseMessage> PublishCreateStreak(AddStreakDTO addStreakDTO);
    public Task<ResponseMessage> PublishUpdateStreak(string StreakId, UpdateStreakDTO updateStreakDTO);
    public Task<ResponseMessage> PublishDeleteStreak(string StreakId);
    public Task<ResponseMessage> PublishStreakComplete(string StreakId, StreakCompleteDTO streakCompleteDTO);
}