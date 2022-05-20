using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserOperation.Web.Models
{
    public class PositionViewModel
    {
        [DisplayName("Position name")]
        [Required(ErrorMessage = "Please select position name")]
        public int? PositionId { get; set; }
        public string? PositionName { get; set; }
    }
}
