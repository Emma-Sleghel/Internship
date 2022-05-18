namespace UserOperation.Web.Models
{
    public class StabilityViewModel
    {
        public int StabilityId { get; set; }
        public string StabilityMonth { get; set; }
        public int LeavingYear { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public StabilityLevelViewModel? StabilityLevel { get; set; }
        public CriticalityViewModel? Criticality { get; set; }
    }
}
