using AutoMapper.Internal;
using Kosta_Task.Models;
using Kosta_Task.Models.Dtos;

namespace Kosta_Task.Services.IServices
{
	public interface IBaseService
	{
		Task<ResponseDto> SendAsync(ApiRequest apiRequest);
	}
}
