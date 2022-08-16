using MediatR;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Queries.GetFullStreakInfoById;

public class GetFullStreakInfoByIdQuery : IRequest<ResponseMessageContent<FullStreakInfo>>
{
    public string StreakId { get; set; }
}