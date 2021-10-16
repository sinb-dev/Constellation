using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var result = ContainerHandler.Run(image, port, "Jensns");
            ContainerResponse response = new();
            response.message = result;
            
            return response;
        }
    }
}
