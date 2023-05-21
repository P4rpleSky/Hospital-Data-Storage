using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kosta_Task.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentService _departmentService;

		public DepartmentController(IDepartmentService departmentService)
		{
			_departmentService = departmentService;
		}

		public async Task<IActionResult> DepartmentIndex()
		{
			var departmentsList = new List<DepartmentDto>();
			var response = await _departmentService.GetDepartmentsAsync();
			if (response is null || response.IsSuccess == false)
			{
				return RedirectToAction(
					nameof(ErrorController.ErrorMessagesIndex), nameof(ErrorController),
					new
					{
						errorMessages = response is null ?
							new List<string> { "API response is null!" } : 
							response.ErrorMessages
					});
			}
			departmentsList = JsonConvert.DeserializeObject<List<DepartmentDto>>(
				response.Result.ToString());
			return View(departmentsList);
		}
    }
}