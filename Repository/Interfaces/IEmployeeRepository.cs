﻿using Kosta_Task.Models.Dtos;

namespace Kosta_Task.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentIdAsync(Guid departmentId);
        Task<EmployeeDto> GetEmployeeByIdAsync(decimal employeeId);
		Task<bool> DeleteEmployeeAsync(int employeeId);
		Task<EmployeeDto> CreateUpdateEmployeeAsync(EmployeeDto employeeDto);
	}
}
