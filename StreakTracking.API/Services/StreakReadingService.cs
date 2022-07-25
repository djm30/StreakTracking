using AutoMapper;
using StreakTracking.API.Models;
using StreakTracking.Infrastructure.Repositories;
using CurrentStreak = StreakTracking.Domain.Calculated.CurrentStreak;

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


    public ResponseMessageContent<Streak> GetStreakById(string Id)
    {
        throw new NotImplementedException();
    }

    public ResponseMessageContent<IEnumerable<Streak>> GetStreaks()
    {
        throw new NotImplementedException();
    }

    public ResponseMessageContent<CurrentStreak> GetCurrentStreak()
    {
        throw new NotImplementedException();
    }
}