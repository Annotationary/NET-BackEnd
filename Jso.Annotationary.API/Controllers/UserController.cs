using Jso.Annotationary.Application.Users.Commands;
using Jso.Annotationary.Application.Users.Queries;
using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Response;
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
            var result = await _mediator.Send(command);

            // Fail
            if (result.IsFailure)
            {
                var errorResponse = ApiResponse<User>.Failure(
                    new List<string>() {result.Error.Message},
                    "Cannot create user",
                    400
                    );
                return BadRequest(errorResponse);
            }
            
            // Success
            var successResponse = ApiResponse<User>.Success(
                result.Value,
                "User created successfully",
                201
                );

            return CreatedAtAction(
                nameof(GetAllUsers), 
                new { id= result.Value }, 
                successResponse
                );
        }

        /// <summary> Get user details with projects. </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUserQuery());
            return Ok(result);
        }

        /// <summary> Get user by userid. </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
