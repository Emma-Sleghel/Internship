using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOperation.Services.Dtos
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public PositionDto Position { get; set; }
        public LevelDto Level { get; set; }
        public IEnumerable<ProjectDto> Projects { get; set; }
        public int[] ProjectIds { get; set; }
       
    }
}
