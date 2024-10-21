using Microsoft.AspNetCore.Mvc;

namespace AnhBach.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookController : ControllerBase
	{
		public BookController()
		{
		}
		[HttpGet]
		public int Get()
		{
			return 88880;
		}
	}
}
