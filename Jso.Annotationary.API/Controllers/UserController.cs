using Jso.Annotationary.Application.Users.Commands;
using Jso.Annotationary.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jso.Annotationary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary> Create a new user. </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllUsers), new { id }, new { id });
        }

        /// <summary> Get user details with projects. </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetAllUserQuery());
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
