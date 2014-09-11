DockerApiDotNet
===============

dotNet wrapper for the docker.io REST api.

Currently only `GET /containers/json` and `GET /containers/{id}/json` request are implemented.

## Usage

```
using DockerApiDotNet;
using DockerApiDotNet.Data;

DockerAPIClient client = new DockerAPIClient("tcp://127.0.0.1:1234");

Container[] containers = client.GetContainers();

foreach (Container c in containers)
  Console.WriteLine(c.ShortID + "(" + c.Name + ")");
```

DockerAPIClient can connect to unix socket (eg `unix:///var/run/docker.sock`) when compiled/run with mono or tcp (eg `tcp://127.0.0.1:1234`).
