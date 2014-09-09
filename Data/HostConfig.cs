using System.Collections.Generic;

namespace DockerApiDotNet.Data
{
	public class HostConfig
	{

		// Unkown types from doc
		/*
		"Devices":[
		],
		"DnsSearch":null,
		*/

		public string ContainerIDFile { get; set; }
		
		public string NetworkMode { get; set; }

		public Dictionary<string, List<NetworkSettingsPortBind>> PortBindings { get; set; }
		
		public bool Privileged { get; set; }

		public bool PublishAllPorts { get; set; }

		IList<string> VolumesFrom { get; set; }

		public RestartPolicy RestartPolicy { get; set; }

		public IList<KeyValuePair<string, string>> LxcConf { get; set; }

		public IList<string> CapAdd { get; set; }

		public IList<string> CapDrop { get; set; }

		public IList<string> Binds { get; set; }

		public IList<string> Links { get; set; }

		public IList<string> Dns { get; set; }
	}
}
