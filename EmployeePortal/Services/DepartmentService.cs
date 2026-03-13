using Application.Dto;
using Application.Interfaces.IRepo;
using Application.Interfaces.IService;
using Domain.Entities;
using Infrastructure.Repository;
using System.Transactions;

namespace EmployeePortal.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _repo;

        public DepartmentService(IDepartmentRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            try
            {
                var departments = await _repo.GetAllDepartmentAsync();

                return departments.Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching departments: {ex.Message}", ex);
            }
        }
        public async Task<DepartmentDto?> GetByDepartmentIdAsync(int id)
        {
            try
            {
                var d = await _repo.GetDepartmentByIdAsync(id);
                if (d == null)
                    return null;

                return new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                };

            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching by id  {ex.Message}", ex);
            }
        }
        public async Task UpdateDepartmentAsync(DepartmentDto department)
        {
            try
            {
                var dept = await _repo.GetDepartmentByIdAsync(department.Id);

                if (dept == null)
                    throw new Exception("Department not found");

                dept.Name = department.Name;

                await _repo.UpdateDepartmentAsync(dept);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating department: {ex.Message}", ex);
            }
        }
        public async Task AddDepartmentAsync(DepartmentDto department)
        {
            try
            {
                var d = new Department
                {
                    Name = department.Name,
                };
                await _repo.AddDepartmentAsync(d);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            try
            {
                var d = await _repo.GetDepartmentByIdAsync(id);
                if (d == null)
                    return;
                await _repo.DeleteDepartmentAsync(d);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deete the department {ex.Message}");
            }
        }

    }
}
