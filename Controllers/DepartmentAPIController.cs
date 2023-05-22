using Kosta_Task.Models.Dtos;
using Kosta_Task.Repository;
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

        [HttpGet("departmentId={departmentId:Guid}")]
        public async Task<ResponseDto> Get(Guid departmentId)
        {
            try
            {
                var departmentDtos = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
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
        public async Task<ResponseDto> Post([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                var departmentDtos = await _departmentRepository.CreateUpdateDepartmentAsync(departmentDto);
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
        public async Task<ResponseDto> Put([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                var departmentDtos = await _departmentRepository.CreateUpdateDepartmentAsync(departmentDto);
                _response.Result = departmentDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.ToString());
            }
            return _response;
        }

        [HttpDelete("departmentId={departmentId:Guid}")]
        public async Task<ResponseDto> Delete(Guid departmentId)
        {
            try
            {
                var departmentDtos = await _departmentRepository.DeleteDepartmentAsync(departmentId);
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
