using MediatR;
using StreakTracking.Domain.Entities;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Queries.GetStreakByID;

public class GetStreakByIdQuery : IRequest<ResponseMessageContent<Streak>>
{
    public string StreakId { get; set; }
}