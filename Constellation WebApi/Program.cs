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
            //ContainerHandler.Run("possum", 8080, "possum");
            await UserManager.Create("Anne","dam", "H4PD101121");
            var result = await UserManager.Login("Anne","Dam");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
