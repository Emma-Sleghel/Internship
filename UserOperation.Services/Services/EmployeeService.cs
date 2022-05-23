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
    }
}
