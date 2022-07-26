using AutoMapper;
using StreakTracking.Events.Events;
using StreakTracking.API.Models;

namespace StreakTracking.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AddStreakDTO, AddStreakEvent>();
        CreateMap<UpdateStreakDTO, UpdateStreakEvent>();
        CreateMap<StreakCompleteDTO, StreakCompleteEvent>();
    }
}