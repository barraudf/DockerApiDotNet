using System;
using System.Collections.Generic;

namespace DockerApiDotNet.Data
{
	public class ContainerConfig
	{
		public bool AttachStderr { get; set; }

		
		public bool AttachStdin { get; set; }
		
		public bool AttachStdout { get; set; }
		
		public string[] Cmd { get; set; }
		
		public int CpuShares { get; set; }
		
		public string Cpuset { get; set; }
		
		public string Domainname { get; set; }
		
		public string Entrypoint { get; set; }
		
		public string[] Env { get; set; }

		public Dictionary<string, object> ExposedPorts { get; set; }
		
		public string Hostname { get; set; }
		
		public string Image { get; set; }
		
		public int Memory { get; set; }
		
		public int MemorySwap { get; set; }
		
		public bool NetworkDisabled { get; set; }
		
		public string OnBuild { get; set; }
		
		public bool OpenStdin { get; set; }
		
		// unknown type from doc
		//"PortSpecs":null,
		
		public bool StdinOnce { get; set; }
		
		public bool Tty { get; set; }
		
		public string User { get; set; }

		public Dictionary<string, object> Volumes { get; set; }
		
		public string WorkingDir { get; set; }
	}
}
