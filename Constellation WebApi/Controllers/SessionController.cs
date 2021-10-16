using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Constellation_WebApi.SessionHandling;
using System.Threading.Tasks;

namespace Constellation_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        
        private readonly ILogger<ContainerController> _logger;

        public SessionController(ILogger<ContainerController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<bool> Login(string username, string password) 
        {
            if (true)
            {
                Session session = new();
                SessionManager.Add(session);
            }
            return true;
        }
    }
}
