using System;
using System.Threading;
using Rssdp;

namespace Discovery
{
    internal class Program
    {
        private static readonly Guid MyGuid = new Guid("0b73c4e0-cfc7-4621-8be3-469927683e4b");

        private static void Main(string[] args)
        {
            // Declare \_Publisher as a field somewhere, so it doesn't get GCed after the method finishes.
            SsdpDevicePublisher publisher = new SsdpDevicePublisher();
            var def = PublishDevice();
            publisher.AddDevice(def);

            Console.ReadLine();
        }

        // Call this method from somewhere to actually do the publish.
        public static SsdpRootDevice PublishDevice()
        {
            // As this is a sample, we are only setting the minimum required properties.
            var deviceDefinition = new SsdpRootDevice
            {
                CacheLifetime = TimeSpan.FromMinutes(30), //How long SSDP clients can cache this info.
                Location = new Uri("http://192.168.66.197:52087/discover.json"), // Must point to the URL that serves your devices UPnP description document. 
                DeviceTypeNamespace = "urn:schemas-upnp-org:device:MediaServer:1",
                DeviceType = "MyCustomDevice",
                FriendlyName = "m3uP",
                Manufacturer = "Silicondust",
                ModelName = "HDTC-2US",
                ModelNumber = "HDTC-2US",
                Uuid = MyGuid.ToString() // This must be a globally unique value that survives reboots etc. Get from storage or embedded hardware etc.
            };
            return deviceDefinition;
        }
    }
}