using MediatR;
using StreakTracking.Application.Models;

namespace StreakTracking.Application.Streaks.Commands.UpdateStreak;

public class UpdateStreakCommand : IRequest<ResponseMessage>
{
    public Guid StreakId { get; set; }
    
    public string stringStreakId { get; set; }
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
    public int LongestStreak { get; set; }
}