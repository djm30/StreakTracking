using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using StreakTracking.EventHandler.Models;
using StreakTracking.EventHandler.Repositories;
using StreakTracking.Events.Events;
using StreakTracking.Services;

namespace StreakTracking.EventHandler.Consumers;

public class StreakCompleteConsumer : IConsumer<StreakCompleteEvent>
{
    private readonly ILogger<StreakCompleteConsumer> _logger;
    private readonly IStreakDayWriteRepository _repository;

    public StreakCompleteConsumer(ILogger<StreakCompleteConsumer> logger, IStreakDayWriteRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Consume(ConsumeContext<StreakCompleteEvent> context)
    {
        // TODO REFACTOR USING AUTOMAPER
        var message = context.Message;
        var streakDay = new StreakDay
        {
            Id = message.Date,
            StreakId = message.Id,
            Complete = message.Complete,
        };
        // 
        if (streakDay.Complete)
        {
            await _repository.Create(streakDay);
        }
        else
        {
            // DELETING INSTEAD OF MODIFYING, SAVES MORE SPACE
            await _repository.Delete(streakDay);
        }
    }
}
