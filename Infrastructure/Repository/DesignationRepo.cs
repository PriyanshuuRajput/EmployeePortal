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

        public async Task<PagedResult<Designation>> GetAllDesignationsAsync(int pageNumber, int pageSize,string search, int? selectedDesignationId ,string sortColumn, string sortDirection)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = context.Designations
                .Where(x => !x.IsDeleted);

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
                case "designation":
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
            if(selectedDesignationId.HasValue)
            {
                query = query.Where(d => d.Id == selectedDesignationId.Value);
            }
            var totalCount = await query.CountAsync();

            var designations = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Designation>
            {
                Items = designations,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
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
