using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOperation.Services.Dtos
{
    public class LeaveDto
    {
        public int LeaveID { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }
        public ReasonDto PrimaryReason { get; set; }
        public ReasonDto SecondaryReason { get; set; }
        public int LeaveMonth { get; set; }
        public int LeaveYear { get; set; }
        public int ActiveHC { get; set; }
        public int PrimaryReasonId { get; set; }
        public int SecondaryReasonId { get; set; }

    }
}
