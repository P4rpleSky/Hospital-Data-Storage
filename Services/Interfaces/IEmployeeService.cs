using Kosta_Task.Models.Dtos;

namespace Kosta_Task.Services.IServices
{
	public interface IEmployeeService
	{
		Task<ResponseDto> GetEmployeesByDepartmentIdAsync(Guid departmentId);
		Task<ResponseDto> GetEmployeeByIdAsync(decimal employeeId);
		Task<ResponseDto> DeleteEmployeeAsync(int employeeId);
		Task<ResponseDto> CreateEmployeeAsync(EmployeeDto employeeDto);
		Task<ResponseDto> UpdateEmployeeAsync(EmployeeDto employeeDto);
	}
}
