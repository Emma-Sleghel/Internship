using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOperation.Services.Dtos
{
    public class StabilityDto
    {
        public int StabilityId { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }
        public StabilityLevelDto StabilityLevel { get; set; }
        public CriticalityDto Criticality { get; set; }

        public string StabilityMonth { get; set; }
        public int LeavingYear { get; set; }
        public int StabilityLevelID { get; set; }
        public int CriticalityID { get; set; }
    }
}
