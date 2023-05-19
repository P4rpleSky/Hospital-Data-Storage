using Kosta_Task.Models.Dtos;

namespace Kosta_Task.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(int userId);
        Task<EmployeeDto> CreateUpdateEmployee(EmployeeDto userDto);
        Task<bool> DeleteEmployee(int employeeId);
    }
}
