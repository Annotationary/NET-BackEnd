using AutoMapper;
using Jso.Annotationary.Application.DTOs.User;
using Jso.Annotationary.Application.Interfaces;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Errors;
using Jso.Annotationary.Domain.Response;

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

        // ==============================================================
        // Add new user service
        // ==============================================================
        public async Task<Result> AddAsync(CreateUserDto createUserDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);

            if (existingUser != null)
            {
                return Result.Failure(
                    DomainErrors.User.EmailInUse
                );
            }
            
            var user = _mapper.Map<User>(createUserDto);
            await _userRepository.AddAsync(user);
            return  Result.Success();
        }

        // ==============================================================
        // Remove user service
        // ==============================================================
        public async Task<Result> DeleteAsync(Guid id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                return Result.Failure(
                    DomainErrors.User.NotFound
                );
            }
            
            await _userRepository.DeleteAsync(id);
            return Result.Success();
        }

        // ==============================================================
        // Get all user service
        // ==============================================================
        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        // ==============================================================
        // Get user by id service
        // ==============================================================
        public async Task<Result<UserResponseDto>> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return Result<UserResponseDto>.Failure(
                    DomainErrors.User.NotFound
                );
            }
            
            return Result<UserResponseDto>.Success(
                    _mapper.Map<UserResponseDto>(user)
                );
        }

        // ==============================================================
        // Update user service
        // ==============================================================
        public async Task<Result> UpdateAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            if (user == null)
            {
                return Result.Failure(
                    DomainErrors.User.NotFound
                );
            }
            
            _mapper.Map(updateUserDto, user);

            await _userRepository.UpdateAsync(user);
            
            return Result.Success();
        }
    }
}
