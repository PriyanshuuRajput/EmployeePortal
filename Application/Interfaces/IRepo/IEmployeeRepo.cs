using Domain.Common;
using Domain.Entities;


namespace Application.Interfaces.IRepo
{
    public interface IEmployeeRepo
    {
        Task<PagedResult<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize);

        Task<Employee?> GetEmployeeByIdAsync(int id);

        Task AddEmployeeAsync(Employee employee);

        Task UpdateEmployeeAsync(Employee employee);

        Task DeleteEmployeeAsync(int id);
    }
}
