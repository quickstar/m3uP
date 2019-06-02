using System.Linq;

using m3uP.Models;
using m3uP.Services;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace m3uP.Controllers
{
	[ApiController]
	public class LineupController : ControllerBase
	{
		private readonly IPlaylistParser _playlistParser;
		private readonly IPlaylistProvider _playlistProvider;

		// GET api/discovery/description
		public LineupController(IPlaylistParser playlistParser, IPlaylistProvider playlistProvider)
		{
			_playlistParser = playlistParser;
			_playlistProvider = playlistProvider;
		}

		[Produces("application/json")]
		[HttpGet]
		[Route("lineup.json")]
		[Route("[controller]")]
		public ActionResult Get()
		{
			var allChannels = _playlistProvider.ReadChannels();
			var channelInfos = _playlistParser.ParseChannels(allChannels)
					.OrderBy(c => c.GroupTitle)
					.ThenBy(c => c.Name)
					.Select((c, i) => new Lineup
					{
							GuideName = c.Name,
							GuideNumber = (1000 + i).ToString(),
							Hd = 1,
							Url = c.StreamUrl.ToString()
					})
					.ToArray();

			return new JsonResult(channelInfos, new JsonSerializerSettings
			{
					ContractResolver = new DefaultContractResolver()
			});
		}
	}
}