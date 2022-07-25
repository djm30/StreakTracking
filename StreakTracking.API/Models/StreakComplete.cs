using System.Text.Json.Serialization;

namespace StreakTracking.API.Models;

public class StreakComplete
{
    [JsonIgnore]
    public Guid StreakId { get; set; }
    public bool Complete { get; set; }
    public DateTime Date { get; set; }
}