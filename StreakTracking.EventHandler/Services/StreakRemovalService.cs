using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreakTracking.Infrastructure.Repositories;

namespace StreakTracking.Services;

/// <summary>
/// According to stack overflow, I shouldn't have repositories being used in other repositories
/// So i've moved that logic here into a 'business layer'
/// I hope im doing this convention correctly
/// </summary>
public class StreakRemovalService : IStreakRemovalService
{
    private readonly IStreakWriteRepository _streakRepository;
    private readonly IStreakDayWriteRepository _streakDayRepository;
    private readonly ILogger<StreakRemovalService> _logger;

    public StreakRemovalService(IStreakWriteRepository streakRepository, IStreakDayWriteRepository streakDayRepository,
        ILogger<StreakRemovalService> logger)
    {
        _streakRepository = streakRepository ?? throw new ArgumentNullException(nameof(streakRepository));
        _streakDayRepository = streakDayRepository ?? throw new ArgumentNullException(nameof(streakDayRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task DeleteStreak(string streakId)
    {
        await _streakDayRepository.DeleteAll(streakId);
        await _streakRepository.Delete(streakId);
    }
}