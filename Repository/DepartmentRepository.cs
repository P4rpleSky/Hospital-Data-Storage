using AutoMapper;
using Kosta_Task.DbContexts;
using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kosta_Task.Repository
{
    public class DepartmentRepository : IDepartmentRepository
	{
		private readonly TestDbContext _db;
		private readonly IMapper _mapper;

		public DepartmentRepository(TestDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

        public async Task<DepartmentDto> CreateUpdateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<DepartmentDto, Department>(departmentDto);
            if (_db.Departments.Any(x => x.Id == department.Id))
            {
                _db.Departments.Update(department);
            }
            else
            {
                _db.Departments.Add(department);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Department, DepartmentDto>(department);
        }

        public async Task<bool> DeleteDepartmentAsync(Guid departmentId)
        {
            var department = await _db.Departments
                .FirstOrDefaultAsync(x => x.Id == departmentId);

            if (department == null)
            {
                return false;
            }

            _db.Departments.Remove(department);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid departmentId)
        {
            var department = await _db.Departments
                .FirstAsync(x => x.Id == departmentId);
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
		{
			var departmentsList = await _db.Departments.ToListAsync();
			return _mapper.Map<List<DepartmentDto>>(departmentsList);
		}
	}
}
