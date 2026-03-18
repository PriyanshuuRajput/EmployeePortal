using Application.Dto;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IService
{
    public interface IDesignationService
    {
        Task<PagedResult<DesignationDto>> GetAllDesignationsAsync(int pageNumber, int pageSize,string search ,int? selectedDesignationId, string sortColumn, string sortDirection);
        Task<DesignationDto?> GetDesignationByIdAsync(int id);
        Task AddDesignationAsync(DesignationDto designationDto);
        Task UpdateDesignationAsync(DesignationDto designationDto);
        Task DeleteDesignationAsync(int id);
    }
}
