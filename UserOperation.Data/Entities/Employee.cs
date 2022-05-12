using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOperation.Data.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public virtual Position Position { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }
}
