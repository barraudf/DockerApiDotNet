namespace DockerApiDotNet.Data
{
	public enum ChangeTypes { Changed = 0, Added = 1, Deleted = 2 };

	public class ContainerDiff
	{
		public string Path { get; set; }

		public ChangeTypes Kind { get; set; }
	}
}
