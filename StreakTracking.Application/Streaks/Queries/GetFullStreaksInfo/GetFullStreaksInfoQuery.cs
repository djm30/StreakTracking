using MediatR;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Calculated;

namespace StreakTracking.Application.Streaks.Queries.GetFullStreaksInfo;

public class GetFullStreaksInfoQuery : IRequest<ResponseMessageContent<IEnumerable<FullStreakInfo>>>
{
    
}