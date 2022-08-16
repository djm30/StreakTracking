using MediatR;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Commands.DeleteStreak;

public class DeleteStreakCommand : IRequest<ResponseMessage>
{
    public string stringStreakId { get; set; }
    public Guid StreakId { get; set; }
}