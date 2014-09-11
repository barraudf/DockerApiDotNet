using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace DockerApiDotNet.Data
{
	public class ContainerDetails
	{
		public string Command { get; set; }

		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime Created { get; set; }

		public string Id { get; set; }

		public string Path { get; set; }

		public string Image { get; set; }

		public IList<string> Args { get; set; }

		public string SysInitPath { get; set; }

		public string ResolvConfPath { get; set; }

		public ContainerConfig Config { get; set; }

		public string Driver { get; set; }

		public string ExecDriver { get; set; }

		public string HostnamePath { get; set; }

		public string HostsPath { get; set; }

		public string MountLabel { get; set; }

		public string Name { get; set; }

		public string ProcessLabel { get; set; }

		public HostConfig HostConfig { get; set; }

		public NetworkSettings NetworkSettings { get; set; }

		public State State { get; set; }

		public Dictionary<string, string> Volumes { get; set; }

		public Dictionary<string, bool> VolumesRW { get; set; }

		public string ShortID
		{
			get { return Id.Substring(0, 12); }
		}
	}
}
