using System;

namespace m3uP.Models
{
	public class ChanelInfo
	{
		public string Id { get; set; }
		public string RawInfo { get; set; }
		public string Name { get; set; }
		public Uri Logo { get; set; }
		public Uri StreamUrl { get; set; }
		public string GroupTitle { get; set; }
		public string[] Quality { get; set; }
	}
}