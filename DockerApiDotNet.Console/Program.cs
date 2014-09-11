using DockerApiDotNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerApiDotNet.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (args.Length == 0)
				{
					PrintHelp();
				}
				else
				{
#if __MonoCS__
					string dockerSocket = "unix:///var/run/docker.sock";
#else
					string dockerSocket = "tcp://127.0.0.1:1234";
#endif
					
					DockerAPIClient client = new DockerAPIClient(dockerSocket);

					switch (args[0])
					{
						case "ps":
							ps(args, client);
							break;
						case "inspect":
							inspect(args, client);
							break;
						case "top":
							top(args, client);
							break;
						case "diff":
							diff(args, client);
							break;
						case "info":
							info(client);
							break;
						case "version":
							version(client);
							break;
						default:
							PrintHelp();
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine("Error : " + ex.Message + Environment.NewLine + ex.StackTrace);
			}

#if !__MonoCS__
			System.Console.WriteLine();
			System.Console.WriteLine("Press any key to continue...");
			System.Console.ReadKey();
#endif
		}

		private static void PrintHelp()
		{
			System.Console.WriteLine("Usage : mono " + System.AppDomain.CurrentDomain.FriendlyName + " COMMAND [arg...]");
			System.Console.WriteLine();
			System.Console.WriteLine("Commands :");
			System.Console.WriteLine("	ps			List containers");
			System.Console.WriteLine("	inspect		Return low-level information on a container");
			System.Console.WriteLine("	top			List processes running inside a container");
			System.Console.WriteLine("	diff		Inspect changes on a container's filesystem");
			System.Console.WriteLine("	version		Show the docker version information");
			System.Console.WriteLine("	info		Display system-wide information");
		}

		private static void ps(string[] args, DockerApiDotNet.DockerAPIClient client)
		{
			bool all = false;
			bool size = false;

			if (args.Length > 1)
			{
				foreach (string a in args)
				{
					switch (a)
					{
						case "-a":
							all = true;
							break;

						case "-s":
						case "--size=true":
							size = true;
							break;

						default:
							break;
					}
				}
			}
			Container[] containers = client.GetContainers(size, all);
			foreach (Container c in containers)
				System.Console.WriteLine(c.ShortID + "(" + c.Name + ")");
		}

		private static void inspect(string[] args, DockerApiDotNet.DockerAPIClient client)
		{
			if (args.Length < 2)
				PrintHelp();
			else
			{
				ContainerDetails c = client.GetContainer(args[1]);
				System.Console.WriteLine("{0} - Name: {1} - Image: {2} - Running: {3}", c.ShortID, c.Name, c.Config.Image, c.State.Running ? "yes" : "no");
			}
		}

		private static void top(string[] args, DockerApiDotNet.DockerAPIClient client)
		{
			if (args.Length < 2)
				PrintHelp();
			else
			{
				string opts = "";

				if (args.Length > 2)
					opts = args[2];

				ContainerProcesses p = client.GetContainerTop(args[1], opts);
				foreach (string s in p.Titles)
					System.Console.Write(s + "\t");
				System.Console.WriteLine();

				foreach (IList<string> process in p.Processes)
				{
					foreach (string s in process)
						System.Console.Write(s + "\t");
					System.Console.WriteLine();
				}
			}
		}

		private static void diff(string[] args, DockerApiDotNet.DockerAPIClient client)
		{
			if (args.Length < 2)
				PrintHelp();
			else
			{

				ContainerDiff[] diff = client.GetContainerDiff(args[1]);
				foreach (ContainerDiff cd in diff)
					System.Console.WriteLine("{0} {1}", cd.Kind.ToString()[0], cd.Path);
			}
		}

		private static void version(DockerApiDotNet.DockerAPIClient client)
		{
			VersionInfo version = client.GetVersion();
			System.Console.WriteLine("ApiVersion\t: {0}", version.ApiVersion);
			System.Console.WriteLine("GitCommit\t: {0}", version.GitCommit);
			System.Console.WriteLine("GoVersion\t: {0}", version.GoVersion);
			System.Console.WriteLine("Version\t\t: {0}", version.Version);

		}

		private static void info(DockerApiDotNet.DockerAPIClient client)
		{
			SystemInfo info = client.GetInfo();
			System.Console.WriteLine("Containers\t\t: {0}", info.Containers);
			System.Console.WriteLine("Debug\t\t\t: {0}", info.Debug);
			System.Console.WriteLine("Driver\t\t\t: {0}", info.Driver);
			System.Console.WriteLine("ExecutionDriver\t\t: {0}", info.ExecutionDriver);
			System.Console.WriteLine("Images\t\t\t: {0}", info.Images);
			System.Console.WriteLine("IndexServerAddress\t: {0}", info.IndexServerAddress);
			System.Console.WriteLine("InitPath\t\t: {0}", info.InitPath);
			System.Console.WriteLine("IPv4Forwarding\t\t: {0}", info.IPv4Forwarding);
			System.Console.WriteLine("KernelVersion\t\t: {0}", info.KernelVersion);
			System.Console.WriteLine("MemoryLimit\t\t: {0}", info.MemoryLimit);
			System.Console.WriteLine("NEventsListener\t\t: {0}", info.NEventsListener);
			System.Console.WriteLine("NFd\t\t\t: {0}", info.NFd);
			System.Console.WriteLine("NGoroutines\t\t: {0}", info.NGoroutines);
			System.Console.WriteLine("SwapLimit\t\t: {0}", info.SwapLimit);
		}
	}
}
