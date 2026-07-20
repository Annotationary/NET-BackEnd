using Jso.Annotationary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Application.DTOs.User
{
    public class UpdateUserDto
    {
        public string? UserName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Specialization { get; set; }
        public UserRole UserRole { get; set; }
    }
}
