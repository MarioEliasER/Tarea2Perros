using Microsoft.AspNetCore.Mvc;

namespace Tarea2Perros.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Info()
		{
			return View();
		}
	}
}
