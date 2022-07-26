using System;
using System.Threading.Tasks;
using MassTransit;
using StreakTracking.EventHandler.Services;
using StreakTracking.Events.Events;

namespace StreakTracking.EventHandler.Consumers;

public class UpdateStreakConsumer : IConsumer<UpdateStreakEvent>
{
    private readonly IStreakWriteService _service;

    public UpdateStreakConsumer(IStreakWriteService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    public async Task Consume(ConsumeContext<UpdateStreakEvent> context)
    {
        await _service.UpdateStreak(context.Message);
    }
}