using System.Collections.Generic;

namespace DockerApiDotNet.Data
{
	public class NetworkSettings
	{
		public string Bridge { get; set; }

		public string Gateway { get; set; }

		public string IPAddress { get; set; }

		public int IPPrefixLen { get; set; }

		// Unkown type from doc
		//"PortMapping":null,

		public Dictionary<string, IList<NetworkSettingsPortBind>> Ports { get; set; }
	}
}
