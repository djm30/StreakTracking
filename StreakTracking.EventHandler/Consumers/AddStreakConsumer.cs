using System;
using System.Threading.Tasks;
using MassTransit;
using StreakTracking.EventHandler.Services;
using StreakTracking.Events.Events;

namespace StreakTracking.EventHandler.Consumers
{
    public class AddStreakConsumer : IConsumer<AddStreakEvent>
    {
        private readonly IStreakWriteService _service;

        public AddStreakConsumer(IStreakWriteService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task Consume(ConsumeContext<AddStreakEvent> context)
        {
            await _service.CreateStreak(context.Message);
        }
    }
}