using Application.Dto;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces.IService
{
    public interface IEmployeeService
    {
        public Task<PagedResult<EmployeeDto>> GetAllEmployeesAsync(int pageNumber, int pageSize,string search);

        public Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

        public Task AddEmployeeAsync(EmployeeDto employeeDto);

        public Task UpdateEmployeeAsync(EmployeeDto employeeDto);

        public Task DeleteEmployeeAsync(int id);
    }
}
