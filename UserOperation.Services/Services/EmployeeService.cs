using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private IGenericRepository<Employee> _employeeRepository;
        private IGenericRepository<Project> _projectRepository;
        private IGenericRepository<Level> _levelRepository;
        private IGenericRepository<Position> _positionRepository;
        public EmployeeService(IMapper mapper, IGenericRepository<Employee> employeeRepository,
            IGenericRepository<Project> projectRepository, IGenericRepository<Level> levelRepository, IGenericRepository<Position> positionRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;   
            _levelRepository = levelRepository;
            _positionRepository = positionRepository;
        }
        public ICollection<EmployeeDto> GetAllEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetAll());
            return employees;
        }
        public EmployeeDto GetEmployeeById(string id)
        {
            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetById(id));
            return employee;
        }
        public void CreateEmployee(EmployeeDto employee)
        {
            var level = _levelRepository.GetById(employee.Level.LevelId);
            var position = _positionRepository.GetById(employee.Position.PositionId);
            var projects = _projectRepository.Query(x => employee.ProjectsIds.Contains(x.ProjectId)).ToList();
            

            var employeeMap = _mapper.Map<Employee>(employee);
            employeeMap.Level = level;
            employeeMap.Position = position;
            employeeMap.Projects = projects;
            _employeeRepository.Create(employeeMap);       
        }

        public void DeleteEmployee(string id)
        {
            Employee employee = _employeeRepository.GetById(id);
            _employeeRepository.Delete(employee);
        }
        public void UpdateEmployee(EmployeeDto employee)
        {
            var  dbEmployee =  _employeeRepository.Query().Include(x=>x.Projects).Where(x=>x.EmployeeID==employee.EmployeeId).FirstOrDefault();
            var level =_levelRepository.GetById(employee.Level.LevelId);
            var position =_positionRepository.GetById(employee.Position.PositionId);
            var projects =  _projectRepository.Query(x => employee.ProjectsIds.Contains(x.ProjectId)).ToList();
            
            dbEmployee.Level = level;
            dbEmployee.Position = position;
            dbEmployee.Projects.Clear();        
            dbEmployee.Projects = projects;
            dbEmployee.EmployeeName = employee.EmployeeName;
            _employeeRepository.Update(dbEmployee);
            
        }
        public int[] GetProjectsIdsFromString(string projects)
        {
            var projectsNames = projects.Split(',');
            return _projectRepository.Query(x => projectsNames.Contains(x.ProjectName)).Select(x=>x.ProjectId).ToArray();
        }
        public int GetPositionId(string positionName)
        {
            return _positionRepository.Query(x=>x.PositionName==positionName).FirstOrDefault().PositionId;
        }
        public int GetLevelId(string LevelName)
        {
            return _levelRepository.Query(x => x.LevelName == LevelName).FirstOrDefault().LevelId;
        }
        public ProjectDto GetProjectById(int id)
        {
            var projects = _mapper.Map<ProjectDto>(_projectRepository.GetById(id));
            return projects;
        }
        public List<ProjectDto> GetProjectsByIds(int[] ids)
        {
            List<ProjectDto> projects = new List<ProjectDto>();
            foreach(var id in ids)
            {
                projects.Add(GetProjectById(id));

            }
            return projects;
        }
        
    }
}
