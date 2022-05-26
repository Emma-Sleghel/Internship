
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOperation.Data.Entities;
using UserOperation.Services.Dtos;

namespace UserOperation.Services.Services
{
    public interface IEmployeeService
    {
        ICollection<EmployeeDto> GetAllEmployees();
        EmployeeDto GetEmployeeById(string id);
        void CreateEmployee(EmployeeDto employee);
        void DeleteEmployee(string id);
        void UpdateEmployee(EmployeeDto employee);
        int[] GetProjectsIdsFromString(string projects);
        int GetLevelId(string LevelName);
        int GetPositionId(string LevelName);
        ProjectDto GetProjectById(int id);
        List<ProjectDto> GetProjectsByIds(int[] ids);
    }
}
