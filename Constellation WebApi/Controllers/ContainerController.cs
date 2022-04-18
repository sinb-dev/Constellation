using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Constellation_WebApi.SessionHandling;

namespace Constellation_WebApi.Controllers
{
    public class ContainerResponse {
        public string message {get;set;}
    }
    [ApiController]
    [Route("[controller]")]
    public class ContainerController : ControllerBase
    {
        
        private readonly ILogger<ContainerController> _logger;

        public ContainerController(ILogger<ContainerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ContainerResponse Run(string userId, string prefix)
        {
            var defs = UserManager.GetContainerDefinitions(userId).Result;
            ContainerResponse response = new();
            response.message = $"No definitions by the name of {prefix} was found";
            foreach (var d in defs) {
                if (d.prefix == prefix) {
                    response.message = ContainerHandler.Run(userId, d);
                }
            }

            return response;
        }
        [HttpGet("status")]
        public Task<Dictionary<string,string>> Status(string userId) 
        {
            return ContainerHandler.GetStatus(userId);
        }
        [HttpGet("command")]
        public string Command(string arguments)
        {
            System.Diagnostics.Process.Start("docker",arguments);
            return "OK";
        }
    }
}
