using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Constellation_WebApi.SessionHandling;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Constellation_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly ILogger<ContainerController> _logger;

        public UserController(ILogger<ContainerController> logger)
        {
            _logger = logger;
        }

        [HttpPut("create")]
        public async Task<bool> Create(string username, string password, string course) 
        {
            return await UserManager.Create(username, password, course);
        }
        
        [HttpPost("update")]
        public async Task<bool> Update(string id, string password) 
        {
            return await UserManager.Update(id, password);
        }

        [HttpDelete("delete")]
        public async Task<bool> delete(string id) 
        {
            return await UserManager.Remove(id);
        }

        [HttpPost("list")]
        public async Task<List<UserDocument>> list() 
        {
            return await UserManager.List();
        }
    }
    
}
