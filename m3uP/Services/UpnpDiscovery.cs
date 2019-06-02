using m3uP.Models;

using Microsoft.Extensions.Configuration;

namespace m3uP.Services
{
	public class UpnpDiscovery : IUpnpDiscovery
	{
		private const string DiscoverTemplate = @"<root xmlns=""urn:schemas-upnp-org:device-1-0"">
					<specVersion>
						<major>1</major>
						<minor>0</minor>
					</specVersion>
					<URLBase>{0}</URLBase>
					<device>
						<deviceType>urn:schemas-upnp-org:device:MediaServer:1</deviceType>
						<friendlyName>{1}</friendlyName>
						<manufacturer>{2}</manufacturer>
						<modelName>{3}</modelName>
						<modelNumber>{4}</modelNumber>
						<serialNumber>{5}</serialNumber>
						<UDN>{6}</UDN>
					</device>
				</root>";

		private readonly string _baseUrl;

		private readonly IConfigurationSection _configuration;

		public UpnpDiscovery(IConfiguration configuration)
		{
			_configuration = configuration.GetSection("discovery");
			_baseUrl = configuration.GetValue<string>("app:baseurl");
		}

		public string DeviceXml
		{
			get
			{
				var device = GenerateUpnpInfo();
				return string.Format(DiscoverTemplate,
									_baseUrl,
									device.FriendlyName,
									device.Manufacturer,
									device.ModelName,
									device.ModelNumber,
									device.SerialNumber,
									device.Udn);
			}
		}

		public UpnpInfo GenerateUpnpInfo()
		{
			var upnpInfo = new UpnpInfo
			{
					BaseUrl = _baseUrl,
					DeviceAuth = _configuration.GetValue<string>("Device-Auth"),
					DeviceId = _configuration.GetValue<string>("Device-ID"),
					DeviceUuid = _configuration.GetValue<string>("Device-UUID"),
					DeviceType = "urn:schemas-upnp-org:device:MediaServer:1",
					FirmwareName = _configuration.GetValue<string>("Device-Firmware-Name"),
					FirmwareVersion = _configuration.GetValue<string>("Device-Firmware-Version"),
					FriendlyName = _configuration.GetValue<string>("Device-Friendly-Name"),
					LineupUrl = $"{_baseUrl.TrimEnd('/')}/lineup.json",
					Manufacturer = _configuration.GetValue<string>("Device-Manufacturer"),
					ModelName = _configuration.GetValue<string>("Device-Model-Number"),
					ModelNumber = _configuration.GetValue<string>("Device-Model-Number"),
					SerialNumber = _configuration.GetValue<string>("Device-UUID"),
					TunerCount = 1,
					Udn = $"uuid:{_configuration.GetValue<string>("Device-ID")}"
			};

			return upnpInfo;
		}
	}
}