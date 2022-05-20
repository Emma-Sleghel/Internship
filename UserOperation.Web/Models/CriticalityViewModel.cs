using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserOperation.Web.Models
{
    public class CriticalityViewModel
    {
        [DisplayName("Critically")]
        [Required(ErrorMessage = "Please select critically")]
        public int? CriticalityID { get; set; }
        public string? CriticalityName { get; set; }
    }
}
