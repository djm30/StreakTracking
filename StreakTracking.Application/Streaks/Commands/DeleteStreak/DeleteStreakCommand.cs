using MediatR;
using StreakTracking.Application.Models;

namespace StreakTracking.Application.Streaks.Commands.DeleteStreak;

public class DeleteStreakCommand : IRequest<ResponseMessage>
{
    public string stringStreakId { get; set; }
    public Guid StreakId { get; set; }
}