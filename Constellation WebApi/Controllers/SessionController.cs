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
        public async Task<LoginResponse> Login(string username, string password) 
        {
            if (true)
            {
                Session session = new();
                SessionManager.Add(session);
                return LoginResponse.Success(session.SessionId);
            }
            return LoginResponse.Failed();
        }
        public class LoginResponse
        {
            public string Message {get;set;}
            public string SessionId {get;set;}
            public static LoginResponse Success(string sessionId) => new LoginResponse() {Message = "OK", SessionId = sessionId };
            public static LoginResponse Failed() => new LoginResponse() {Message = "Login failed", SessionId = "" };
        }
    }
}
