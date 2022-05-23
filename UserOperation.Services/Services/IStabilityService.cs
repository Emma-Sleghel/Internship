using UserOperation.Services.Dtos;

namespace UserOperation.Services.Services
{
    public interface IStabilityService
    {
        StabilityDto GetStabilityById(int id);
        ICollection<StabilityDto> GetAllStabilities();
        int? CreateStability(StabilityDto stability);
        void UpdateStability(StabilityDto stability);
        bool DeleteStability(int id);
        ICollection<ProjectDto> GetAllProjects();
    }
}
