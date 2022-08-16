using AutoMapper;
using StreakTracking.Endpoints.Application.Models;
using StreakTracking.Endpoints.Application.Streaks.Commands.AddStreak;
using StreakTracking.Endpoints.Application.Streaks.Commands.DeleteStreak;
using StreakTracking.Endpoints.Application.Streaks.Commands.StreakComplete;
using StreakTracking.Endpoints.Application.Streaks.Commands.UpdateStreak;
using StreakTracking.Events.Events;

namespace StreakTracking.Endpoints.Application.Helpers;

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