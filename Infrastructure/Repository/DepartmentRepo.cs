using Application.Interfaces.IRepo;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly AppDbContext context;
        public DepartmentRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddDepartmentAsync(Department department)
        {
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

        }

        public async Task DeleteDepartmentAsync(Department department)
        {
            context.Departments.Remove(department);
            await context.SaveChangesAsync();
        }

        public async Task<PagedResult<Department>> GetAllDepartmentAsync(int pageNumber, int pageSize , string search, int? selectedDepartment,  string sortColumn , string sortDirection)
        {
            if(pageNumber <= 0) pageNumber = 1;
            if(pageSize <= 0) pageSize = 10 ;
            var query = context.Departments
                                .Where(d=>!d.IsDeleted)
                                .AsQueryable();
            //searching
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d =>
                    d.Name.Contains(search));
            }

            //sorting
            sortColumn = sortColumn?.Trim().ToLower();
            sortDirection = sortDirection?.Trim().ToLower();

            switch (sortColumn)
            {
                case "department":
                    query = sortDirection == "asc"
                        ? query.OrderBy(d => d.Name)
                        : query.OrderByDescending(d => d.Name);
                    break;

                case "status":
                    query = sortDirection == "asc"
                        ? query.OrderBy(d => d.IsActive)
                        : query.OrderByDescending(d => d.IsActive);
                    break;

                default:
                    query = query.OrderByDescending(d => d.Id);
                    break;
            }
            //filter

            if (selectedDepartment.HasValue)
            {
                query = query.Where(d => d.Id == selectedDepartment.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Department>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            var e = await context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            return e;
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            context.Departments.Update(department);
            await context.SaveChangesAsync();
        }
    }
}
