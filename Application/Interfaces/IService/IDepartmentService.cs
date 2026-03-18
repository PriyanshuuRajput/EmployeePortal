using Application.Dto;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IService
{
    public interface IDepartmentService
    {
        Task<PagedResult<DepartmentDto>> GetAllAsync(int pageNumber, int pageSize, string search, int? selectedDepartment, string sortColumn, string sortDirection);
        Task DeleteDepartmentAsync(int id);
        Task AddDepartmentAsync(DepartmentDto department);
        Task UpdateDepartmentAsync(DepartmentDto department);
        Task<DepartmentDto?> GetByDepartmentIdAsync(int id);
    }
}
