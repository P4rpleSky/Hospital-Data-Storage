using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Services;
using Kosta_Task.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using Azure;

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
            var departmentDtos = string.Empty;
            var response = await _departmentService.GetDepartmentsAsync();
            if (response is not null && response.IsSuccess)
            {
                departmentDtos = response.Result.ToString();
            }
            var departmentRazorDto = new DepartmentRazorDto
            {
                DepartmentDto = new DepartmentDto(),
                DepartmentsListJson = departmentDtos
            };
            return View(departmentRazorDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepartmentCreate(DepartmentRazorDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _departmentService.CreateDepartmentAsync(model.DepartmentDto);
                if (response is not null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DepartmentIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DepartmentEdit(Guid departmentId)
        {
            var departmentResponse = await _departmentService.GetDepartmentByIdAsync(departmentId);
            var departmentDtos = new List<DepartmentDto>();
            var departmentsListResponse = await _departmentService.GetDepartmentsExceptChildrenAsync(departmentId);
            if (departmentResponse is not null && departmentResponse.IsSuccess &&
                departmentsListResponse is not null && departmentsListResponse.IsSuccess)
            {
                var departmentDto = JsonConvert.DeserializeObject<DepartmentDto>(departmentResponse.Result.ToString());
                var departmentRazorDto = new DepartmentRazorDto
                {
                    DepartmentDto = departmentDto,
                    DepartmentsListJson = departmentsListResponse.Result.ToString()
                };
                return View(departmentRazorDto);
            }
            return NotFound();            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepartmentEdit(DepartmentRazorDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _departmentService.CreateDepartmentAsync(model.DepartmentDto);
                if (response is not null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DepartmentIndex));
                }
			}
            return View(model);
        }

        public async Task<IActionResult> DepartmentDelete(Guid departmentId)
        {
            var departmentResponse = await _departmentService.GetDepartmentByIdAsync(departmentId);
            var departmentDtos = new List<DepartmentDto>();
            var departmentsListResponse = await _departmentService.GetDepartmentsAsync();
            if (departmentResponse is not null && departmentResponse.IsSuccess &&
                departmentsListResponse is not null && departmentsListResponse.IsSuccess)
            {
                var departmentDto = JsonConvert.DeserializeObject<DepartmentDto>(departmentResponse.Result.ToString());
                
                departmentDtos = JsonConvert.DeserializeObject<List<DepartmentDto>>(departmentsListResponse.Result.ToString());
                departmentDtos = departmentDtos.Where(x => x.Id == departmentDto.ParentDepartmentId).ToList();
                
                if (departmentDto.ParentDepartmentId is null)
                    return NotFound();

                var departmentRazorDto = new DepartmentRazorDto
                {
                    DepartmentDto = departmentDto,
                    DepartmentsListJson = JsonConvert.SerializeObject(departmentDtos)
                };
                return View(departmentRazorDto);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepartmentDelete(DepartmentRazorDto model)
        {
            var response = await _departmentService.DeleteDepartmentByIdAsync(model.DepartmentDto.Id);
            if (response is not null && response.IsSuccess)
            {
                return RedirectToAction(nameof(DepartmentIndex));
            }
            return NotFound();
        }
    }
}