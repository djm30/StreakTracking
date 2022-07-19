using System;

namespace StreakTracking.EventHandler.Models;

public class Streak
{
    public Guid StreakId { get; set; }  
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
    public int HighestStreak { get; set; }
    
}