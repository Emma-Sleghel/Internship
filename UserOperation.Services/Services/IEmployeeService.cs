﻿
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
        EmployeeDto GetEmployeeById(int id);
        void CreateEmployee(EmployeeDto employee);
        void DeleteEmployee(int id);
        void UpdateEmployee(EmployeeDto employee);
    }
}