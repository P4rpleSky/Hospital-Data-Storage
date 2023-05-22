using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Services;
using Kosta_Task.Services.IServices;
using Kosta_Task.Pages;
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
			var departmentDtos = new List<DepartmentDto>();
			var response = await _departmentService.GetDepartmentsAsync();
			if (response is not null && response.IsSuccess)
			{
                departmentDtos = JsonConvert.DeserializeObject<List<DepartmentDto>>(response.Result.ToString());
            }
            return View(departmentDtos);
        }

        public async Task<IActionResult> DepartmentCreate()
        {
            var departmentsList = new List<DepartmentDto>();
            var response = await _departmentService.GetDepartmentsAsync();
            if (response is not null && response.IsSuccess)
            {
                departmentsList = JsonConvert.DeserializeObject<List<DepartmentDto>>(response.Result.ToString());
            }
            return View(departmentsList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepartmentCreate(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _departmentService.CreateDepartmentAsync(departmentDto);
                if (response is not null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DepartmentIndex));
                }
            }
            return View(departmentDto);
        }

        public async Task<IActionResult> DepartmentEdit(Guid departmentId)
        {
            var response = await _departmentService.GetDepartmentByIdAsync(departmentId);
            if (response is not null && response.IsSuccess)
            {
                var departmentDto = JsonConvert.DeserializeObject<DepartmentDto>(response.Result.ToString());
                return View(departmentDto);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepartmentEdit(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _departmentService.CreateDepartmentAsync(departmentDto);
                if (response is not null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DepartmentIndex));
                }
            }
            return View(departmentDto);
        }

        public async Task<IActionResult> DepartmentDelete(Guid departmentId)
        {
            var response = await _departmentService.GetDepartmentByIdAsync(departmentId);
            if (response is not null && response.IsSuccess)
            {
                var departmentDto = JsonConvert.DeserializeObject<DepartmentDto>(response.Result.ToString());
                return View(departmentDto);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepartmentDelete(DepartmentDto departmentDto)
        {
            var response = await _departmentService.DeleteDepartmentByIdAsync(departmentDto.Id);
            if (response is not null && response.IsSuccess)
            {
                return RedirectToAction(nameof(DepartmentIndex));
            }
            return NotFound();
        }
    }
}