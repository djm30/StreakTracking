using MediatR;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Commands.AddStreak;

public class AddStreakCommand : IRequest<ResponseMessage>
{
    public Guid StreakId { get; set; }
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
}