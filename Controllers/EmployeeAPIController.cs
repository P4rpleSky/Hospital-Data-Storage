using Kosta_Task.Models.Dtos;
using Kosta_Task.Repository;
using Kosta_Task.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kosta_Task.Controllers
{
	[Route("api/employee")]
	public class EmployeeApiController : Controller
	{
		private ResponseDto _response;
		private IEmployeeRepository _employeeRepository;

		public EmployeeApiController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
			this._response = new ResponseDto();
		}

		[HttpGet("departmentId={departmentId:Guid}")]
		public async Task<ResponseDto> Get(Guid departmentId)
		{
			try
			{
				var departmentDtos = await _employeeRepository.GetEmployeesByDepartmentIdAsync(departmentId);
				_response.Result = departmentDtos;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages.Add(ex.ToString());
			}
			return _response;
		}

		[HttpGet("employeeId={employeeId:decimal}")]
		public async Task<ResponseDto> Get(decimal employeeId)
		{
			try
			{
				var departmentDtos = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
				_response.Result = departmentDtos;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages.Add(ex.ToString());
			}
			return _response;
		}

		[HttpPost]
		public async Task<ResponseDto> Post([FromBody] EmployeeDto employeeDto)
		{
			try
			{
				var departmentDtos = await _employeeRepository.CreateUpdateEmployeeAsync(employeeDto);
				_response.Result = departmentDtos;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages.Add(ex.ToString());
			}
			return _response;
		}

		[HttpPut]
		public async Task<ResponseDto> Put([FromBody] EmployeeDto employeeDto)
		{
			try
			{
				var departmentDtos = await _employeeRepository.CreateUpdateEmployeeAsync(employeeDto);
				_response.Result = departmentDtos;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages.Add(ex.ToString());
			}
			return _response;
		}

		//[HttpDelete]
		//public async Task<ResponseDto> Delete([FromBody] EmployeeDto employeeDto)
		//{
		//	try
		//	{
		//		var departmentDtos = await _employeeRepository.CreateUpdateEmployeeAsync(employeeDto);
		//		_response.Result = departmentDtos;
		//	}
		//	catch (Exception ex)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages.Add(ex.ToString());
		//	}
		//	return _response;
		//}
	}
}
