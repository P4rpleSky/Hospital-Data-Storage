using AutoMapper;
using Kosta_Task.DbContexts;
using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kosta_Task.Repository
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly TestDbContext _db;
		private readonly IMapper _mapper;

		public EmployeeRepository(TestDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<EmployeeDto> CreateUpdateEmployeeAsync(EmployeeDto employeeDto)
		{
			if (employeeDto.DateOfBirth > DateTime.Now)
			{
				throw new InvalidOperationException("Дата рождения не может быть позднее текущей!");
			}

            var employee = _mapper.Map<EmployeeDto, Employee>(employeeDto);
			if (employee.Id > 0)
			{
				_db.Employees.Update(employee);
			}
			else
			{
				//if (_db.Employees.Any(x => 
				//	x.DepartmentId == employeeDto.DepartmentId &&
				//	x.DocNumber == employeeDto.DocNumber && 
				//	x.DocSeries == employeeDto.DocSeries))
				//{
				//	throw new ArgumentException($"Сотрудник с номером документа \"{employeeDto.DocNumber}\" и серией документа \"{employeeDto.DocSeries}\" уже числится в данной организации!");
    //            }
				_db.Employees.Add(employee);
			}
			await _db.SaveChangesAsync();
			return _mapper.Map<Employee, EmployeeDto>(employee);
		}

		public async Task<bool> DeleteEmployeeByIdAsync(decimal employeeId)
		{
			var employee = await _db.Employees
				.FirstOrDefaultAsync(x => x.Id == employeeId);

			if (employee == null)
			{
				return false;
			}

			_db.Employees.Remove(employee);
			await _db.SaveChangesAsync();

			return true;
		}

		public async Task<EmployeeDto> GetEmployeeByIdAsync(decimal employeeId)
		{
			var employee = await _db.Employees
				.FirstAsync(x => x.Id == employeeId);
			return _mapper.Map<EmployeeDto>(employee);
		}

		public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentIdAsync(Guid departmentId)
		{
			var employeesList = await _db.Employees
				.Where(x => x.DepartmentId == departmentId)
				.ToListAsync();
			return _mapper.Map<List<EmployeeDto>>(employeesList);
		}
	}
}
