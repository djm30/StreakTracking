using System.Net;
using MediatR;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Application.Streaks.Queries.GetStreaks;

public class GetStreaksHandler : IRequestHandler<GetStreaksQuery, ResponseMessageContent<IEnumerable<Streak>>>
{
    private readonly IStreakReadingService _streakService;

    public GetStreaksHandler(IStreakReadingService streakService)
    {
        _streakService = streakService ?? throw new ArgumentNullException(nameof(streakService));
    }
    
    public async Task<ResponseMessageContent<IEnumerable<Streak>>> Handle(GetStreaksQuery request, CancellationToken cancellationToken)
    {
        var streaks = await _streakService.GetStreaks();
        return new ResponseMessageContent<IEnumerable<Streak>>
        {
            Content = streaks,
            Message = "List of streaks",
            StatusCode = HttpStatusCode.OK
        };
    }
}