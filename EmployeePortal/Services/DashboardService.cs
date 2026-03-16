using Application.Dto;
using Application.Interfaces.IService;
using DocumentFormat.OpenXml.Spreadsheet;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Services
{
    public class DashboardService : IDashboardService

    {
        private readonly AppDbContext context;
        public DashboardService(AppDbContext context)
        {
            this.context = context;

        }
        public async Task<DashboardDto> GetDashboardAsync()
        {
            var n = DateTime.Now;
            var totalEmployees  = await context.Employees
                .Where(e=> !e.IsDeleted && e.IsActive)
                .CountAsync();

           var newHires = await context.Employees
        .Where(e => !e.IsDeleted &&
                    e.HireDate != null &&
                e.HireDate.Value.Month == n.Month &&
                e.HireDate.Value.Year == n.Year)
        .CountAsync();

            var totalDepartments = await context.Departments.CountAsync();
            return new DashboardDto
            {
                totalEmployee = totalEmployees,
                totalDepartment = totalDepartments,
                newHire = newHires
            };
        }

        public async Task<List<DepartmentDto>> GetDepartmeOverViewAsync()
        {
            var departments = await context.Employees
                .Where(e => !e.IsDeleted && e.IsActive)
                .GroupBy(e => e.Designation.Department)
                .Select(g => new DepartmentDto
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    EmployeeCount = g.Count()
                })
                .ToListAsync();
            return departments;
        }
    }
}
