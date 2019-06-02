using Newtonsoft.Json;

namespace m3uP.Models
{
	public class Lineup
	{
		public string GuideName { get; set; }

		public string GuideNumber { get; set; }

		[JsonProperty("HD")]
		public int Hd { get; set; }

		[JsonProperty("URL")]
		public string Url { get; set; }
	}
}