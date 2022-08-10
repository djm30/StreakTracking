using System.Net;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Contracts.Persistance;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Application.Services;

public class StreakReadingService : IStreakReadingService
{

    // TODO REFACTOR TO mediatR, SERVICES SHOULDN'T BE RETURNING RESPONSE MESSAGE TYPE
    
    private readonly IStreakReadRepository _repository;

    public StreakReadingService(IStreakReadRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ResponseMessageContent<Streak>> GetStreakById(string id)
    {
        var streak = await _repository.GetStreakById(id);
        if (streak == null)
        {
            return new ResponseMessageContent<Streak>
            {
                Content = null,
                Message = "No Streak found for this ID",
                StatusCode = HttpStatusCode.NotFound
            };
        }

        return new ResponseMessageContent<Streak>
        {
            Content = streak,
            Message = "Content Found",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ResponseMessageContent<IEnumerable<Streak>>> GetStreaks()
    {
        var streaks = await _repository.GetStreaks();
        return new ResponseMessageContent<IEnumerable<Streak>>
        {
            Content = streaks,
            Message = "List of streaks",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ResponseMessageContent<CurrentStreak>> GetCurrentStreak(string id)
    {
        var currentStreak = await _repository.GetCurrent(id);
        if (currentStreak is null)
        {
            return new ResponseMessageContent<CurrentStreak>
            {
                Content = null,
                Message = "No Streak found for this ID",
                StatusCode = HttpStatusCode.NotFound
            };
        }

        return new ResponseMessageContent<CurrentStreak>
        {
            Content = currentStreak,
            Message = "Current streak calculated",
            StatusCode = HttpStatusCode.OK
        };
    }

    // TODO UNIT TEST THIS
    public async Task<ResponseMessageContent<FullStreakInfo>> GetFullStreakInfoById(string id)
    {

        var streak = await _repository.GetStreakById(id);
        
        if (streak is null)
        {
            return new ResponseMessageContent<FullStreakInfo>()
            {
                Content = null,
                Message = "No Streak found for this ID",
                StatusCode = HttpStatusCode.NotFound
            };
        }

        var currentStreakResponse = await GetCurrentStreak(id);
        var currentStreak = currentStreakResponse.Content.Streak;

        var streakCompletions = await _repository.GetCompletions(id);

        var fullStreakInfo = new FullStreakInfo()
        {
            StreakId = streak.StreakId,
            StreakName = streak.StreakName,
            StreakDescription = streak.StreakDescription,
            LongestStreak = streak.LongestStreak,
            CurrentStreak = currentStreak,
            Completions = streakCompletions
        };

        return new ResponseMessageContent<FullStreakInfo>()
        {
            Content = fullStreakInfo,
            Message = "Full Streak Info",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ResponseMessageContent<IEnumerable<FullStreakInfo>>> GetFullStreakInfo()
    {
        var listOfStreakInfo = new List<FullStreakInfo>();
        var streaks = await _repository.GetStreaks();
        var streakIds = streaks.Select(s => s.StreakId);

        foreach (var streakId in streakIds)
        {
            var fullStreakInfoResponse = await GetFullStreakInfoById(streakId.ToString());
            if(fullStreakInfoResponse.Content is not null)
                listOfStreakInfo.Add(fullStreakInfoResponse.Content);
        }

        return new ResponseMessageContent<IEnumerable<FullStreakInfo>>()
        {
            Content = listOfStreakInfo,
            Message = "Full Info of all Streaks",
            StatusCode = HttpStatusCode.OK
        };
    }
}