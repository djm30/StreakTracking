using System.Net;
using MediatR;
using StreakTracking.Domain.Entities;
using StreakTracking.Endpoints.Application.Contracts.Business;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Queries.GetStreakByID;

public class GetStreakByIdHandler : IRequestHandler<GetStreakByIdQuery, ResponseMessageContent<Streak>>
{
    private readonly IStreakReadingService _streakService;

    public GetStreakByIdHandler(IStreakReadingService streakService)
    {
        _streakService = streakService ?? throw new ArgumentNullException(nameof(streakService));
    }

    public async Task<ResponseMessageContent<Streak>> Handle(GetStreakByIdQuery request, CancellationToken cancellationToken)
    {
        var streak = await _streakService.GetStreakById(request.StreakId);
        if (streak is null)
        {
            return new ResponseMessageContent<Streak>
            {
                Content = null,
                Message = "No Streak found for this ID",
                StatusCode = HttpStatusCode.NotFound
            };
        }

        return new ResponseMessageContent<Streak>
        {
            Content = streak,
            Message = "Content Found",
            StatusCode = HttpStatusCode.OK
        };
    }
}