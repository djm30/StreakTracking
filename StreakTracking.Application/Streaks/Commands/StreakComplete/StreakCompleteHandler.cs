using System.Net;
using MediatR;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Models;

namespace StreakTracking.Application.Streaks.Commands.StreakComplete;

public class StreakCompleteHandler : IRequestHandler<StreakCompleteCommand, ResponseMessage>
{
    private readonly IEventPublishingService _publishingService;

    public StreakCompleteHandler(IEventPublishingService publishingService)
    {
        _publishingService = publishingService ?? throw new ArgumentNullException(nameof(publishingService));
    }
    
    public async Task<ResponseMessage> Handle(StreakCompleteCommand request, CancellationToken cancellationToken)
    {
        if (request.Date > DateTime.Today)
        {
            return new ResponseMessage()
            {
                Message = "Provided date cannot be in the future",
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
        
        return new ResponseMessage()
        {
            Message =
                $"Currently marking Streak with ID: {request.StreakId} complete for day: {request.Date.Date}",
            StatusCode = HttpStatusCode.Accepted
        };
    }
}