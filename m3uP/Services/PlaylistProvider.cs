using System;
using System.IO;
using System.Net.Http;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace m3uP.Services
{
	public class PlaylistProvider : IPlaylistProvider
	{
		private readonly IConfiguration _configuration;

		public PlaylistProvider(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string ReadChannels()
		{
			var channelFilePath = Path.Combine(Environment.CurrentDirectory, "channels.m3u");
			if (!File.Exists(channelFilePath))
			{
				using (var httpClient = new HttpClient())
				{
					var playlistUrl = _configuration.GetValue<string>("Provider:m3u");
					var response = httpClient.GetAsync(playlistUrl).Result;
					var textResult = response.Content.ReadAsStringAsync().Result;
					File.WriteAllText(channelFilePath, textResult, Encoding.UTF8);
					return textResult;
				}
			}

			return File.ReadAllText(channelFilePath, Encoding.UTF8);
		}

		public string ReadEpg()
		{
			var epgFilePath = Path.Combine(Environment.CurrentDirectory, "epg.xml");
			if (!File.Exists(epgFilePath))
			{
				using (var httpClient = new HttpClient())
				{
					var playlistUrl = _configuration.GetValue<string>("Provider:epg");
					var response = httpClient.GetAsync(playlistUrl).Result;
					var textResult = response.Content.ReadAsStringAsync().Result;
					File.WriteAllText(epgFilePath, textResult, Encoding.UTF8);
					return textResult;
				}
			}

			return File.ReadAllText(epgFilePath, Encoding.UTF8);
		}
	}
}