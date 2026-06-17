using AutoMapper;
using Jso.Annotationary.Application.DTOs.User;
using Jso.Annotationary.Domain.Entities;

namespace Jso.Annotationary.Application.Mappings;

/// <summary>
///
/// A UserMapping configuration class or profile defines the explicit rules for transforming data between your internal
///  User domain entity and external objects like the UserResponseDto
///
/// It handles the tedious job of copying property values (e.g., matching User.Email to UserResponseDto.Email) automatically
/// so you do not have to write manual assignment code every time.
/// </summary>
public class UserMapping : Profile
{
    public UserMapping()
    {
        // Entity -> Response DTO
        CreateMap<User, UserResponseDto>();
        
        // Create DTO -> Entity
        CreateMap<CreateUserDto, User>();

        // Update DTO -> Entity
        CreateMap<UpdateUserDto, User>();
    }
}