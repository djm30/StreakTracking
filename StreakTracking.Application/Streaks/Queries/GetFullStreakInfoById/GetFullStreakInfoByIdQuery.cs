using MediatR;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Calculated;

namespace StreakTracking.Application.Streaks.Queries.GetFullStreakInfoById;

public class GetFullStreakInfoByIdQuery : IRequest<ResponseMessageContent<FullStreakInfo>>
{
    public string StreakId { get; set; }
}