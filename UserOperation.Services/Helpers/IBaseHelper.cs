using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOperation.Services.Dtos;

namespace UserOperation.Services.Helpers
{
    public interface IBaseHelper
    {
        ICollection<LevelDto> GetLevels();
        ICollection<ProjectDto> GetProjects();
        ICollection<StabilityLevelDto> GetStabilityLevels();
        ICollection<CriticalityDto> GetCriticalities();
        ICollection<PositionDto> GetPositions();
        ICollection<ReasonDto> GetReasons();
    }
}
