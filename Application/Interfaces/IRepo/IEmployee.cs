using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepo
{
    public interface IEmployee
    {
        Task<(List<Employee> Employees, int TotalCount)> GetEmployeesAsync(int pageNumber, int pageSize);

        Task<Employee?> GetEmployeeByIdAsync(int id);

        Task AddEmployeeAsync(Employee employee);

        Task UpdateEmployeeAsync(Employee employee);

        Task DeleteEmployeeAsync(int id);
    }
}
