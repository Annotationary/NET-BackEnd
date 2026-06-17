using Jso.Annotationary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jso.Annotationary.API.Controllers
{
    /// <summary>
    /// A UserController inside the API project is the entry point for all HTTP requests related to user management. It acts
    /// as a traffic controller that receives incoming requests from clients (like a web or mobile app), extracts the
    /// necessary data, calls the appropriate application service, and returns the final HTTP response.
    /// </summary>
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
    }
}
