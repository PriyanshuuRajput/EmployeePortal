using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IService
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();
        Task DeleteDepartmentAsync(int id);
        Task AddDepartmentAsync(DepartmentDto department);
        Task UpdateDepartmentAsync(DepartmentDto department);
        Task<DepartmentDto?> GetByDepartmentIdAsync(int id);
    }
}
