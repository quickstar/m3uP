using System.Collections.Generic;
using m3uP.Models;

namespace m3uP.Services
{
	public interface IPlaylistParser
	{
		IEnumerable<ChanelInfo> ParseChannels(string playlistContent);
	}
}