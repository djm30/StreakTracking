using System.Net;
using MediatR;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Models;

namespace StreakTracking.Application.Streaks.Commands.UpdateStreak;

public class UpdateStreakHandler : IRequestHandler<UpdateStreakCommand, ResponseMessage>
{
    private readonly IEventPublishingService _publishingService;


    public UpdateStreakHandler(IEventPublishingService publishingService)
    {
        _publishingService = publishingService ?? throw new ArgumentNullException(nameof(publishingService));
    }

    public async Task<ResponseMessage> Handle(UpdateStreakCommand request, CancellationToken cancellationToken)
    {
        if (request.LongestStreak < 0 || request.StreakDescription == "" ||
            request.StreakName == "")
        {
            return new ResponseMessage()
            {
                Message = "Validation failed: Please provide a name, description and a valid value for the longest streak",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
        
        var validGuid = Guid.TryParse(request.stringStreakId, out Guid parsedResult);

        if (!validGuid)
        {
            return new ResponseMessage()
            {
                Message = "Invalid GUID provided",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
        
        request.StreakId = parsedResult;
        await _publishingService.PublishUpdateStreak(request);
        
        return new ResponseMessage()
        {
            Message = $"Currently updating Streak with ID: {request.StreakId}", 
            StatusCode = HttpStatusCode.Accepted
        };
    }
}