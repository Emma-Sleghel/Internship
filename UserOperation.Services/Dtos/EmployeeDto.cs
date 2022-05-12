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
        public int PositionId { get; set; }
        public int LevelId { get; set; }
        public List<int> ProjectIds { get; set; }
    }
}
