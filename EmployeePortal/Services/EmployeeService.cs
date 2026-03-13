using Application.Dto;
using Application.Interfaces.IRepo;
using Application.Interfaces.IService;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Repository;

namespace EmployeePortal.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IDepartmentRepo _departmentRepo;
        private readonly IDesignationRepo _designationRepo;

        public EmployeeService(IEmployeeRepo employeeRepo, IDepartmentRepo departmentRepo, IDesignationRepo designationRepo)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _designationRepo = designationRepo;
        }

        public async Task<PagedResult<EmployeeDto>> GetAllEmployeesAsync(int pageNumber, int pageSize,string search)
        {
            try
            {
                var emp = await _employeeRepo.GetAllEmployeesAsync(pageNumber, pageSize,search);

                var result = emp.Items.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    EmpCode = e.EmpCode,
                    Title = e.Title,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    AlternatePhoneNumber = e.AlternatePhoneNumber,
                    Gender = e.Gender,
                    DateOfBirth = e.DateofBirth,
                    HireDate = e.HireDate,
                    Salary = e.Salary,
                    Address = e.Address,
                    Image = e.Image,
                    Designation = e.Designation == null ? null : new DesignationDto
                    {
                        Id = e.Designation.Id,
                        Name = e.Designation.Name,
                        DepartmentId = e.Designation.DepartmentId
                    },

                    Department = e.Designation?.Department == null ? null : new DepartmentDto
                    {
                        Id = e.Designation.Department.Id,
                        Name = e.Designation.Department.Name
                    }

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
                    Title = e.Title,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    AlternatePhoneNumber = e.AlternatePhoneNumber,
                    Gender = e.Gender,
                    DateOfBirth = e.DateofBirth,
                    HireDate = e.HireDate,
                    Salary = e.Salary,
                    Address = e.Address,
                    Image = e.Image,
                    Designation = e.Designation == null ? null : new DesignationDto
                    {
                        Id = e.Designation.Id,
                        Name = e.Designation.Name
                    },

                    Department = e.Designation?.Department == null ? null : new DepartmentDto
                    {
                        Id = e.Designation.Department.Id,
                        Name = e.Designation.Department.Name
                    }
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
                var existingEmployee = await _employeeRepo.GetEmployeeByEmailAsync(employeeDto.Email);

                if (existingEmployee != null)
                {
                    throw new Exception("Email already exists");
                }
                var e = new Employee
                {
                    Title = employeeDto.Title,
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
                var designation = await _designationRepo.GetDesignationByIdAsync(employeeDto.DesignationId);
                if (designation != null)
                {
                    var dept = await _departmentRepo.GetDepartmentByIdAsync(designation.DepartmentId);

                    if (dept != null)
                    {
                        e.EmpCode = EmpCodeMethod(dept.Name, e.Id);

                        await _employeeRepo.UpdateEmployeeAsync(e);
                    }
                }
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
                    Title = employeeDto.Title,
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

        //Empcode method 
        private string EmpCodeMethod(string deptName, int id)
        {
            var y = DateTime.Now.Year.ToString().Substring(2);

            string deptcode = "";
            if (!string.IsNullOrEmpty(deptName))
            {
                deptcode += deptName[0];
                if (deptName.Length > 2)
                {
                    deptcode += deptName[deptName.Length / 2];
                }
                if (deptName.Length > 1)
                {
                    deptcode += deptName[^1];
                }
                deptcode = deptcode.ToUpper();

            }
            return $"{y}{deptcode}{id}";
        }
    }
}
