using Jso.Annotationary.Application.DTOs.User;
using Jso.Annotationary.Application.Interfaces;
using Jso.Annotationary.Domain.Response;
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

        // ==============================================================
        // Get all users
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            var response = ApiResponse<IEnumerable<UserResponseDto>>.Success(
                data: users,
                message: "Users retrieved successfully"
            );
            
            return Ok(response);
        }

        // ==============================================================
        // Get user by id
        // ==============================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var users = await _userService.GetByIdAsync(id);

            if (users == null)
            {
                var errorResponse = ApiResponse<UserResponseDto>.Failure(
                    errors: new List<string> { $"User with ID {id} was not found." },
                    message: "User not found",
                    statusCode: 404
                );
                
                return NotFound(errorResponse);
            }

            var successResponse = ApiResponse<UserResponseDto>.Success(
                data: users,
                message: "User retrieved successfully"
            );
            
            return Ok(successResponse);
        }
    }
}
