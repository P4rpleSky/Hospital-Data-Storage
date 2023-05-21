using AutoMapper;
using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;

namespace Kosta_Task
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<DepartmentDto, Department>().ReverseMap();
				config.CreateMap<Employee, EmployeeDto>()
					.AfterMap((employee, employeeDto) =>
					{
						employeeDto.Age = (int)(DateTime.Now.Subtract(employee.DateOfBirth).TotalDays / 365);
					})
					.ReverseMap();
			});

			return mappingConfig;
		}
	}
}
