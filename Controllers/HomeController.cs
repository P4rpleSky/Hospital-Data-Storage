using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Kosta_Task.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDepartmentService _departmentService;

		public HomeController(IDepartmentService departmentService)
		{
			_departmentService = departmentService;
		}

		public async Task<IActionResult> Index()
		{
			var departmentsList = new List<DepartmentDto>();
			var response = await _departmentService.GetDepartmentsAsync();
			if (response != null && response.IsSuccess)
			{
				departmentsList = JsonConvert.DeserializeObject<List<DepartmentDto>>(
					response.Result.ToString());
			}
			return View(departmentsList);
		}
	}
}