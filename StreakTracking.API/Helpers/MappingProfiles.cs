using AutoMapper;
using StreakTracking.Events.Events;
using StreakTracking.Models;

namespace StreakTracking.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UpdateStreak, UpdateStreakEvent>();
        CreateMap<StreakComplete, StreakCompleteEvent>();
    }
}