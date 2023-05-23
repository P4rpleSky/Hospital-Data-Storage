using Kosta_Task.Models.Dtos;

namespace Kosta_Task.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task<IEnumerable<DepartmentDto>> GetDepartmentsExceptChildrenAsync(Guid parentId);
        Task<DepartmentDto> GetDepartmentByIdAsync(Guid departmentId);
        Task<DepartmentDto> CreateUpdateDepartmentAsync(DepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(Guid departmentId);
    }
}
