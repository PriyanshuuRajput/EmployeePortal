using Application.Interfaces.IRepo;
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

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            var d=  await context.Departments.ToListAsync();
            return d;
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
