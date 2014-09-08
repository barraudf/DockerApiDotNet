using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DockerApiDotNet
{
	internal class HttpOverSocketResponse
	{
		public WebHeaderCollection Headers { get; set; }
		public string Content { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public string StatusDescription { get; set; }

		public HttpOverSocketResponse(NetworkStream client)
		{
			Headers = new WebHeaderCollection();

			using (StreamReader sr = new StreamReader(client))
			{
				ReadHttpStatus(sr);
				ReadHttpHeaders(sr);
				ReadContent(sr);
			}
		}

		protected void ReadHttpStatus(StreamReader sr)
		{
			string status = sr.ReadLine();
			Match m = Regex.Match(status, "^HTTP/1.[0|1] (?<code>[0-9]+) (?<description>.*)$", RegexOptions.None);

			if (m.Success)
			{
				StatusCode = (HttpStatusCode) int.Parse(m.Groups["code"].Value);
				StatusDescription = m.Groups["description"].Value;
			}
			else
				throw new InvalidDataException("Can't read http response status from line \"" + status + "\"");
		}

		protected void ReadHttpHeaders(StreamReader sr)
		{
			while(true)
			{
				string line = sr.ReadLine();

				if (line.Length == 0)
					break;

				Match m = Regex.Match(line, @"^(?<key>[^:]+):[\t\s]*(?<value>.+)$", RegexOptions.None);

				if (m.Success)
				{
					string key = m.Groups["key"].Value;
					string value = m.Groups["value"].Value;
					Headers.Add(key, value);
				}
				else
					throw new InvalidDataException("Can't read http header from line \"" + line + "\"");
			}
		}

		protected void ReadContent(StreamReader sr)
		{
			StringBuilder sb = new StringBuilder();
			while (sr.Peek() >= 0)
			{
				sb.Append((char)sr.Read());
			}
			Content = sb.ToString();
		}
	}
}
