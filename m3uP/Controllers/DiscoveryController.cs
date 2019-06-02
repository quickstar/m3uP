using Microsoft.AspNetCore.Mvc;

namespace m3uP.Controllers
{
	[ApiController]
	public class DiscoveryController : ControllerBase
	{
		// GET api/discovery/description
		[HttpGet]
		[Route("discover.json")]
		public ActionResult<string> Get()
		{
			return "value";
		}
	}
}