using Kosta_Task.Models.Dtos;

namespace Kosta_Task.Services.IServices
{
    public interface IDepartmentService : IBaseService
	{
		Task<ResponseDto> GetDepartmentsAsync();
        Task<ResponseDto> GetDepartmentByIdAsync(Guid departmentId);
        Task<ResponseDto> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task<ResponseDto> UpdateDepartmentAsync(DepartmentDto departmentDto);
        Task<ResponseDto> DeleteDepartmentByIdAsync(Guid departmentId);
    }
}
