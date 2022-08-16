using System.Net;
using MediatR;
using StreakTracking.Endpoints.Application.Contracts.Business;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Queries.GetFullStreaksInfo;

public class GetFullStreaksInfoHandler : IRequestHandler<GetFullStreaksInfoQuery, ResponseMessageContent<IEnumerable<FullStreakInfo>>>
{
    private readonly IStreakReadingService _streakService;

    public GetFullStreaksInfoHandler(IStreakReadingService streakService)
    {
        _streakService = streakService ?? throw new ArgumentNullException(nameof(streakService));
    }


    public async Task<ResponseMessageContent<IEnumerable<FullStreakInfo>>> Handle(GetFullStreaksInfoQuery request, CancellationToken cancellationToken)
    {
        var listOfStreakInfo = await _streakService.GetFullStreakInfo();
        return new ResponseMessageContent<IEnumerable<FullStreakInfo>>()
        {
            Content = listOfStreakInfo,
            Message = "Full Info of all Streaks",
            StatusCode = HttpStatusCode.OK
        };
    }
}