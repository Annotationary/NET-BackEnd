using AutoMapper;
using Jso.Annotationary.Application.Users.Commands.UpdateUser;
using Jso.Annotationary.Application.Users.DTOs;
using Jso.Annotationary.Domain.Entities;

namespace Jso.Annotationary.Application.Users.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Entity -> DTO
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserStatus, 
                    opt => opt.MapFrom(scr => scr.UserStatus.ToString()))
                .ForMember(dest => dest.UserRole, 
                    opt => opt.MapFrom(scr => scr.UserRole.ToString()));
            
            // Update Command -> Entity
            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserStatus,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UserRole,
                    opt => opt.Ignore());
        }
    }
}
