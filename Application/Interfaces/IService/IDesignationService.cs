using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IService
{
    public interface IDesignationService
    {
        Task<List<DesignationDto>> GetAllDesignationsAsync();
        Task<DesignationDto?> GetDesignationByIdAsync(int id);
        Task AddDesignationAsync(DesignationDto designationDto);
        Task UpdateDesignationAsync(DesignationDto designationDto);
        Task DeleteDesignationAsync(int id);
    }
}
