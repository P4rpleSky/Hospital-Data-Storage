using Microsoft.AspNetCore.Mvc;

namespace Kosta_Task.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult ErrorMessagesIndex()
		{
			return View();
		}
	}
}
