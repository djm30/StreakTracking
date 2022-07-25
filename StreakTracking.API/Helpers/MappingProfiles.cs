using AutoMapper;
using StreakTracking.Events.Events;
using StreakTracking.API.Models;

namespace StreakTracking.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UpdateStreak, UpdateStreakEvent>();
        CreateMap<StreakComplete, StreakCompleteEvent>();
        CreateMap<StreakTracking.Domain.Entities.Streak, Streak>().ReverseMap();
        CreateMap<StreakTracking.Domain.Calculated.CurrentStreak, CurrentStreak>().ReverseMap();
    }
}