using System.Net;

using m3uP.Models;
using m3uP.Services;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace m3uP.Controllers
{
	[ApiController]
	public class DiscoveryController : ControllerBase
	{
		private readonly IUpnpDiscovery _upnpDiscovery;

		// GET api/discovery/description
		public DiscoveryController(IUpnpDiscovery upnpDiscovery)
		{
			_upnpDiscovery = upnpDiscovery;
		}

		[Produces("application/json")]
		[HttpGet]
		[Route("discover.json")]
		public ActionResult<UpnpInfo> Discover()
		{
			var generateUpnpInfo = _upnpDiscovery.GenerateUpnpInfo();
			return new JsonResult(generateUpnpInfo, new JsonSerializerSettings
			{
					ContractResolver = new DefaultContractResolver()
			});
		}

		[Produces("application/xml")]
		[HttpGet]
		[Route("device.xml")]
		[Route("[controller]")]
		public ActionResult<string> Get()
		{
			var upnpInfo = _upnpDiscovery.DeviceXml;

			return new ContentResult
			{
					ContentType = "application/xml",
					Content = upnpInfo,
					StatusCode = (int)HttpStatusCode.OK
			};
		}
	}
}