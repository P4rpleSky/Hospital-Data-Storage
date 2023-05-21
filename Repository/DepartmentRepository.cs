using AutoMapper;
using Kosta_Task.DbContexts;
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

		public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
		{
			var departmentsList = await _db.Departments.ToListAsync();
			return _mapper.Map<List<DepartmentDto>>(departmentsList);
		}
	}
}
