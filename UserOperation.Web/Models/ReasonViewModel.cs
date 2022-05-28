using System.ComponentModel.DataAnnotations;

namespace UserOperation.Web.Models
{
    public class ReasonViewModel
    {
        
        [Required(ErrorMessage = "Please select a reason")]
        public int? ReasonId { get; set; }
        public string? ReasonName { get; set; }
    }
}
