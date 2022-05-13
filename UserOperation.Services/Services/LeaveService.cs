using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOperation.Data.Entities;
using UserOperation.Services.Dtos;
using UserOperation.Data.Repository;

namespace UserOperation.Services.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IMapper _mapper;

        private IGenericRepository<Leave> _leaveRepository;
        private IGenericRepository<Employee> _employeeRepository;
        private IGenericRepository<Reason> _reasonRepository;

        public LeaveService(IMapper mapper, IGenericRepository<Leave> leaveRepository, IGenericRepository<Employee> employeeRepository, IGenericRepository<Reason> reasonRepository)
        {
            _mapper = mapper;
            _leaveRepository = leaveRepository;
            _employeeRepository = employeeRepository;
            _reasonRepository = reasonRepository;
        }

        public LeaveDto GetLeaveById(int id)
        {
            var leave = _mapper.Map<LeaveDto>(_leaveRepository.GetById(id));
            return leave;
        }

        public ICollection<LeaveDto> GetAllLeaves()
        {
            var leaves = _mapper.Map<List<LeaveDto>>(_leaveRepository.GetAll());
            return leaves;
        }
        public int? CreateLeave(LeaveDto leave)
        {
            var leaveEntity = _leaveRepository.Query(l=>l.Employee.EmployeeID == leave.EmployeeId)
                .FirstOrDefault();
            if (leaveEntity != null)
                return null;
            
            var employee = _employeeRepository.GetById(leave.EmployeeId);     
            var primaryReason = _reasonRepository.GetById(leave.PrimaryReasonId);
            var secondaryReason = _reasonRepository.GetById(leave.SecondaryReasonId);
            var leaveMap = _mapper.Map<Leave>(leave);

            leaveMap.Employee = employee;
            leaveMap.PrimaryReason = primaryReason;
            leaveMap.SecondaryReason = secondaryReason;
           
            _leaveRepository.Create(leaveMap);
            return leaveMap.LeaveID;
        }

        public LeaveDto UpdateLeave(LeaveDto leave)
        {
            var leaveEntity = _leaveRepository.Query(l => l.Employee.EmployeeID == leave.EmployeeId)
                .FirstOrDefault();
            if (leaveEntity == null)
                return null;

            var dbLeave = _leaveRepository.GetById(leave.LeaveID);
            var employee = _employeeRepository.GetById(leave.EmployeeId);
            var primaryReason = _reasonRepository.GetById(leave.PrimaryReasonId);
            var secondaryReason = _reasonRepository.GetById(leave.SecondaryReasonId);
            dbLeave.LeaveMonth = leave.LeaveMonth;
            dbLeave.LeaveYear = leave.LeaveYear;
            dbLeave.ActiveHC = leave.ActiveHC;
            
            dbLeave.Employee = employee;
            dbLeave.PrimaryReason = primaryReason;
            dbLeave.SecondaryReason = secondaryReason;

            _leaveRepository.Update(dbLeave);

            return _mapper.Map<LeaveDto>(dbLeave);
        }


        public bool DeleteLeave(int id)
        {
            
            Leave leave = _leaveRepository.GetById(id);
            if(leave == null)
            {
                return false;
            }
         
            _leaveRepository.Delete(leave);

            return true;
        }
    }
}
