namespace UserOperation.Web.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public  PositionViewModel Position { get; set; }
        public LevelViewModel Level { get; set; }
        public List<int> ProjectId { get; set; }
    }
}
