using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;
using Kosta_Task.Services.IServices;

namespace Kosta_Task.Services
{
	public class DepartmentService : BaseService, IDepartmentService
	{
		private readonly IHttpClientFactory _clientFactory;

		public DepartmentService(IHttpClientFactory clientFactory) : base(clientFactory)
		{
			_clientFactory = clientFactory;
		}

        public async Task<ResponseDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            return await this.SendAsync(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data= departmentDto,
                Url = SD.BaseUrl + "api/department"
            });
        }

        public async Task<ResponseDto> DeleteDepartmentByIdAsync(Guid departmentId)
        {
            return await this.SendAsync(new ApiRequest
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.BaseUrl + $"api/department/departmentId={departmentId}"
            });
        }

        public async Task<ResponseDto> GetDepartmentByIdAsync(Guid departmentId)
        {
            return await this.SendAsync(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BaseUrl + $"api/department/departmentId={departmentId}"
            });
        }

        public async Task<ResponseDto> GetDepartmentsAsync()
		{
			return await this.SendAsync(new ApiRequest
			{
				ApiType = SD.ApiType.GET,
				Url = SD.BaseUrl + "api/department"
			});
		}

        public async Task<ResponseDto> UpdateDepartmentAsync(DepartmentDto departmentDto)
        {
            return await this.SendAsync(new ApiRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = departmentDto,
                Url = SD.BaseUrl + "api/department"
            });
        }
    }
}
