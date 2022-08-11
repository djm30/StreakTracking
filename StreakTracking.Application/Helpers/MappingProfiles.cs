using AutoMapper;
using StreakTracking.Events.Events;
using StreakTracking.Application.Models;
using StreakTracking.Application.Streaks.Commands.AddStreak;
using StreakTracking.Application.Streaks.Commands.DeleteStreak;
using StreakTracking.Application.Streaks.Commands.StreakComplete;
using StreakTracking.Application.Streaks.Commands.UpdateStreak;

namespace StreakTracking.Application.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Mapping between DTOs and Commands
        CreateMap<AddStreakDTO, AddStreakCommand>();
        CreateMap<UpdateStreakDTO, UpdateStreakCommand>();
        CreateMap<StreakCompleteDTO, StreakCompleteCommand>();
        
        // Mapping between Commands and Events
        CreateMap<StreakCompleteCommand, StreakCompleteEvent>();
        CreateMap<UpdateStreakCommand, UpdateStreakCommand>();
        CreateMap<StreakCompleteCommand, StreakCompleteEvent>();
        CreateMap<DeleteStreakCommand, DeleteStreakEvent>();
    }
}