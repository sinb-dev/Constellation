using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ductus.FluentDocker;
using Ductus.FluentDocker.Services;
using System.Linq;
using Ductus.FluentDocker.Builders;
using System.Net.NetworkInformation;
using Constellation_WebApi.SessionHandling;

namespace Constellation_WebApi
{
    public static class ContainerHandler
    {
        static IHostService client;
        static ContainerHandler() 
        {
            var hosts = new Hosts().Discover();
            client = hosts.FirstOrDefault(x=>x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
        }
        const string REPOSITORY = "docker.data.techcollege.dk";
        //public static async Task<string> Run(string image, int container_port)
        public static string Run(string userId, ContainerDefinition conDef) 
        {
            if (conDef == null) {
                Logger.Error($"Attempted to run container for userId, but the container definition was null");
                return "error";
            }
            return Run(conDef.image, conDef.port,
                $"{conDef.prefix}_{userId}");
        }
        public static string Run(string image, int container_port, string container_name)
        {
            image = sanitizeImageName(image);
            int host_port = 0;
            int lower_bound = 10000;
            int upper_bound = 60000;
            if (!getAvailablePortOS(out host_port, lower_bound, upper_bound)) 
            {
                return $"Failed to find an available port between {lower_bound} and {upper_bound}";
            }
            var builder = new Builder();
            try
            {
               new Builder().UseContainer()
                    .UseImage($"{REPOSITORY}/{image}")
                    .WithName(container_name)
                    .ExposePort(host_port,container_port)
                    .Build()
                    .Start();
            } 
            catch (Exception e) 
            {
                return e.Message;
            }
            return "";
        }

        public static async Task<Dictionary<string,string>> GetStatus(string userId)
        {
            var defs = UserManager.GetContainerDefinitions(userId).Result;
            var ps = Ps();

            Dictionary<string,string> response = new();
            foreach (var def in defs) 
            {
                string state = "NotStarted";
                foreach (var service in ps) 
                {
                    if (service.Name == def.prefix+"_"+userId) {
                        state = service.State.ToString();
                    }
                }
                response[def.prefix] = state;
            }
            return response;
        }

        public static IList<Ductus.FluentDocker.Services.IContainerService> Ps() 
        {
            var builder = new Builder();
            try
            {
                var hosts = new Hosts().Discover();
                var _docker = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
                return _docker.GetRunningContainers();

            } 
            catch (Exception e) 
            {
                return new List<Ductus.FluentDocker.Services.IContainerService>();
            }
        }

        static bool getAvailablePortOS(out int port,int min, int max)
        {
            HashSet<int> occupiedPorts = getOccupiedPorts();
            while (occupiedPorts.Contains(min)) {
                min++;
                if (min > max) {
                    port = -1;
                    return false;
                }
            }
            port = min;
            return true;
        }
        static HashSet<int> getOccupiedPorts() 
        {
            HashSet<int> list = new();
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                list.Add(tcpi.LocalEndPoint.Port);
            }

            var conts = client.GetContainers();
            foreach (var c in conts) {
                foreach (KeyValuePair<string, Ductus.FluentDocker.Model.Containers.HostIpEndpoint[]> kv in c.GetConfiguration().NetworkSettings.Ports) {
                    foreach (var r in kv.Value)
                    {
                        list.Add(r.Port);
                    }
                }
            }
            return list;
        }
        private static string sanitizeImageName(string image)
        {
            return image
                .Replace("/","")
                .Replace("..", "")
                .Replace(@"\","");
        }

        public static void Stop(string containerName)
        {

        }
    }
}