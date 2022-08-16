using System.Net;
using MediatR;
using StreakTracking.Endpoints.Application.Contracts.Business;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Queries.GetCurrentStreak;

public class GetCurrentStreakHandler : IRequestHandler<GetCurrentStreakQuery, ResponseMessageContent<CurrentStreak>>
{
    private readonly IStreakReadingService _streakService;

    public GetCurrentStreakHandler(IStreakReadingService streakService)
    {
        _streakService = streakService ?? throw new ArgumentNullException(nameof(streakService));
    }
    
    public async Task<ResponseMessageContent<CurrentStreak>> Handle(GetCurrentStreakQuery request, CancellationToken cancellationToken)
    {
        var currentStreak = await _streakService.GetCurrentStreak(request.StreakId);
        if (currentStreak is null)
        {
            return new ResponseMessageContent<CurrentStreak>
            {
                Content = null,
                Message = "No Streak found for this ID",
                StatusCode = HttpStatusCode.NotFound
            };
        }

        return new ResponseMessageContent<CurrentStreak>
        {
            Content = currentStreak,
            Message = "Current streak calculated",
            StatusCode = HttpStatusCode.OK
        };
    }
}