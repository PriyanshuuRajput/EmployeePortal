using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IService
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();
        Task<List<DepartmentDto>> GetDepartmeOverViewAsync();
    }
}
