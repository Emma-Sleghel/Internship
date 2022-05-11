using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOperation.Data.Entities
{
    public class Stability
    {
        public int StabilityId { get; set; }
        public virtual Employee Employee { get; set; }
        public string StabilityMonth { get; set; }
        public int LeavingYear { get; set; }
        public virtual StabilityLevel StabilityLevel { get; set; }
        public virtual Criticality Criticality { get; set; }

    }
}
