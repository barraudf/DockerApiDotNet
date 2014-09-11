using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DockerApiDotNet.Data
{
	public class Container
	{
		public string Command { get; set; }

		[JsonConverter(typeof(JsonConverters.UnixDateTimeConverter))]
		public DateTime Created { get; set; }

		public string Id { get; set; }

		public string Image { get; set; }

		public IList<string> Names { get; set; }

		public IList<Port> Ports { get; set; }

		public string Status { get; set; }

		public int SizeRW { get; set; }

		public int SizeRootFs { get; set; }

		public bool PrintSize = false;

		public string ShortID
		{
			get { return Id.Substring(0, 12); }
		}

		public string Name
		{
			get
			{
				foreach (string s in Names)
				{
					if (s.Split('/').Length == 2)
						return s.Substring(1);
				}

				return Names[0].Substring(1);
			}
		}

	}
}
