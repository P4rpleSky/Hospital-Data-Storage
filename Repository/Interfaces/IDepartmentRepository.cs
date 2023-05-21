using Kosta_Task.Models.Dtos;

namespace Kosta_Task.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        //Task<DepartmentDto> GetEmployeeById(int userId);
        //Task<DepartmentDto> CreateUpdateEmployee(DepartmentDto userDto);
        //Task<bool> DeleteEmployee(int employeeId);
    }
}
