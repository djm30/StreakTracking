using StreakTracking.Application.Models;

namespace StreakTracking.Application.Contracts.Business;

public interface IEventPublishingService
{
    public Task<ResponseMessage> PublishCreateStreak(AddStreakDTO addStreakDTO);
    public Task<ResponseMessage> PublishUpdateStreak(string StreakId, UpdateStreakDTO updateStreakDTO);
    public Task<ResponseMessage> PublishDeleteStreak(string StreakId);
    public Task<ResponseMessage> PublishStreakComplete(string StreakId, StreakCompleteDTO streakCompleteDTO);
}