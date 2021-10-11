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

        public static async Task<string> Run(string image)
        {
            await client.Images
        .CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = "docker.data.techcollege.dk/hello-world",
                Tag = "latest"
            },
            new AuthConfig(),
            new Progress<JSONMessage>());
            

            var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters
                {
                    Image = "docker.data.techcollege.dk/hello-world",
                    ExposedPorts = new Dictionary<string, EmptyStruct>
                    {
                        {
                            "8000", default(EmptyStruct)
                        }
                    },
                    HostConfig = new HostConfig
                    {
                        PortBindings = new Dictionary<string, IList<PortBinding>>
                        {
                            {"8000", new List<PortBinding> {new PortBinding {HostPort = "8000"}}}
                        },
                        PublishAllPorts = true
                    }
                });


            return "";
            
            //ContainerResponse response = new();
            //return response;
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
        public static async void QueryContainer(string containerName)
        {
            DockerClient client = new DockerClientConfiguration()
              .CreateClient();
            try {
                IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
                    new ContainersListParameters(){
                        Limit = 10,
                    });

                    foreach (var c in containers)
                    {
                        foreach (var s in c.Names)
                        {
                            Console.WriteLine("Container. "+s);
                        }
                    }
                    } catch {
                        Console.WriteLine("Oh bugger");
                    }
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