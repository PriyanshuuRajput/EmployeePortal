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
    public class DesignationRepo : IDesignationRepo
    {
        private readonly AppDbContext context;

        public DesignationRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddDesignationAsync(Designation designation)
        {
            await context.Designations.AddAsync(designation);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDesignationAsync(int id)
        {
            var designation = await context.Designations.FindAsync(id);

            if (designation != null)
            {
                designation.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Designation>> GetAllDesignationsAsync()
        {
            return await context.Designations
             .Where(x => !x.IsDeleted)
             .ToListAsync();
        }

        public async Task<Designation?> GetDesignationByIdAsync(int id)
        {
            return await context.Designations
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task UpdateDesignationAsync(Designation designation)
        {
            context.Designations.Update(designation);
            await context.SaveChangesAsync();
        }
    }
}
