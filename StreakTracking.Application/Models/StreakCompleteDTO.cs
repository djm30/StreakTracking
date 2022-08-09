using System.Text.Json.Serialization;

namespace StreakTracking.Application.Models;

public class StreakCompleteDTO
{
    [JsonIgnore]
    public Guid StreakId { get; set; }
    public bool Complete { get; set; }
    public DateTime Date { get; set; }
}