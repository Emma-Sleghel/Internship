using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOperation.Data.Entities;
using UserOperation.Data.Repository;
using UserOperation.Services.Dtos;

namespace UserOperation.Services.Services
{
    public class StabilityService : IStabilityService
    {
        private readonly IMapper _mapper;

        private IGenericRepository<Stability> _stabilityRepository;
        private IGenericRepository<Employee> _employeeRepository;
        private IGenericRepository<Criticality> _criticalityRepository;
        private IGenericRepository<StabilityLevel> _stabilityLevelRepository;



        public StabilityService(IMapper mapper, IGenericRepository<Stability> stabilityRepository, IGenericRepository<Employee> employeeRepository, IGenericRepository<Criticality> criticalityRepository, IGenericRepository<StabilityLevel> stabilityLevelRepository)
        {
            _mapper = mapper;
            _stabilityRepository = stabilityRepository;
            _employeeRepository = employeeRepository;
            _criticalityRepository = criticalityRepository;
            _stabilityLevelRepository = stabilityLevelRepository;
        }

        public StabilityDto GetStabilityById(int id)
        {
            var stability = _mapper.Map<StabilityDto>(_stabilityRepository.GetById(id));
            return stability;
        }

        public ICollection<StabilityDto> GetAllStabilities()
        {
            var stabilities = _mapper.Map<List<StabilityDto>>(_stabilityRepository.GetAll());
            return stabilities;
        }
        public int? CreateStability(StabilityDto stability)
        {
            var stabilityEntity = _stabilityRepository.Query(l => l.Employee.EmployeeID == stability.EmployeeId)
                
             .FirstOrDefault();
            if (stabilityEntity != null)
            {
                return null;
            }

            var employee = _employeeRepository.GetById(stability.EmployeeId);
            var criticality = _criticalityRepository.GetById(stability.CriticalityID);
            var stabilityLevel = _stabilityLevelRepository.GetById(stability.StabilityLevelID);
            var stabilityMap = _mapper.Map<Stability>(stability);

            stabilityMap.Employee = employee;
            stabilityMap.Criticality = criticality;
            stabilityMap.StabilityLevel = stabilityLevel;

            _stabilityRepository.Create(stabilityMap);
            return stabilityMap.StabilityId;
        }

        public void UpdateStability(StabilityDto stability)
        {
            var dbStability = _stabilityRepository.GetById(stability.StabilityId);
            var employee = _employeeRepository.GetById(stability.EmployeeId);
            var criticality = _criticalityRepository.GetById(stability.CriticalityID);
            var stabilityLevel = _stabilityLevelRepository.GetById(stability.StabilityLevelID);
            dbStability.StabilityMonth = stability.StabilityMonth;
            dbStability.LeavingYear = stability.LeavingYear;
           

            dbStability.Employee = employee;
            dbStability.Criticality = criticality;
            dbStability.StabilityLevel = stabilityLevel;

            _stabilityRepository.Update(dbStability);
        }

        public bool DeleteStability(int id)
        {

            Stability stability = _stabilityRepository.GetById(id);
            if (stability == null)
            {
                return false;
            }

            _stabilityRepository.Delete(stability);

            return true;
        }
    }





}

