using Application.Interfaces.IService;
using Domain.Common;
using Application.Interfaces.IRepo;
using Application.Dto;
using Domain.Entities;

namespace EmployeePortal.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeService(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<PagedResult<EmployeeDto>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var emp = await _employeeRepo.GetAllEmployeesAsync(pageNumber, pageSize);

                var result = emp.Items.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    EmpCode = e.EmpCode,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    AlternatePhoneNumber = e.AlternatePhoneNumber,
                    Gender = e.Gender,
                    DateOfBirth = e.DateofBirth,
                    HireDate = e.HireDate,
                    DesignationId = e.DesignationId,
                    Salary = e.Salary,
                    Address = e.Address,
                    Image = e.Image

                }).ToList();

                return new PagedResult<EmployeeDto>
                {
                    Items = result,
                    TotalCount = emp.TotalCount,
                    PageNumber = emp.PageNumber,
                    PageSize = emp.PageSize
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }
        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var e = await _employeeRepo.GetEmployeeByIdAsync(id);
                if (e == null)
                {
                    return null;
                }
                return new EmployeeDto
                {
                    Id = e.Id,
                    EmpCode = e.EmpCode,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    AlternatePhoneNumber = e.AlternatePhoneNumber,
                    Gender = e.Gender,
                    DateOfBirth = e.DateofBirth,
                    HireDate = e.HireDate,
                    DesignationId = e.DesignationId,
                    Salary = e.Salary,
                    Address = e.Address,
                    Image = e.Image
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            try
            {
                var e = new Employee
                {
                    EmpCode = employeeDto.EmpCode,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Email = employeeDto.Email,
                    PhoneNumber = employeeDto.PhoneNumber,
                    AlternatePhoneNumber = employeeDto.AlternatePhoneNumber,
                    Gender = employeeDto.Gender,
                    DateofBirth = employeeDto.DateOfBirth,
                    HireDate = employeeDto.HireDate,
                    DesignationId = employeeDto.DesignationId,
                    Salary = employeeDto.Salary,
                    Address = employeeDto.Address,
                    Image = employeeDto.Image
                };
                await _employeeRepo.AddEmployeeAsync(e);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }
        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            try
            {
                var emp = await _employeeRepo.GetEmployeeByIdAsync(employeeDto.Id);
                if (emp == null)
                {
                    throw new Exception("Employee not found");
                }
                var employee = new Employee
                {
                    Id = employeeDto.Id,
                    EmpCode = employeeDto.EmpCode,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Email = employeeDto.Email,
                    PhoneNumber = employeeDto.PhoneNumber,
                    AlternatePhoneNumber = employeeDto.AlternatePhoneNumber,
                    Gender = employeeDto.Gender,
                    DateofBirth = employeeDto.DateOfBirth,
                    HireDate = employeeDto.HireDate,
                    DesignationId = employeeDto.DesignationId,
                    Salary = employeeDto.Salary,
                    Address = employeeDto.Address,
                    Image = employeeDto.Image
                };
                await _employeeRepo.UpdateEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update employee", ex);
            }

        }
        public async Task DeleteEmployeeAsync(int id)
        {

            var emp = await _employeeRepo.GetEmployeeByIdAsync(id);
            if (emp == null) throw new Exception("Employee not found");
            await _employeeRepo.DeleteEmployeeAsync(id);

        }
    }
}
