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
				config.CreateMap<DepartmentDto, Department>();
				config.CreateMap<Department, DepartmentDto>();
			});

			return mappingConfig;
		}
	}
}
