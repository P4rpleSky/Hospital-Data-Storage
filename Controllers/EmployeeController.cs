using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kosta_Task.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

		public async Task<IActionResult> EmployeeIndex(Guid departmentId)
        {
            var employeesList = new List<EmployeeDto>();
            var response = await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);
            if (response is not null && response.IsSuccess)
            {
                employeesList = JsonConvert.DeserializeObject<List<EmployeeDto>>(response.Result.ToString());
            }
            return View(employeesList);
        }

        public IActionResult EmployeeCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeCreate(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.CreateEmployeeAsync(employeeDto);
                if (response is not null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(EmployeeIndex), new { departmentId = employeeDto.DepartmentId });
                }
            }
            return View(employeeDto);
        }

        public async Task<IActionResult> EmployeeEdit(decimal employeeId)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (response is not null && response.IsSuccess)
            {
                var employeeDto = JsonConvert.DeserializeObject<EmployeeDto>(response.Result.ToString());
                return View(employeeDto);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeEdit(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.UpdateEmployeeAsync(employeeDto);
                if (response is not null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(EmployeeIndex), new { departmentId = employeeDto.DepartmentId });
                }
            }
            return View(employeeDto);
        }

        public async Task<IActionResult> EmployeeDelete(decimal employeeId)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (response is not null && response.IsSuccess)
            {
                var employeeDto = JsonConvert.DeserializeObject<EmployeeDto>(response.Result.ToString());
                return View(employeeDto);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeDelete(EmployeeDto employeeDto)
        {
            var response = await _employeeService.DeleteEmployeeByIdAsync(employeeDto.Id);
            if (response is not null && response.IsSuccess)
            {
                return RedirectToAction(nameof(EmployeeIndex), new { departmentId = employeeDto.DepartmentId });
            }
            return NotFound();
        }
    }
}
