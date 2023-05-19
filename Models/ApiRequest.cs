using System.Security.AccessControl;
using static Kosta_Task.SD;

namespace Kosta_Task.Models
{
	public class ApiRequest
	{
		public ApiType ApiType { get; set; }
		public string Url { get; set; } = string.Empty;
		public object? Data { get; set; }
	}
}
