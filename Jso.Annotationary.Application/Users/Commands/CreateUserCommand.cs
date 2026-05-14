using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Response;
using MediatR;

namespace Jso.Annotationary.Application.Users.Commands
{
    public record CreateUserCommand(
        string UserName,
        string Email,
        string Password,
        string? AvatarUrl,
        string? CoverImageUrl,
        string? Specialization
    ) : IRequest<Result<User>> ;
}
