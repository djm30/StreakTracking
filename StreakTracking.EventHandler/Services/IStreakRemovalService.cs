using System.Threading.Tasks;
using StreakTracking.EventHandler.Models;

namespace StreakTracking.Services;

public interface IStreakDayService
{
    public Task DeleteStreak(string streakId);
}