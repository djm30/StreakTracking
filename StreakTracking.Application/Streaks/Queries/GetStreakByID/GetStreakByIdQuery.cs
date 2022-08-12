using MediatR;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Application.Streaks.Queries.GetStreakByID;

public class GetStreakByIdQuery : IRequest<ResponseMessageContent<Streak>>
{
    public string StreakId { get; set; }
}