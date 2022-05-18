using AutoMapper;
using UserOperation.Data.Entities;
using UserOperation.Services.Dtos;
using UserOperation.Web.Models;

namespace UserOperation.Web.MappingProfiles
{
    public class MappingProfiles : Profile

    {

        public MappingProfiles()
        {
            CreateMap<Criticality, CriticalityDto>();
            CreateMap<CriticalityDto, Criticality>();
            CreateMap<CriticalityDto, CriticalityViewModel>();
            CreateMap<CriticalityViewModel, CriticalityDto>();



            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<EmployeeDto, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, EmployeeDto>();



            CreateMap<Leave, LeaveDto>();
            CreateMap<LeaveDto, Leave>();
            CreateMap<LeaveDto, LeaveViewModel>();
            CreateMap<LeaveViewModel, LeaveDto>();




            CreateMap<Level, LevelDto>();
            CreateMap<LevelDto, Level>();
            CreateMap<LevelDto, LevelViewModel>();
            CreateMap<LevelViewModel, LevelDto>();



            CreateMap<Position, PositionDto>();
            CreateMap<PositionDto, Position>();
            CreateMap<PositionDto, PositionViewModel>();
            CreateMap<PositionViewModel, PositionDto>();



            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
            CreateMap<ProjectDto, ProjectViewModel>();
            CreateMap<ProjectViewModel, ProjectDto>();



            CreateMap<Reason, ReasonDto>();
            CreateMap<ReasonDto, Reason>();
            CreateMap<ReasonDto, ReasonViewModel>();
            CreateMap<ReasonViewModel, ReasonDto>();



            CreateMap<Stability, StabilityDto>();
            CreateMap<StabilityDto, Stability>();
            CreateMap<StabilityDto, StabilityViewModel>();
            CreateMap<StabilityViewModel, StabilityDto>();



            CreateMap<StabilityLevel, StabilityLevelDto>();
            CreateMap<StabilityLevelDto, StabilityLevel>();
            CreateMap<StabilityLevelDto, StabilityLevelViewModel>();
            CreateMap<StabilityLevelViewModel, StabilityLevelDto>();





        }

    }
}


