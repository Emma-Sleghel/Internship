namespace UserOperation.Services.Dtos
{
    public class StabilityDto
    {
        public int StabilityId { get; set; }
        public string EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }
        public StabilityLevelDto StabilityLevel { get; set; }
        public CriticalityDto Criticality { get; set; }
        public string StabilityMonth { get; set; }
        public int LeavingYear { get; set; }
       
    }
}
