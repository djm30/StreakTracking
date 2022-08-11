using MediatR;
using StreakTracking.Application.Models;

namespace StreakTracking.Application.Streaks.Commands.StreakComplete;

public class StreakCompleteCommand : IRequest<ResponseMessage>
{
    
    public string stringStreakId { get; set; }
    public Guid StreakId { get; set; }
    public bool Complete { get; set; }
    public DateTime Date { get; set; }
}