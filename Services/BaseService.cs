using Kosta_Task.Models.Dtos;
using Kosta_Task.Models;
using System.Text;
using Newtonsoft.Json;
using static Kosta_Task.SD;

namespace Kosta_Task.Services
{
	public class BaseService
	{
		public IHttpClientFactory HttpClient { get; set; }

		public BaseService(IHttpClientFactory httpClient)
		{
			HttpClient = httpClient;
		}

		public async Task<ResponseDto> SendAsync(ApiRequest apiRequest)
		{
			try
			{
				var client = HttpClient.CreateClient("KostaAPI");
				HttpRequestMessage message = new HttpRequestMessage();
				message.Headers.Add("Accept", "application/json");
				message.RequestUri = new Uri(apiRequest.Url);
				client.DefaultRequestHeaders.Clear();
				if (apiRequest.Data != null)
				{
					message.Content = new StringContent(
						JsonConvert.SerializeObject(apiRequest.Data),
						Encoding.UTF8, 
						"application/json");
				}
				HttpResponseMessage apiResponse;
				switch (apiRequest.ApiType)
				{
					case ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					case ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
					default:
						message.Method = HttpMethod.Get;
						break;
				}
				apiResponse = await client.SendAsync(message);

				var apiContent = await apiResponse.Content.ReadAsStringAsync();

				if (!apiResponse.IsSuccessStatusCode)
					throw new Exception(apiContent);

				var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
				return apiResponseDto;
			}
			catch (Exception ex)
			{
				var dto = new ResponseDto
				{
					Message = "Error",
					ErrorMessages = new List<string> { ex.Message },
					IsSuccess = false
				};
				return dto;
			}
		}
	}
}
