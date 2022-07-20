using System;

namespace StreakTracking.EventHandler.Models;

public class StreakDay
{
    public DateTime Id { get; set; }
    public Guid StreakId { get; set; }
    public bool Complete { get; set; }
}