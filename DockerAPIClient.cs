﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DockerApiDotNet.Data;

namespace DockerApiDotNet
{
	public class DockerAPIClient : IDisposable
	{
		HttpOverSocketRequest _Request = null;

		public DockerAPIClient(string destination)
		{
			_Request = HttpOverSocketRequest.Create(destination);

			if (_Request == null)
				throw new ArgumentException("Can't connect to socket at \"" + destination + "\".");
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
				path += "&since=" + System.Net.WebUtility.UrlEncode(since);
			if (before != null)
				path += "&before=" + System.Net.WebUtility.UrlEncode(before);

			_Request.Path = path;
			HttpOverSocketResponse response = _Request.GetResponse();

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				List<Container> ret = JsonConvert.DeserializeObject<List<Container>>(response.Content);

				return ret.ToArray();
			}
			else
				throw new Exception("Server error : " + response.StatusCode + " " + response.StatusDescription);
		}

		public ContainerDetails GetContainer(string id)
		{
			string path = string.Format("/containers/{0}/json", id);

			_Request.Path = path;
			HttpOverSocketResponse response = _Request.GetResponse();

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ContainerDetails ret = JsonConvert.DeserializeObject<ContainerDetails>(response.Content);

				return ret;
			}
			else
				throw new Exception("Server error : " + response.StatusCode + " " + response.StatusDescription);
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
				if (_Request != null)
					_Request.Dispose();
			}

			// Dispose of any unmanaged resources not wrapped in safe handles.

			disposed = true;
		}
		#endregion IDisposable
	}
}
