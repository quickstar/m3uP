using System.Linq;

using m3uP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace m3uP.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPlaylistParser _playlistParser;
		private readonly IPlaylistProvider _playlistProvider;

		public HomeController(IPlaylistParser playlistParser, IPlaylistProvider playlistProvider)
		{
			_playlistParser = playlistParser;
			_playlistProvider = playlistProvider;
		}

		public IActionResult Index()
		{
			var allChannels = _playlistProvider.ReadChannels();
			var channelInfos = _playlistParser.ParseChannels(allChannels)
					.OrderBy(c => c.GroupTitle)
					.ThenBy(c => c.Name);
			return View(channelInfos);
		}
	}
}