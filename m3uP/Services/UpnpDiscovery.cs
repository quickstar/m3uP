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

		private readonly IConfigurationSection _configuration;
		private readonly string _urlBase;

		public UpnpDiscovery(IConfiguration configuration)
		{
			_configuration = configuration.GetSection("Discovery");
			_urlBase = configuration.GetValue<string>("app:urlbase");
		}

		public UpnpInfo GenerateUpnpInfo()
		{
			var upnpInfo = new UpnpInfo { SpecVersion = new SpecVersion { Major = 1, Minor = 0 } };
			upnpInfo.Device = new Device
			{
					DeviceType = "urn:schemas-upnp-org:device:MediaServer:1",
					FriendlyName = _configuration.GetValue<string>("Device-Friendly-Name"),
					Manufacturer = _configuration.GetValue<string>("Device-Manufacturer"),
					ModelName = _configuration.GetValue<string>("Device-Model-Number"),
					ModelNumber = _configuration.GetValue<string>("Device-Model-Number"),
					SerialNumber = _configuration.GetValue<string>("Device-UUID"),
					Udn = $"uuid:{_configuration.GetValue<string>("Device-ID")}"
			};
			upnpInfo.UrlBase = _urlBase;

			return upnpInfo;
		}

		public string DeviceXml
		{
			get
			{
				var device = GenerateUpnpInfo().Device;
				return string.Format(DiscoverTemplate,
									_urlBase,
									device.FriendlyName,
									device.Manufacturer,
									device.ModelName,
									device.ModelNumber,
									device.SerialNumber,
									device.Udn);
			}
		}
	}
}