using Kosta_Task.Models.Dtos;
using Kosta_Task.Services.IServices;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kosta_Task.Attributes
{
	public class DepartmentDataAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var departmentService = (IDepartmentService)validationContext
				.GetService(typeof(IDepartmentService));

			var idProperty = validationContext.ObjectType.GetProperty("Id");
			var nameProperty = validationContext.ObjectType.GetProperty("Name");
			var parentNameProperty = validationContext.ObjectType.GetProperty("ParentDepartmentName");

			var id = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
			var name = nameProperty.GetValue(validationContext.ObjectInstance);
			var parentName = parentNameProperty.GetValue(validationContext.ObjectInstance);

			var response = departmentService.GetDepartmentsAsync().Result;
			var departmentsList = JsonConvert.DeserializeObject<List<DepartmentDto>>(response.Result.ToString());

			foreach (var department in departmentsList)
			{
				if (department.ParentDepartmentId is not null && department.ParentDepartmentId != Guid.Empty)
				{
					department.ParentDepartmentName = departmentsList
						.First(x => x.Id == department.ParentDepartmentId)
						.Name;
				}
			}

			if (!departmentsList.Any(x => x.Id != id &&
					x.Name.Equals(name) &&
                    x.ParentDepartmentName is not null && x.ParentDepartmentName.Equals(parentName)))
			{
				return null;
			}
			else
			{
				return new ValidationResult("Отдел с таким именем уже существует в указанном родительском отделе!");
			}
		}
	}
}
