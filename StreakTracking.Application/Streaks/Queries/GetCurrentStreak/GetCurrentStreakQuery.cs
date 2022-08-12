using MediatR;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Calculated;

namespace StreakTracking.Application.Streaks.Queries.GetCurrentStreak;

public class GetCurrentStreakQuery : IRequest<ResponseMessageContent<CurrentStreak>>
{
    public string StreakId { get; set; }
}