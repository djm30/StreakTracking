using MediatR;
using StreakTracking.Application.Models;

namespace StreakTracking.Application.Streaks.Commands.AddStreak;

public class AddStreakCommand : IRequest<ResponseMessage>
{
    public Guid StreakId { get; set; }
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
}