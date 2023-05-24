using Kosta_Task.Models.Dtos;
using Kosta_Task.Services.IServices;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kosta_Task.Attributes
{
	public class СodeAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var departmentService = (IDepartmentService)validationContext
				.GetService(typeof(IDepartmentService));

            var codeProperty = validationContext.ObjectType.GetProperty("Code");
            var idProperty = validationContext.ObjectType.GetProperty("Id");
            
			var id = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
			var code = codeProperty.GetValue(validationContext.ObjectInstance);

			if (code is null || string.IsNullOrEmpty(code.ToString()))
			{
				return null;
			}

			var response = departmentService.GetDepartmentsAsync().Result;
			var departmentsList = JsonConvert.DeserializeObject<List<DepartmentDto>>(response.Result.ToString());

			if (!departmentsList.Any(x => x.Id != id &&
                x.Code is not null && x.Code.Equals(code)))
			{
				return null;
			}
			else
			{
				return new ValidationResult("Отдел с таким мнемокодом уже существует!");
			}
		}
	}
}
