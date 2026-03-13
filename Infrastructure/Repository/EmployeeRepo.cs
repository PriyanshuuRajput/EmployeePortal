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

        public async Task<PagedResult<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize, string search)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            List<Employee> employees = new();
            int totalCount = 0;

            try
            {

                var query = _context.Employees.AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(e =>
                        e.FirstName.Contains(search) ||
                        e.LastName.Contains(search) ||
                        e.Email.Contains(search) ||
                        e.EmpCode.Contains(search));
                }
                totalCount = await query.CountAsync();

                employees = await query
                    .Include(e => e.Designation)
                    .ThenInclude(d=>d.Department)
                    .OrderBy(e => e.Id)
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
                var employee = await _context.Employees
                    .Include(e => e.Designation)
                    .ThenInclude(d => d.Department)
                    .FirstOrDefaultAsync(e => e.Id == id);

                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching employee by ID: " + ex.Message);
                return null;
            }
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
                var e= await _context.Employees
                    .FirstOrDefaultAsync(e => e.Email == email);
                return e;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
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
                var existingEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Id == employee.Id);

                if (existingEmployee == null)
                    return;
                existingEmployee.Title = employee.Title;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.AlternatePhoneNumber = employee.AlternatePhoneNumber;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.DateofBirth = employee.DateofBirth;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.DesignationId = employee.DesignationId;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.Address = employee.Address;
                existingEmployee.Image = employee.Image;

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
