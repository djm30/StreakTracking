using StreakTracking.Domain.Entities;

namespace StreakTracking.Domain.Calculated;

public class FullStreakInfo
{
    public Guid StreakId { get; set; }  
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
    public int LongestStreak { get; set; }
    public int CurrentStreak { get; set; }
    public IEnumerable<StreakDay> Completions { get; set; }
}