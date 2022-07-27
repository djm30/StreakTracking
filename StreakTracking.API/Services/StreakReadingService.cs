using AutoMapper;
using StreakTracking.API.Models;
using StreakTracking.Infrastructure.Repositories;
using System.Net;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;

namespace StreakTracking.API.Services;

public class StreakReadingService : IStreakReadingService
{

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
        if (currentStreak == null)
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
}