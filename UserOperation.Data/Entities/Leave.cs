using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOperation.Data.Entities
{
    public class Leave
    {
        public int LeaveID { get; set; }
        public virtual Employee Employee { get; set; }
        public int LeaveMonth { get; set; }
        public int LeaveYear { get; set; }
        public int ActiveHC { get; set; }
        public int PrimaryReasonId { get; set; }
        public int SecondaryReasonId { get; set; }

        [ForeignKey("PrimaryReasonId")]
        public virtual Reason PrimaryReason { get; set; }

        [ForeignKey("SecondaryReasonId")]
        public virtual Reason SecondaryReason { get; set; }


    }
}
