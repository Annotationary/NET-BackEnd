using AutoMapper;
using Jso.Annotationary.Application.DTOs.User;
using Jso.Annotationary.Application.Interfaces;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Domain.Entities;

namespace Jso.Annotationary.Application.Services
{
    /// <summary>
    ///
    /// The UserService is the concrete class inside the Application project that implements the IUserService interface.
    /// It contains the actual business logic for your use cases, acting as the coordinator that ties your repositories,
    /// domain rules, mapping profiles, and DTOs together.
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,  IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Manual mapping method to convert User entity to UserResponseDto
        // private UserResponseDto MapToResponse(User user)
        // {
        //     return new UserResponseDto
        //     {
        //         UserId = user.UserId,
        //         Username = user.Username,
        //         Email = user.Email,
        //         AvatarUrl = user.AvatarUrl,
        //         CoverImageUrl = user.CoverImageUrl,
        //         Specialization = user.Specialization,
        //         UserStatus = user.UserStatus,
        //         UserRole = user.UserRole,
        //         CreatedAt = user.CreatedAt
        //     };
        // }

        public async Task AddAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<CreateUserDto, User>(createUserDto);
            await _userRepository.AddAsync(user);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }
            
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task UpdateAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var user = _userRepository.GetByIdAsync(id);
            
            if (user == null)
            {
                throw new Exception($"User with id {id} not found");
            }
            
            _mapper.Map(updateUserDto, user);

            await _userRepository.SaveChangeAsync();
        }
    }
}
