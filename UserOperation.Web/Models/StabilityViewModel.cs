using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserOperation.Web.Models
{
    public class StabilityViewModel
    {
        public int StabilityId { get; set; }
        [DisplayName("Stability month")]
        [Required(ErrorMessage = "Please select stability month")]
        public string StabilityMonth { get; set; }
        [DisplayName("Leaving year")]
        [Required(ErrorMessage = "Please select leaving year")]
        public int? LeavingYear { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public StabilityLevelViewModel? StabilityLevel { get; set; }
        public CriticalityViewModel? Criticality { get; set; }
    }
}
