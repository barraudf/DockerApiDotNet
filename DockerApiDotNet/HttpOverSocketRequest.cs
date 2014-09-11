using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#if __MonoCS__
using Mono.Unix;
#endif

namespace DockerApiDotNet
{
	public enum HttpRequestMethod { GET, POST, DELETE };

	internal class HttpOverSocketRequest : IDisposable
	{
		protected const string HTTP_VERS = "HTTP/1.0";

		public string Path { get; set; }
		public  WebHeaderCollection Headers { get; set; }
		public HttpRequestMethod Method { get; set; }

		private NetworkStream _Client;

		public HttpOverSocketRequest(NetworkStream client)
		{
			_Client = client;
			Headers = new WebHeaderCollection();
			Method = HttpRequestMethod.GET;
		}

		public static HttpOverSocketRequest Create(string destination)
		{
			HttpOverSocketRequest request = null;
			NetworkStream client = null;
			GroupCollection match = null;

#if __MonoCS__
			if (client == null && Utils.RegExMatch(destination, @"^unix://(?<path>/.*)$", out match))
			{
				UnixClient socketClient = new UnixClient(match["path"].Value);
				client = socketClient.GetStream();
			}
#endif
			if (client == null && Utils.RegExMatch(destination, @"^tcp://(?<host>[\w\d\.]+):(?<port>\d+)$", out match))
			{
				TcpClient socketClient = new TcpClient(match["host"].Value, int.Parse(match["port"].Value));
				client = socketClient.GetStream();
			}

			if (client != null)
			{
				request = new HttpOverSocketRequest(client);
			}

			return request;
		}

		public HttpOverSocketResponse GetResponse()
		{
			using (StreamWriter sw = new StreamWriter(_Client))
			{
				sw.WriteLine(Method.ToString() + " " + Path + " " + HTTP_VERS);
				foreach(string key in Headers)
				{
					sw.WriteLine(key + ": " + Headers[key]);
				}
				sw.WriteLine();
				sw.Flush();

				HttpOverSocketResponse response = new HttpOverSocketResponse(_Client);

				return response;
			}
		}

		#region IDisposable
		private bool disposed = false;
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed) return;

			// Dispose of managed resources here.
			if (disposing)
			{
				if (_Client != null)
					_Client.Dispose();
			}

			// Dispose of any unmanaged resources not wrapped in safe handles.

			disposed = true;
		}
		#endregion IDisposable
	}
}

