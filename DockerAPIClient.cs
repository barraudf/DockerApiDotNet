using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DockerApiDotNet.Data;

namespace DockerApiDotNet
{
	public class DockerAPIClient
	{
		private string _Destination = null;

		public DockerAPIClient(string destination)
		{
			if (string.IsNullOrEmpty(destination))
				throw new ArgumentException("The destination argument can not be null or empty.");

			_Destination = destination;
		}

		public Container[] GetContainers(bool size = false, bool all = false, int limit = 0, string since = null, string before = null)
		{
			string path = "/containers/json?";

			if (size)
				path += "&size=1";
			if (all)
				path += "&all=1";
			if (limit > 0)
				path += "&limit=" + limit;
			if (since != null)
				path += "&since=" + WebUtility.UrlEncode(since);
			if (before != null)
				path += "&before=" + WebUtility.UrlEncode(before);

			List<Container> ret = GetJsonResponse<List<Container>>(path);

			return ret.ToArray();
		}

		public ContainerDetails GetContainer(string id)
		{
			string path = string.Format("/containers/{0}/json", id);

			ContainerDetails ret = GetJsonResponse<ContainerDetails>(path);

			return ret;
		}

		public ContainerProcesses GetContainerTop(string id, string opts = "")
		{
			string path = string.Format("/containers/{0}/top", id);

			if (!string.IsNullOrEmpty(opts))
				path += "?ps_args=" + WebUtility.UrlEncode(opts);

			ContainerProcesses ret = GetJsonResponse<ContainerProcesses>(path);

			return ret;
		}

		public ContainerDiff[] GetContainerDiff(string id)
		{
			string path = string.Format("/containers/{0}/changes", id);

			ContainerDiff[] ret = GetJsonResponse<ContainerDiff[]>(path);

			return ret;
		}

		public VersionInfo GetVersion()
		{
			VersionInfo ret = GetJsonResponse<VersionInfo>("/version");
			return ret;
		}

		public SystemInfo GetInfo()
		{
			SystemInfo ret = GetJsonResponse<SystemInfo>("/info");
			return ret;
		}

		internal void ProcessResponseCode(HttpOverSocketResponse response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
				throw new Exception(string.Format("Server error : {0}({1}) - {2}", response.StatusCode, response.StatusCode.ToString(), response.StatusDescription));
		}

		protected T GetJsonResponse<T>(string path)
		{
			using (HttpOverSocketRequest request = HttpOverSocketRequest.Create(_Destination))
			{

				if (request == null)
					throw new ArgumentException("Can't connect to socket at \"" + _Destination + "\".");
				request.Path = path;
				HttpOverSocketResponse response = request.GetResponse();

				ProcessResponseCode(response);

				T ret = JsonConvert.DeserializeObject<T>(response.Content);

				return ret;
			}
		}
	}
}
