using AutoMapper;
using UserOperation.Data.Entities;
using UserOperation.Services.Dtos;

namespace UserOperation.Web.MappingProfiles
{
    public class MappingProfiles : Profile

    {

        public MappingProfiles()
        {
            CreateMap<Criticality, CriticalityDto>();
            CreateMap<CriticalityDto, Criticality>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Leave, LeaveDto>();
            CreateMap<LeaveDto, Leave>();

            CreateMap<Level, LevelDto>();
            CreateMap<LevelDto, Level>();

            CreateMap<Position, PositionDto>();
            CreateMap<PositionDto, Position>();

            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();

            CreateMap<Reason, ReasonDto>();
            CreateMap<ReasonDto, Reason>();

            CreateMap<Stability, StabilityDto>();
            CreateMap<StabilityDto, Stability>();

            CreateMap<StabilityLevel, StabilityLevelDto>();
            CreateMap<StabilityLevelDto, StabilityLevel>();






        }

    }
}


