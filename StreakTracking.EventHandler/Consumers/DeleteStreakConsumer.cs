using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using StreakTracking.EventHandler;
using StreakTracking.EventHandler.Models;
using StreakTracking.EventHandler.Repositories;
using StreakTracking.Events.Events;

namespace StreakTracking.EventHandler.Consumers;

public class DeleteStreakConsumer : IConsumer<DeleteStreakEvent>
{
    private readonly ILogger<DeleteStreakConsumer> _logger;
    private readonly IStreakWriteRepository _repository;

    public DeleteStreakConsumer(IStreakWriteRepository repository, ILogger<DeleteStreakConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DeleteStreakEvent> context)
    {
        var streakId = context.Message.StreakId;
        await _repository.Delete(streakId.ToString());
    }
    
}