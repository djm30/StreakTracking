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

    public async Task<Streak> GetStreakById(string id)
    {
        var streak = await _repository.GetStreakById(id);
        return streak;
    }

    public async Task<IEnumerable<Streak>> GetStreaks()
    {
        var streaks = await _repository.GetStreaks();
        return streaks;
    }

    public async Task<CurrentStreak> GetCurrentStreak(string id)
    {
        var currentStreak = await _repository.GetCurrent(id);
        return currentStreak;
    }

    // TODO UNIT TEST THIS
    public async Task<FullStreakInfo> GetFullStreakInfoById(string id)
    {
        var streak = await _repository.GetStreakById(id);
        
        if (streak is null)
        {
            return null;

        }

        var currentStreakResponse = await GetCurrentStreak(id);
        var currentStreak = currentStreakResponse.Streak;

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

        return fullStreakInfo;
    }

    public async Task<IEnumerable<FullStreakInfo>> GetFullStreakInfo()
    {
        var listOfStreakInfo = new List<FullStreakInfo>();
        var streaks = await _repository.GetStreaks();
        var streakIds = streaks.Select(s => s.StreakId);

        foreach (var streakId in streakIds)
        {
            var fullStreakInfo = await GetFullStreakInfoById(streakId.ToString());
            if(fullStreakInfo is not null)
                listOfStreakInfo.Add(fullStreakInfo);
        }

        return listOfStreakInfo;
    }
}