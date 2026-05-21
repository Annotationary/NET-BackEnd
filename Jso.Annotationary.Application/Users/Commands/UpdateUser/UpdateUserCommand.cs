using System.Text.Json.Serialization;
using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Response;
using MediatR;

namespace Jso.Annotationary.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        [property : JsonIgnore]
        Guid UserId,
        string UserName,
        string Email,
        string? AvatarUrl,
        string? CoverImageUrl,
        string Specialization
        ) : IRequest<Result<User>>;
}

