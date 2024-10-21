using AnhBach.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnhBach.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class DIController : Controller
	{
		private readonly IMyService _myService;

		public DIController(IMyService myService)
		{
			_myService = myService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var message = _myService.GetMessage();
			return Ok(message);
		}
	}
}
