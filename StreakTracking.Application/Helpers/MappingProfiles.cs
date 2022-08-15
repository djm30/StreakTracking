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
        CreateMap<AddStreakCommand, AddStreakEvent>();
        CreateMap<UpdateStreakCommand, UpdateStreakEvent>();
        CreateMap<StreakCompleteCommand, StreakCompleteEvent>();
        CreateMap<DeleteStreakCommand, DeleteStreakEvent>();
    }
}