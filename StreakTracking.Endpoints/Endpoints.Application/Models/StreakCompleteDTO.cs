using System.Text.Json.Serialization;

namespace StreakTracking.Endpoints.Application.Models;

public class StreakCompleteDTO
{
    [JsonIgnore]
    public Guid StreakId { get; set; }
    public bool Complete { get; set; }
    public DateTime Date { get; set; }
}