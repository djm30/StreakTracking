using MediatR;
using StreakTracking.Domain.Entities;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Queries.GetStreaks;

public class GetStreaksQuery : IRequest<ResponseMessageContent<IEnumerable<Streak>>>
{
    
}