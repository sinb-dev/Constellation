using Docker.DotNet;
using Docker.DotNet.Models;
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
        public async Task<ContainerResponse> Run(string userId, string image, int port)
        {
            
            var result = await ContainerHandler.Run(image, port, 10000);
            ContainerResponse response = new();
            response.message = result;
            
            return response;
        }

        [HttpGet("query")]
        public async Task<string> Query(string containerName)
        {
            ContainerListResponse container = await ContainerHandler.QueryContainer(containerName);
            
            return JsonSerializer.Serialize(container);;
        }
    }
}
