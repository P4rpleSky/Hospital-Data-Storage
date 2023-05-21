using Kosta_Task.Models.Dtos;
using Kosta_Task.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kosta_Task.Controllers
{
	[Route("api/department")]
	public class DepartmentAPIController : Controller
	{
		private ResponseDto _response;
		private IDepartmentRepository _departmentRepository;

		public DepartmentAPIController(IDepartmentRepository departmentRepository)
		{
			_departmentRepository = departmentRepository;
			this._response = new ResponseDto();
		}

		[HttpGet]
		public async Task<ResponseDto> Get()
		{
			try
			{
				var departmentDtos = await _departmentRepository.GetDepartmentsAsync();
				_response.Result = departmentDtos;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages.Add(ex.ToString());
			}
			return _response;
		}
	}
}
