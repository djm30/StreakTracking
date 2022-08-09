using AutoMapper;
using StreakTracking.Events.Events;
using StreakTracking.API.Models;
using StreakTracking.Application.Models;

namespace StreakTracking.Application.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AddStreakDTO, AddStreakEvent>();
        CreateMap<UpdateStreakDTO, UpdateStreakEvent>();
        CreateMap<StreakCompleteDTO, StreakCompleteEvent>();
    }
}