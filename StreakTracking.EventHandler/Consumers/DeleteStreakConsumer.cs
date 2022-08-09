using System;
using System.Threading.Tasks;
using MassTransit;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Events.Events;

namespace StreakTracking.EventHandler.Consumers;

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