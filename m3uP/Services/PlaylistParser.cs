using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using m3uP.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace m3uP.Services
{
	public class PlaylistParser : IPlaylistParser
	{
		private static readonly Regex AttributeSelector =
				new Regex("(?<!\\S)(tvg-)?([a-z0-9-]+)=\"([\\w0-9.\\s%:&*\\/+()|'-]+)\"(?!\\S,)");

		private static readonly Regex LineFilter =
				new Regex("^#EXTINF:.*group-title=\"Switzerland\".*,.*(HD|FHD).+\\n.+$",
						RegexOptions.Multiline | RegexOptions.IgnoreCase);

		public IEnumerable<ChanelInfo> ParseChannels(string playlistContent)
		{
			string[] filteredLines = { };
			var lineMatches = LineFilter.Matches(playlistContent);
			if (lineMatches.Count > 0)
			{
				filteredLines = lineMatches.Select(m => m.Value).ToArray();
			}

			foreach (var filteredLine in filteredLines)
			{
				var miniSplit = filteredLine.Split('\n');

				var channelAttributes = AttributeSelector.Matches(miniSplit[0]);
				var newObj = new JObject();
				foreach (Match match in channelAttributes)
				{
					if (match.Success && match.Groups.Any())
					{
						var name = match.Groups[2].Value.Replace("-", string.Empty);
						var value = match.Groups[3].Value;
						if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
						{
							newObj.Add(new JProperty(name, value));
						}
					}
				}

				newObj.Add(new JProperty("rawinfo", filteredLine));
				newObj.Add(new JProperty("streamurl", miniSplit[1]));
				var qualities = miniSplit[0].Split(",").Last().Split(" ");
				var quals = new JArray();
				for (var i = qualities.Length - 1; i >= 0; i--)
				{
					quals.Add(new JValue(qualities[i]));
				}

				newObj.Add(new JProperty("quality", quals));
				yield return JsonConvert.DeserializeObject<ChanelInfo>(newObj.ToString());
			}
		}
	}
}