using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using StreakTracking.EventHandler.Models;
using StreakTracking.EventHandler.Repositories;
using StreakTracking.Events.Events;

namespace StreakTracking.EventHandler.Consumers
{
    public class AddStreakConsumer : IConsumer<AddStreakEvent>
    {

        private readonly IStreakWriteRepository _repository;
        private readonly ILogger<AddStreakConsumer> _logger;

        public AddStreakConsumer(IStreakWriteRepository repository, ILogger<AddStreakConsumer> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<AddStreakEvent> context)
        {

            var message = context.Message;
            
            var streak = new Streak
            {
                StreakId = Guid.NewGuid(),
                StreakName = context.Message.Name,
                StreakDescription = context.Message.Description,
                LongestStreak  = 0
            };
            _logger.LogInformation("Recieved message: {message}, consuming it now", context.Message);
            await _repository.Create(streak: streak);
        }
    }
}