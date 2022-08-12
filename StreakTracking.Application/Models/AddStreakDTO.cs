using System.Text.Json.Serialization;

namespace StreakTracking.Application.Models;

public class AddStreakDTO
{
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
}