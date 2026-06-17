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
            var result = await _userService.GetByIdAsync(id);

            if (result.IsFailure)
            {
                return NotFound(
                    ApiResponse<UserResponseDto>.Failure(
                        errors: new List<string> { result.Error.Message },
                        message: "User not found",
                        statusCode: 404
                    )
                );
            }
            
            return Ok(
                ApiResponse<UserResponseDto>.Success(
                    data: result.Value,
                    message: "User retrieved successfully"
                )
            );
        }
        
        // ==============================================================
        // Create new user
        // ==============================================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponse<object>.Failure(errors, "Validation failed", 400));
            }

            await _userService.AddAsync(createUserDto);
            
            var successResponse = ApiResponse<object>.Success(null, "User created successfully", 201);
            return StatusCode(201, successResponse);
        }

        // ==============================================================
        // Update user
        // ==============================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponse<object>.Failure(errors, "Validation failed", 400));
            }

            try
            {
                await _userService.UpdateAsync(id, updateUserDto);
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<object>.Failure(new List<string> { ex.Message }, "User not found", 404));
            }

            var successResponse = ApiResponse<object>.Success(null, "User updated successfully");
            return Ok(successResponse);
        }

        // ==============================================================
        // Delete user
        // ==============================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            
            var successResponse = ApiResponse<object>.Success(null, "User deleted successfully");
            return Ok(successResponse);
        }
    }
}
