using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace DockerApiDotNet.Data
{
	public class State
	{
		public int ExitCode { get; set; }

		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime FinishedAt { get; set; }

		public bool Paused { get; set; }

		public int Pid { get; set; }

		public bool Restarting { get; set; }

		public bool Running { get; set; }

		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime StartedAt { get; set; }
	}
}
