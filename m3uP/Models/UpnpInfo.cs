using Newtonsoft.Json;

namespace m3uP.Models
{
	public class UpnpInfo
	{
		[JsonProperty("BaseURL")]
		public string BaseUrl { get; set; }

		public string DeviceAuth { get; set; }

		[JsonProperty("DeviceID")]
		public string DeviceId { get; set; }

		[JsonIgnore]
		public string DeviceType { get; set; }

		public string FirmwareName { get; set; }

		public string FirmwareVersion { get; set; }

		public string FriendlyName { get; set; }

		[JsonProperty("LineupURL")]
		public string LineupUrl { get; set; }

		public string Manufacturer { get; set; }

		[JsonIgnore]
		public string ModelName { get; set; }

		public string ModelNumber { get; set; }

		[JsonIgnore]
		public string SerialNumber { get; set; }

		public int TunerCount { get; set; }

		[JsonIgnore]
		public string Udn { get; set; }

		[JsonIgnore]
		public string DeviceUuid { get; set; }
	}
}