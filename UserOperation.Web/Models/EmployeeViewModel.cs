using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserOperation.Web.Models
{
    public class EmployeeViewModel
    {
        [DisplayName("Personnel No.")]
        [Required(ErrorMessage = "Please enter personnel no.")]
        public string EmployeeId { get; set; }
        [DisplayName("Employee name")]
        [Required(ErrorMessage = "Please enter employee name")]
        public string EmployeeName { get; set; }
        public  PositionViewModel? Position { get; set; }
        public LevelViewModel? Level { get; set; }
        public IEnumerable<ProjectViewModel>? Projects { get; set; }
        [DisplayName("Projects")]
        [Required(ErrorMessage = "Please select projects")]
        public int[] ProjectsIds { get; set; }          
        
    }
}
