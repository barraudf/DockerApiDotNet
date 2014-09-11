namespace DockerApiDotNet.Data
{
	public class VersionInfo
	{
		public string ApiVersion { get; set; }
		public string Version { get; set; }
		public string GitCommit { get; set; }
		public string GoVersion { get; set; }
	}
}
