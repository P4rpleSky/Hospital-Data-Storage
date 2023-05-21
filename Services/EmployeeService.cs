using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Services.IServices;

namespace Kosta_Task.Services
{
	public class EmployeeService : BaseService, IEmployeeService
	{
		private readonly IHttpClientFactory _clientFactory;

		public EmployeeService(IHttpClientFactory clientFactory) : base(clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<ResponseDto> CreateEmployeeAsync(EmployeeDto employeeDto)
		{
			return await this.SendAsync(new ApiRequest
			{
				ApiType = SD.ApiType.POST,
				Data = employeeDto,
				Url = SD.BaseUrl + "api/employee"
			});
		}

        public async Task<ResponseDto> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            return await this.SendAsync(new ApiRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = employeeDto,
                Url = SD.BaseUrl + "api/employee"
            });
        }

        public async Task<ResponseDto> DeleteEmployeeAsync(int employeeId)
		{
			return await this.SendAsync(new ApiRequest
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.BaseUrl + $"api/employee/employeeId={employeeId}"
            });
		}

		public async Task<ResponseDto> GetEmployeeByIdAsync(decimal employeeId)
		{
			return await this.SendAsync(new ApiRequest
			{
				ApiType = SD.ApiType.GET,
				Url = SD.BaseUrl + $"api/employee/employeeId={employeeId}"
			});
		}

		public async Task<ResponseDto> GetEmployeesByDepartmentIdAsync(Guid departmentId)
		{
			return await this.SendAsync(new ApiRequest
			{
				ApiType = SD.ApiType.GET,
				Url = SD.BaseUrl + $"api/employee/departmentId={departmentId}"
			});
		}
	}
}
