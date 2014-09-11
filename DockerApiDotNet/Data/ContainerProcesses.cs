using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerApiDotNet.Data
{
	public class ContainerProcesses
	{
		public IList<string> Titles { get; set; }
		
		public IList<IList<string>> Processes { get; set; }
	}
}
