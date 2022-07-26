using System.Text.Json.Serialization;

namespace StreakTracking.API.Models;

public class AddStreakDTO
{
    [JsonIgnore]
    public Guid StreakId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}