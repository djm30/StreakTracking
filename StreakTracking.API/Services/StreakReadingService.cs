using AutoMapper;
using StreakTracking.API.Models;
using StreakTracking.Infrastructure.Repositories;
using System.Net;

namespace StreakTracking.API.Services;

public class StreakReadingService : IStreakReadingService
{

    private readonly IStreakReadRepository _repository;
    private readonly IMapper _mapper;

    public StreakReadingService(IStreakReadRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ResponseMessageContent<Streak>> GetStreakById(string Id)
    {
        var streak = await _repository.GetStreakById(Id);
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
            Content = _mapper.Map<Streak>(streak),
            Message = "Content Found",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ResponseMessageContent<IEnumerable<Streak>>> GetStreaks()
    {
        var streaks = await _repository.GetStreaks();
        var mappedStreaks = streaks.Select(s => _mapper.Map<Streak>(s));
        return new ResponseMessageContent<IEnumerable<Streak>>
        {
            Content = mappedStreaks,
            Message = "List of streaks",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ResponseMessageContent<CurrentStreak>> GetCurrentStreak(string Id)
    {
        var currentStreak = await _repository.GetCurrent(Id);
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
            Content = _mapper.Map<CurrentStreak>(currentStreak),
            Message = "Streak information",
            StatusCode = HttpStatusCode.OK
        };

    }
}