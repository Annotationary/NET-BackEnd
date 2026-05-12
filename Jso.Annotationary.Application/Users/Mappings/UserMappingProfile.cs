using AutoMapper;
using Jso.Annotationary.Application.Users.DTOs;
using Jso.Annotationary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Application.Users.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserStatus, opt => opt.MapFrom(scr => scr.UserId.ToString()))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(scr => scr.UserId.ToString()));
        }
    }
}
