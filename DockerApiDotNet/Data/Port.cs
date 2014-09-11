namespace DockerApiDotNet.Data
{
	public class Port
	{
		public string IP { get; set; }
		public int PrivatePort { get; set; }
		public int PublicPort { get; set; }
		public string Type { get; set; }
	}
}
