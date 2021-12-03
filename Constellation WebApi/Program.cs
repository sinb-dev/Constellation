using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constellation_WebApi.SessionHandling;

namespace Constellation_WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            /*var defs = UserManager.GetContainerDefinitions("Anne","H4PD101121").Result;
            foreach (var d in defs) {
                if (d.prefix == "possum") {
                    ContainerHandler.Run($"Anne_H4PD101121", d);
                }
            }*/
            ContainerHandler.GetStatus("Anne_H4PD101121");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
