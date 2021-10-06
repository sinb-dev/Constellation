using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constellation_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContainerController : ControllerBase
    {
        public class Response {
            public string message {get;set;}
        }
        private readonly ILogger<ContainerController> _logger;

        public ContainerController(ILogger<ContainerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Response Get(string myrequest="")
        {
            Response response = new();
            if (myrequest != "")
                response.message = myrequest;
            else
                response.message = "Du er bare SÅ dygtig!!!";
            return response;
        }
    }
}
