using MediatR;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Application.Streaks.Queries.GetStreaks;

public class GetStreaksQuery : IRequest<ResponseMessageContent<IEnumerable<Streak>>>
{
    
}