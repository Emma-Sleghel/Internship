namespace UserOperation.Web.Models
{
    public class LeaveViewModel
    {
        public int LeaveID { get; set; }
        public  EmployeeViewModel Employee { get; set; }
        public int LeaveMonth { get; set; }
        public int LeaveYear { get; set; }
        public int ActiveHC { get; set; }
       
        public virtual ReasonViewModel PrimaryReason { get; set; }
        public virtual ReasonViewModel SecondaryReason { get; set; }
    }
}
