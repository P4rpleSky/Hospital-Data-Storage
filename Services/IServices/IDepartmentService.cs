using Kosta_Task.Models.Dtos;

namespace Kosta_Task.Services.IServices
{
	public interface IDepartmentService : IBaseService
	{
		Task<ResponseDto> GetDepartmentsAsync();
	}
}
