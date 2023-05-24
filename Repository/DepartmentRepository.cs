using AutoMapper;
using Kosta_Task.DbContexts;
using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            if (departmentDto.ParentDepartmentName is not null)
            {
                var allowedParentDepartments = await this.GetDepartmentsExceptChildrenAsync(departmentDto.Id);
                var parentDepartment = allowedParentDepartments.First(x => x.Name == departmentDto.ParentDepartmentName);
                departmentDto.ParentDepartmentId = parentDepartment.Id;
            }

            if (departmentDto.Code is not null)
            {
				departmentDto.Code = departmentDto.Code.ToUpper();
			}
			var department = _mapper.Map<DepartmentDto, Department>(departmentDto);
            if (department.Id != Guid.Empty)
            {
                _db.Departments.Update(department);
            }
            else
            {
                department.Id = Guid.NewGuid();
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

            foreach (var childDepartment in _db.Departments.Where(x => x.ParentDepartmentId == departmentId))
            {
                foreach (var employee in _db.Employees.Where(x => x.DepartmentId == childDepartment.Id))
                    _db.Employees.Remove(employee);
                _db.Departments.Remove(childDepartment);
            }

            foreach (var employee in _db.Employees.Where(x => x.DepartmentId == department.Id))
                _db.Employees.Remove(employee);
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
			var departmentsList = await _db.Departments.AsNoTracking().ToListAsync();
			return _mapper.Map<List<DepartmentDto>>(departmentsList);
		}

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsExceptChildrenAsync(Guid parentId)
        {
            if (parentId == Guid.Empty)
                return await this.GetDepartmentsAsync();

            var parentDepartment = await _db.Departments.AsNoTracking().FirstAsync(x => x.Id == parentId);
            var disallowedDepartmentIds = new HashSet<Guid>();
            int counter;
            do
            {
                counter = 0;
                foreach (var department in _db.Departments.AsNoTracking())
                {
                    if ((parentDepartment.Id == department.Id ||
                         parentDepartment.Id == department.ParentDepartmentId ||
                         disallowedDepartmentIds.Any(x => x == department.ParentDepartmentId))
                         && !disallowedDepartmentIds.Contains(department.Id))
                    {
                        counter++;
                        disallowedDepartmentIds.Add(department.Id);
                    }
                }
            } while (counter > 0);
            var allowedDepartments = await this.GetDepartmentsAsync();
            allowedDepartments = allowedDepartments.Where(x => !disallowedDepartmentIds.Contains(x.Id));
            return allowedDepartments;
        }
    }
}
