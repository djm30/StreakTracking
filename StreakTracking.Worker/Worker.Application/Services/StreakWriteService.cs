using Microsoft.Extensions.Logging;
using StreakTracking.Domain.Entities;
using StreakTracking.Events.Events;
using StreakTracking.Worker.Application.Contracts.Business;
using StreakTracking.Worker.Application.Contracts.Persistence;

namespace StreakTracking.Worker.Application.Services;
public class StreakWriteService : IStreakWriteService
{
    
    private readonly IStreakWriteRepository _streakRepository;
    private readonly IStreakDayWriteRepository _streakDayRepository;
    private readonly ILogger<StreakWriteService> _logger;

    public StreakWriteService(IStreakWriteRepository streakRepository, IStreakDayWriteRepository streakDayRepository,
        ILogger<StreakWriteService> logger)
    {
        _streakRepository = streakRepository ?? throw new ArgumentNullException(nameof(streakRepository));
        _streakDayRepository = streakDayRepository ?? throw new ArgumentNullException(nameof(streakDayRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task CreateStreak(AddStreakEvent addStreakEvent)
    {
        var streak = new Streak
        {
            StreakId = addStreakEvent.StreakId,
            StreakName = addStreakEvent.StreakName,
            StreakDescription = addStreakEvent.StreakDescription,
            LongestStreak  = 0
        };

        _logger.LogInformation("Received message: {addStreakEvent}, consuming it now", addStreakEvent);
        await _streakRepository.Create(streak: streak);
    }

    public async Task UpdateStreak(UpdateStreakEvent updateStreakEvent)
    {
        _logger.LogInformation("Received message {updateStreakEvent}", updateStreakEvent);
        var streak = new Streak
        {
            StreakId = updateStreakEvent.StreakId,
            StreakName = updateStreakEvent.StreakName,
            StreakDescription = updateStreakEvent.StreakDescription,
            LongestStreak = updateStreakEvent.LongestStreak
        };
        await _streakRepository.Update(streak);
    }

    public async Task MarkComplete(StreakCompleteEvent streakCompleteEvent)
    {
        var streakDay = new StreakDay
        {
            Id = streakCompleteEvent.Date,
            StreakId = streakCompleteEvent.StreakId,
            Complete = streakCompleteEvent.Complete,
        };
        // 
        if (streakDay.Complete)
        {
            await _streakDayRepository.Create(streakDay);
        }
        else
        {
            // DELETING INSTEAD OF MODIFYING, SAVES MORE SPACE
            await _streakDayRepository.Delete(streakDay);
        }
    }

    public async Task DeleteStreak(DeleteStreakEvent deleteStreakEvent)
    {
        var streakId = deleteStreakEvent.StreakId.ToString();
        await _streakDayRepository.DeleteAll(streakId);
        await _streakRepository.Delete(streakId);
    }
}