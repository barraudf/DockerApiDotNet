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

		public string ShortID
		{
			get { return Id.Substring(0, 12); }
		}
	}
}
