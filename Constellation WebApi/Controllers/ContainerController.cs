using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

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
        public ContainerResponse Run(string userId, string image, int port, string name)
        {
            var result = ContainerHandler.Run(image, port, name);
            ContainerResponse response = new();
            response.message = result;
            
            return response;
        }

        [HttpGet]
        public string Command(string arguments)
        {
            System.Diagnostics.Process.Start("docker",arguments);
            return "OK";
        }
    }
}
