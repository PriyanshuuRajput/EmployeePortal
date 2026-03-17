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

        public async Task<PagedResult<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize, string search,int? selectedDepartment,int? selectedDesignation,string sortColumn,string sortDirection)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            List<Employee> employees = new();
            int totalCount = 0;

            try
            {

                var query = _context.Employees
                            .Where(e => !e.IsDeleted && e.IsActive)
                            .AsQueryable();
                //Searching
                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(e =>
                        e.FirstName.Contains(search) ||
                        e.LastName.Contains(search));
                        //e.Email.Contains(search) ||
                        //e.EmpCode.Contains(search));
                }
                //Filters
                if (selectedDepartment.HasValue)
                {
                    query = query.Where(e =>
                        e.Designation.DepartmentId == selectedDepartment.Value);
                }

                if (selectedDesignation.HasValue)
                {
                    query = query.Where(e =>
                        e.DesignationId == selectedDesignation.Value);
                }

                // Sorting
                sortColumn = sortColumn?.Trim().ToLower();
                sortDirection = sortDirection?.Trim().ToLower();
                switch (sortColumn)
                {
                    case "name":
                        query = sortDirection == "asc"
                            ? query.OrderBy(e => e.FirstName)
                            : query.OrderByDescending(e => e.FirstName);
                        break;

                    case "department":
                        query = sortDirection == "asc"
                            ? query.OrderBy(e => e.Designation.Department.Name)
                            : query.OrderByDescending(e => e.Designation.Department.Name);
                        break;

                    case "designation":
                        query = sortDirection == "asc"
                            ? query.OrderBy(e => e.Designation.Name)
                            : query.OrderByDescending(e => e.Designation.Name);
                        break;

                    default:
                        query = query.OrderByDescending(e => e.Id);
                        break;
                }
                totalCount = await query.CountAsync();

                employees = await query
                    .Include(e => e.Designation)
                    .ThenInclude(d=>d.Department)
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
                    .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted && e.IsActive);

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
                .FirstOrDefaultAsync(e => e.Email == email && !e.IsDeleted && e.IsActive);
            return e;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                var minDob = DateTime.Today.AddYears(-60);
                var maxDob = DateTime.Today.AddYears(-18);
                if (employee.DateofBirth < minDob || employee.DateofBirth > maxDob)
                {
                    throw new Exception("Age must be between 18 and 60 years.");
                }
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

                    //_context.Employees.Update(employee);
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
