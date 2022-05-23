using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserOperation.Web.Models
{
    public class LeaveViewModel
    {
        public int LeaveID { get; set; }
        public  EmployeeViewModel Employee { get; set; }

        [DisplayName("Leave month")]
        [Required(ErrorMessage = "Please select leave month")]
        public string LeaveMonth { get; set; }

        [DisplayName("Leave year")]
        [Required(ErrorMessage = "Please select leave year")]
        public int LeaveYear { get; set; }


        [DisplayName("Active HC")]
        [Required(ErrorMessage = "Please add Active HC")]
        public int ActiveHC { get; set; }

        [DisplayName("Primary reason")]
        [Required(ErrorMessage = "Please select primary reason")]
        public ReasonViewModel PrimaryReason { get; set; }

        [DisplayName("Secondary reason")]
        [Required(ErrorMessage = "Please select secondary reason")]
        public ReasonViewModel SecondaryReason { get; set; }
    }
}
