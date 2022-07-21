using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using StreakTracking.EventHandler;
using StreakTracking.EventHandler.Models;
using StreakTracking.EventHandler.Repositories;
using StreakTracking.Events.Events;
using StreakTracking.Services;

namespace StreakTracking.EventHandler.Consumers;

public class DeleteStreakConsumer : IConsumer<DeleteStreakEvent>
{
    private readonly ILogger<DeleteStreakConsumer> _logger;
    private readonly IStreakRemovalService _service;

    public DeleteStreakConsumer(IStreakRemovalService service, ILogger<DeleteStreakConsumer> logger)
    {
        _logger = logger;
        _service = service;
    }

    public async Task Consume(ConsumeContext<DeleteStreakEvent> context)
    {
        var streakId = context.Message.StreakId;
        await _service.DeleteStreak(streakId.ToString());
    }
    
}