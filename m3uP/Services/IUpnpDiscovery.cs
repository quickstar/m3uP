using System;

using m3uP.Models;

namespace m3uP.Services
{
	public interface IUpnpDiscovery
	{
		UpnpInfo GenerateUpnpInfo();

		string DeviceXml { get; }
	}
}