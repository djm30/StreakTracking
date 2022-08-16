using System.Net;
using MediatR;
using StreakTracking.Endpoints.Application.Contracts.Business;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Commands.AddStreak;

public class AddStreakHandler : IRequestHandler<AddStreakCommand, ResponseMessage>
{
    private readonly IEventPublishingService _publishingService;

    public AddStreakHandler(IEventPublishingService publishingService)
    {
        _publishingService = publishingService ?? throw new ArgumentNullException(nameof(publishingService));
    }

    public async Task<ResponseMessage> Handle(AddStreakCommand request, CancellationToken cancellationToken)
    {
        
        if (request.StreakDescription == "" || request.StreakName == "")
        {
            return new ResponseMessage()
            {
                Message = "Validation failed: Please provide both a name and a description",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        request.StreakId = Guid.NewGuid();

        await _publishingService.PublishCreateStreak(request);
        
        return new ResponseMessage()
        {
            Message = $"Creating new Streak with ID: {request.StreakId}", 
            StatusCode = HttpStatusCode.Accepted
        };
    }
}