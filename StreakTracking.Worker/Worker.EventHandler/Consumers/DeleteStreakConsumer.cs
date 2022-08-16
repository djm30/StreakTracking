using System;
using System.Threading.Tasks;
using MassTransit;
using StreakTracking.Events.Events;
using StreakTracking.Worker.Application.Contracts.Business;

namespace StreakTracking.Worker.EventHandler.Consumers;

public class DeleteStreakConsumer : IConsumer<DeleteStreakEvent>
{
    private readonly IStreakWriteService _service;

    public DeleteStreakConsumer(IStreakWriteService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    public async Task Consume(ConsumeContext<DeleteStreakEvent> context)
    {
        await _service.DeleteStreak(context.Message);
    }
    
}