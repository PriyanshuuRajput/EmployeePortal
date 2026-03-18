using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepo
{
    public interface IDesignationRepo
    {
        Task<PagedResult<Designation>> GetAllDesignationsAsync(int pageNumber, int pageSize, string search,int? selectedDesignationId, string sortColumn, string sortDirection);
        Task<Designation?> GetDesignationByIdAsync(int id);
        Task AddDesignationAsync(Designation designation);
        Task UpdateDesignationAsync(Designation designation);
        Task DeleteDesignationAsync(int id);
    }
}
