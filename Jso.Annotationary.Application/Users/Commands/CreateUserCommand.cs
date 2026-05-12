using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Application.Users.Commands
{
    public record CreateUserCommand(
        string UserName,
        string Email,
        string Password,
        string? AvatarUrl,
        string? CoverImageUrl,
        string? Specialization
    ) : IRequest<Guid> ;
}
