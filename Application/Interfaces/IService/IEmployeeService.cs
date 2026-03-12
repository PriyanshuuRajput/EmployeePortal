using Application.Dto;
using Domain.Common;

namespace Application.Interfaces.IService
{
    public interface IEmployeeService
    {
        public Task<PagedResult<EmployeeDto>> GetAllEmployeesAsync(int pageNumber, int pageSize);

        public Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

        public Task AddEmployeeAsync(EmployeeDto employeeDto);

        public Task UpdateEmployeeAsync(EmployeeDto employeeDto);

        public Task DeleteEmployeeAsync(int id);
    }
}
