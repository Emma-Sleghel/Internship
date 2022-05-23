using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserOperation.Web.Models
{
    public class LevelViewModel
    {
        [DisplayName("Employee level")]
        [Required(ErrorMessage = "Please select employee level")]
        public int? LevelId { get; set; }
        public string? LevelName { get; set; }
        
    }
}
