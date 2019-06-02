using System;

using Newtonsoft.Json;

namespace m3uP.Models
{
	public class UpnpInfo
	{
		public Device Device { get; set; }

		public SpecVersion SpecVersion { get; set; }

		[JsonProperty("URLBase")]
		public string UrlBase { get; set; }
	}

	public class SpecVersion
	{
		public int Major { get; set; }

		public int Minor { get; set; }
	}

	public class Device
	{
		public string DeviceType { get; set; }

		public string FriendlyName { get; set; }

		public string Manufacturer { get; set; }

		public string ModelName { get; set; }

		public string ModelNumber { get; set; }

		public string SerialNumber { get; set; }

		[JsonProperty("UDN")]
		public string Udn { get; set; }
	}
}