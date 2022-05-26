using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOperation.Services.Dtos;

namespace UserOperation.Services.Services
{
    public interface ILeaveService
    {
        LeaveDto GetLeaveById(int id);
        ICollection<LeaveDto> GetAllLeaves();
        int? CreateLeave(LeaveDto leave);
        LeaveDto UpdateLeave(LeaveDto leave);
        bool DeleteLeave(int id);
        int GetReasonId(string LevelName);
    }
}
