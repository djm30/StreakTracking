using System.Threading.Tasks;
using StreakTracking.EventHandler.Models;

namespace StreakTracking.Services;

public interface IStreakRemovalService
{
    public Task DeleteStreak(string streakId);
}