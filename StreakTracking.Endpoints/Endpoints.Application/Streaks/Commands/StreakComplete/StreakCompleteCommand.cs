using MediatR;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.Application.Streaks.Commands.StreakComplete;

public class StreakCompleteCommand : IRequest<ResponseMessage>
{
    
    public string stringStreakId { get; set; }
    public Guid StreakId { get; set; }
    public bool Complete { get; set; }
    public DateTime Date { get; set; }
}