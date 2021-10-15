using System;
using System.Diagnostics;
using System.Collections.Generic;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Threading.Tasks;

namespace Constellation_WebApi
{
    public static class ContainerHandler
    {
        static DockerClient client;
        static ContainerHandler() 
        {
            if (System.Environment.OSVersion.Platform == PlatformID.Unix)
            {
                client = new DockerClientConfiguration(
                    new Uri("unix:///var/run/docker.sock"))
                    .CreateClient();
            } 
            else 
            {
                client = new DockerClientConfiguration(
                    new Uri("npipe://./pipe/docker_engine"))
                    .CreateClient();
            }
        }
        const string REPOSITORY = "docker.data.techcollege.dk";

        public static async Task<string> Run(string image, int containerPort=0, int hostPort=0)
        {
            string repo_image = REPOSITORY+"/"+image;
            await client.Images
                .CreateImageAsync(new ImagesCreateParameters
                    {
                        FromImage = repo_image,
                        Tag = "latest"
                    },
                    new AuthConfig(),
                    new Progress<JSONMessage>());
            Dictionary<string, EmptyStruct> exposedPort = null;
            HostConfig hostConfig = new HostConfig();
            if (containerPort != 0) 
            {
                exposedPort = new Dictionary<string, EmptyStruct>
                    {
                        {
                            containerPort+"", default(EmptyStruct)
                        }
                    };
                hostConfig.PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {"8000", new List<PortBinding> {new PortBinding {HostPort = "8000"}}}
                };
                hostConfig.PublishAllPorts = true;
            }

            var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters
                {
                    Image = repo_image,
                    ExposedPorts = exposedPort,
                    HostConfig = hostConfig
                });


            return "";
        }
        public static string RunCmd(string image)
        {
            string name = "Cont";
            int internalPort = 8080;
            int nextAvailablePort = 10000;

            string arguments = $"run --rm --name {name} -p {internalPort}:{nextAvailablePort} {REPOSITORY}/{image}";

            var output = docker_command(arguments);
            Console.WriteLine("Container started");
            QueryContainer(name);
            return "";
        }
        public static void Stop(string containerName)
        {

        }
        public static async Task<ContainerListResponse> QueryContainer(string containerName)
        {
            DockerClient client = new DockerClientConfiguration()
              .CreateClient();
            try {
                IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
                    new ContainersListParameters(){
                        Limit = 10,
                });
                containerName = "/" + containerName;
                foreach (var c in containers)
                {
                    foreach (var s in c.Names)
                    {
                        if (s == containerName)
                            return c;
                    }
                }
            } catch {
                Console.WriteLine("Oh bugger");
            }
            return null;
        }
        private static List<string> docker_command(string arguments)
        {
            List<string> output_lines = new List<string>();
            var p = new Process 
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            p.Start();
            while (!p.StandardOutput.EndOfStream)
            {
                output_lines.Add(p.StandardOutput.ReadLine());
            }
            return output_lines;
        }
    }
}