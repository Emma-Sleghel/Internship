using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOperation.Data.Entities;
using UserOperation.Services.Dtos;
using UserOperation.Data.Repository;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UserOperation.Services.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IMapper _mapper;

        private IGenericRepository<Leave> _leaveRepository;
        private IGenericRepository<Employee> _employeeRepository;
        private IGenericRepository<Reason> _reasonRepository;
        private readonly IEmployeeService _employeeService;

        public LeaveService(IMapper mapper, IGenericRepository<Leave> leaveRepository, IGenericRepository<Employee> employeeRepository, IGenericRepository<Reason> reasonRepository, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _leaveRepository = leaveRepository;
            _employeeRepository = employeeRepository;
            _reasonRepository = reasonRepository;
            _employeeService = employeeService;
        }

        public LeaveDto GetLeaveById(int id)
        {
            
            var leave = _mapper.Map<LeaveDto>(_leaveRepository.Query().Where(x => x.LeaveID == id)
                .Include(x => x.Employee).ThenInclude(x => x.Level)
                .Include(x => x.Employee).ThenInclude(x => x.Position)
                .Include(x => x.Employee).ThenInclude(x => x.Projects)
                .Include(x=> x.PrimaryReason)
                .Include(x=>x.SecondaryReason).FirstOrDefault());
            return leave;
        }

        public ICollection<LeaveDto> GetAllLeaves()
        {
            var leaves = _mapper.Map<List<LeaveDto>>(_leaveRepository.Query().Include(x => x.Employee).ThenInclude(x => x.Projects)
                .Include(x => x.Employee).ThenInclude(x => x.Level)
                .Include(x => x.Employee).ThenInclude(x => x.Position)
                .Include(x => x.PrimaryReason)
                .Include(x => x.SecondaryReason)
                );

            return leaves;
        }
        public int? CreateLeave(LeaveDto leave)
        {
            var leaveEntity = _leaveRepository.Query().Include(e => e.Employee).Where(l => l.Employee.EmployeeID == leave.Employee.EmployeeId)
                .FirstOrDefault();

            if (leaveEntity != null)
                return null;

            var employee = _employeeRepository.GetById(leave.Employee.EmployeeId);
            if (employee == null)
            {
                _employeeService.CreateEmployee(leave.Employee);
                employee = _employeeRepository.GetById(leave.Employee.EmployeeId);
            }
            var primaryReason = _reasonRepository.GetById(leave.PrimaryReason.ReasonId);
            var secondaryReason = _reasonRepository.GetById(leave.SecondaryReason.ReasonId);
            var leaveMap = _mapper.Map<Leave>(leave);

            leaveMap.Employee = employee;
            leaveMap.PrimaryReason = primaryReason;
            leaveMap.SecondaryReason = secondaryReason;

            _leaveRepository.Create(leaveMap);
            return leaveMap.LeaveID;
        }

        public LeaveDto UpdateLeave(LeaveDto leave)
        {
            var dbLeave = _leaveRepository.GetById(leave.LeaveID);
            var employee = _employeeRepository.GetById(leave.Employee.EmployeeId);
            var primaryReason = _reasonRepository.GetById(leave.PrimaryReason.ReasonId);
            var secondaryReason = _reasonRepository.GetById(leave.SecondaryReason.ReasonId);
            dbLeave.LeaveMonth = leave.LeaveMonth;
            dbLeave.LeaveYear = leave.LeaveYear;
            dbLeave.ActiveHC = leave.ActiveHC;
            if (employee == null)
            {
                _employeeService.CreateEmployee(leave.Employee);
            }
            else
            {
                _employeeService.UpdateEmployee(leave.Employee);
            }
            employee = _employeeRepository.GetById(leave.Employee.EmployeeId);
            dbLeave.Employee = employee;
            dbLeave.PrimaryReason = primaryReason;
            dbLeave.SecondaryReason = secondaryReason;

            _leaveRepository.Update(dbLeave);

            return _mapper.Map<LeaveDto>(dbLeave);
        }


        public bool DeleteLeave(int id)
        {

            Leave leave = _leaveRepository.GetById(id);
            if (leave == null)
            {
                return false;
            }

            _leaveRepository.Delete(leave);

            return true;
        }
        public int GetReasonId(string LevelName)
        {
            return _reasonRepository.Query(x => x.ReasonName == LevelName).FirstOrDefault().ReasonId;
        }
    }
}
