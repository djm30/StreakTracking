using System.Net;
using MediatR;
using StreakTracking.Endpoints.Application.Contracts.Business;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Commands.DeleteStreak;

public class DeleteStreakHandler : IRequestHandler<DeleteStreakCommand, ResponseMessage>
{
    private readonly IEventPublishingService _publishingService;

    public DeleteStreakHandler(IEventPublishingService publishingService)
    {
        _publishingService = publishingService ?? throw new ArgumentNullException(nameof(publishingService));
    }

    public async Task<ResponseMessage> Handle(DeleteStreakCommand request, CancellationToken cancellationToken)
    {
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
        await _publishingService.PublishDeleteStreak(request);
        
        return new ResponseMessage()
        {
            Message = $"Currently deleting Streak with ID: {request.StreakId}",
            StatusCode = HttpStatusCode.Accepted
        };
    }
}