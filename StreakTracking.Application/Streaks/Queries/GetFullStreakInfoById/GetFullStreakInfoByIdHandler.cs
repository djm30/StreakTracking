using System.Net;
using MediatR;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Calculated;

namespace StreakTracking.Application.Streaks.Queries.GetFullStreakInfoById;

public class GetFullStreakInfoByIdHandler : IRequestHandler<GetFullStreakInfoByIdQuery, ResponseMessageContent<FullStreakInfo>>
{
    private readonly IStreakReadingService _streakService;

    public GetFullStreakInfoByIdHandler(IStreakReadingService streakService)
    {
        _streakService = streakService ?? throw new ArgumentNullException(nameof(streakService));
    }
    
    public async Task<ResponseMessageContent<FullStreakInfo>> Handle(GetFullStreakInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var fullStreakInfo = await _streakService.GetFullStreakInfoById(request.StreakId);
        if (fullStreakInfo is null)
        {
            return new ResponseMessageContent<FullStreakInfo>()
            {
                Content = null,
                Message = "No Streak found for this ID",
                StatusCode = HttpStatusCode.NotFound
            };
        }
        
        return new ResponseMessageContent<FullStreakInfo>()
        {
            Content = fullStreakInfo,
            Message = "Full Streak Info",
            StatusCode = HttpStatusCode.OK
        };
    }
}