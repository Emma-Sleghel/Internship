using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private IGenericRepository<Project> _projectRepository;
        private readonly IEmployeeService _employeeService;

        public StabilityService(IMapper mapper, IGenericRepository<Stability> stabilityRepository,
            IGenericRepository<Employee> employeeRepository, IGenericRepository<Criticality> criticalityRepository,
            IGenericRepository<StabilityLevel> stabilityLevelRepository, IGenericRepository<Project> projectRepository, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _stabilityRepository = stabilityRepository;
            _employeeRepository = employeeRepository;
            _criticalityRepository = criticalityRepository;
            _stabilityLevelRepository = stabilityLevelRepository;
            _projectRepository = projectRepository;
            _employeeService = employeeService;
        }

        public ICollection<ProjectDto> GetAllProjects()
        {
            var projects = _mapper.Map<List<ProjectDto>>(_employeeRepository.GetAll()
                .Select(pr => pr.Projects));

            return projects;
               
        }
        public StabilityDto GetStabilityById(int id)
        {
            var stability = _mapper.Map<StabilityDto>(_stabilityRepository.GetById(id));
            return stability;
        }

        public ICollection<StabilityDto> GetAllStabilities()
        {
            var stabilities = _mapper.Map<List<StabilityDto>>(_stabilityRepository.Query()
                .Include(x => x.Employee).ThenInclude(x => x.Level)
                .Include(x => x.Employee).ThenInclude(x => x.Position)
                .Include(x => x.Employee).ThenInclude(x => x.Projects)
                .Include(x => x.StabilityLevel)
                .Include(x => x.Criticality)
                );
            return stabilities;
        }
        public int? CreateStability(StabilityDto stability)
        {   
            var stabilityEntity = _stabilityRepository.Query()
                .Include(x => x.Employee)
                .Where(l => l.Employee.EmployeeID == stability.Employee.EmployeeId)
                .FirstOrDefault();

            if (stabilityEntity != null)
            {
                return null;
            }

            var employee = _employeeRepository.GetById(stability.Employee.EmployeeId);
            if(employee == null)
            {
                _employeeService.CreateEmployee(stability.Employee);
                employee = _employeeRepository.GetById(stability.Employee.EmployeeId);
            }

            var criticality = _criticalityRepository.GetById(stability.Criticality.CriticalityID) ;
            var stabilityLevel = _stabilityLevelRepository.GetById(stability.StabilityLevel.StabilityLevelID);
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
            var employee = _employeeRepository.GetById(stability.Employee.EmployeeId);
            var criticality = _criticalityRepository.GetById(stability.Criticality.CriticalityID);
            var stabilityLevel = _stabilityLevelRepository.GetById(stability.StabilityLevel.StabilityLevelID);
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

