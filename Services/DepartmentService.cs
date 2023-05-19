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

		public async Task<ResponseDto> GetDepartmentsAsync()
		{
			return await this.SendAsync(new ApiRequest
			{
				ApiType = SD.ApiType.GET,
				Url = SD.BaseUrl + "api/departments"
			});
		}
	}
}
