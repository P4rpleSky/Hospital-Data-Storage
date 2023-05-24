using Kosta_Task.Models.Dtos;
using Kosta_Task.Services.IServices;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kosta_Task.Attributes
{
	public class DocumentDataAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var employeeService = (IEmployeeService)validationContext
				.GetService(typeof(IEmployeeService));

			var numberProperty = validationContext.ObjectType.GetProperty("DocNumber");
			var seriesProperty = validationContext.ObjectType.GetProperty("DocSeries");
			var idProperty = validationContext.ObjectType.GetProperty("Id");
			var departmentIdProperty = validationContext.ObjectType.GetProperty("DepartmentId");

			var id = (decimal)idProperty.GetValue(validationContext.ObjectInstance);
			var docNumber = numberProperty.GetValue(validationContext.ObjectInstance);
			if (docNumber is null)
				docNumber = string.Empty;
			var docSeries = seriesProperty.GetValue(validationContext.ObjectInstance);
            if (docSeries is null)
                docSeries = string.Empty;
            var departmentId = (Guid)departmentIdProperty.GetValue(validationContext.ObjectInstance);

			var response = employeeService.GetEmployeesByDepartmentIdAsync(departmentId).Result;
			var employeesList = JsonConvert.DeserializeObject<List<EmployeeDto>>(response.Result.ToString());

			foreach (var employee in employeesList)
			{
				if (employee.DocNumber is null)
					employee.DocNumber = string.Empty;
                if (employee.DocSeries is null)
                    employee.DocNumber = string.Empty;
            }

			if (!employeesList.Any(x => x.Id != id &&
                x.DocNumber.Equals(docNumber) && x.DocSeries.Equals(docSeries)))
			{
				return null;
			}
			else
			{
				return new ValidationResult("Сотрудник с такими паспортными данными уже существует в данном отделе!");
			}
		}
	}
}
