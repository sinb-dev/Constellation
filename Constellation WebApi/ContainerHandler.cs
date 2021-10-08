using System;
using System.Diagnostics;
using System.Collections.Generic;
using Docker.DotNet;
using Docker.DotNet.Models;
namespace Constellation_WebApi
{
    public static class ContainerHandler
    {
        const string REPOSITORY = "docker.data.techcollege.dk";
        public static string Run(string image)
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