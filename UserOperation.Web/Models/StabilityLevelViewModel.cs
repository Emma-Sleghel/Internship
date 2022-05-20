using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserOperation.Web.Models
{
    public class StabilityLevelViewModel
    {
        [DisplayName("Level of stability")]
        [Required(ErrorMessage = "Please select level of stability")]
        public int? StabilityLevelID { get; set; }
        
        public string? StabilityLevelName { get; set; }
    }
}
