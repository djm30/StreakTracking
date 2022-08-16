using MediatR;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Queries.GetCurrentStreak;

public class GetCurrentStreakQuery : IRequest<ResponseMessageContent<CurrentStreak>>
{
    public string StreakId { get; set; }
}