using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreakTracking.EventHandler.Models;
using StreakTracking.EventHandler.Repositories;

namespace StreakTracking.Services;

/// <summary>
/// According to stack overflow, I shouldn't have repositories being used in other repositories
/// So i've moved that logic here into a 'business layer'
/// I hope im doing this convention correctly
/// </summary>
public class StreakDayService : IStreakDayService
{
    private readonly IStreakWriteRepository _streakRepository;
    private readonly IStreakDayWriteRepository _streakDayRepository;
    private readonly ILogger<StreakDayService> _logger;

    public StreakDayService(IStreakWriteRepository streakRepository, IStreakDayWriteRepository streakDayRepository,
        ILogger<StreakDayService> logger)
    {
        _streakRepository = streakRepository ?? throw new ArgumentNullException(nameof(streakRepository));
        _streakDayRepository = streakDayRepository ?? throw new ArgumentNullException(nameof(streakDayRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task DeleteStreak(string streakId)
    {
        throw new NotImplementedException();
    }
}