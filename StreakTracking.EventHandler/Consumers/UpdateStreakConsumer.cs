using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using StreakTracking.EventHandler.Models;
using StreakTracking.EventHandler.Repositories;
using StreakTracking.Events.Events;

namespace StreakTracking.EventHandler.Consumers;

public class UpdateStreakConsumer : IConsumer<UpdateStreakEvent>
{

    private readonly ILogger<UpdateStreakConsumer> _logger;
    private readonly IStreakWriteRepository _repository;

    public UpdateStreakConsumer(IStreakWriteRepository repository, ILogger<UpdateStreakConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UpdateStreakEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Recieved message {message}", message);
        var streak = new Streak
        {
            StreakId = message.StreakId,
            StreakName = message.StreakName,
            StreakDescription = message.StreakDescription,
            LongestStreak = message.LongestStreak
        };
        await _repository.Update(streak);
    }
}