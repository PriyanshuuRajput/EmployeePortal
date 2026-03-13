using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepo
{
    public interface IDepartmentRepo
    {
        Task<List<Department>> GetAllDepartmentAsync();
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Department department);

    }
}
