using MediatR;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Queries.GetFullStreaksInfo;

public class GetFullStreaksInfoQuery : IRequest<ResponseMessageContent<IEnumerable<FullStreakInfo>>>
{
    
}