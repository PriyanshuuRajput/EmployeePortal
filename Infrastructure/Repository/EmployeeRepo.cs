using Application.Interfaces.IRepo;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _context;

        public EmployeeRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            List<Employee> employees = new();
            int totalCount = 0;

            try
            {
                var query = _context.Employees.AsQueryable();

                totalCount = await query.CountAsync();

                employees = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching employees: {ex.Message}");
            }

            return new PagedResult<Employee>
            {
                Items = employees,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = _context.Employees.FindAsync(id);
                return await employee;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching employee by ID: " + ex.Message);
                return null;
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                var employees = _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the employee" + ex.Message);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var existingEmployee = _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating the employee: " + ex.Message);

            }
        }
        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee != null)
                {
                    employee.IsDeleted = true;
                    employee.IsActive = false;

                    _context.Employees.Update(employee);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the employee: {ex.Message}");
            }
        }

        
    }
}
