using Application.Dto;
using Application.Interfaces.IRepo;
using Application.Interfaces.IService;
using Domain.Entities;

namespace EmployeePortal.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepo _designationRepo;

        public DesignationService(IDesignationRepo designationRepo)
        {
            _designationRepo = designationRepo;
        }

        public async Task<List<DesignationDto>> GetAllDesignationsAsync()
        {
            var designations = await _designationRepo.GetAllDesignationsAsync();

            return designations.Select(d => new DesignationDto
            {
                Id = d.Id,
                Name = d.Name,
                DepartmentId = d.DepartmentId
            }).ToList();
        }

        public async Task<DesignationDto?> GetDesignationByIdAsync(int id)
        {
            var designation = await _designationRepo.GetDesignationByIdAsync(id);

            if (designation == null)
                return null;

            return new DesignationDto
            {
                Id = designation.Id,
                Name = designation.Name,
                DepartmentId = designation.DepartmentId
            };
        }

        public async Task AddDesignationAsync(DesignationDto designationDto)
        {
            var designation = new Designation
            {
                Name = designationDto.Name,
                DepartmentId = designationDto.DepartmentId
            };

            await _designationRepo.AddDesignationAsync(designation);
        }

        public async Task UpdateDesignationAsync(DesignationDto designationDto)
        {
            var designation = await _designationRepo.GetDesignationByIdAsync(designationDto.Id);

            if (designation == null)
                throw new Exception("Designation not found");

            designation.Name = designationDto.Name;
            designation.DepartmentId = designationDto.DepartmentId;

            await _designationRepo.UpdateDesignationAsync(designation);
        }

        public async Task DeleteDesignationAsync(int id)
        {
            await _designationRepo.DeleteDesignationAsync(id);
        }
    }
}