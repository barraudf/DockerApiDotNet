using System.Text.RegularExpressions;

namespace DockerApiDotNet
{
	public static class Utils
	{
		public static bool RegExMatch(string input, string pattern, out GroupCollection match, RegexOptions opts = RegexOptions.None)
		{
			Match m = Regex.Match(input, pattern, opts);

			match = m.Groups;
			return m.Success;
		}
	}
}
