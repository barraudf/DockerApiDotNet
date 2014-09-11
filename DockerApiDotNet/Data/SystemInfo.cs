using System.Collections.Generic;

namespace DockerApiDotNet.Data
{
	public class SystemInfo
	{
		public int Containers { get; set; }
		
		public int Images { get; set; }
		
		public string Driver { get; set; }
		
		public string ExecutionDriver { get; set; }
		
		public string KernelVersion { get; set; }
		
		public bool Debug { get; set; }
		
		public int NFd { get; set; }
		
		public int NGoroutines { get; set; }
		
		public int NEventsListener { get; set; }
		
		public string InitPath { get; set; }
		
		public string IndexServerAddress { get; set; }
		
		public bool MemoryLimit { get; set; }
		
		public bool SwapLimit { get; set; }
		
		public bool IPv4Forwarding { get; set; }
	}
}
