using Jso.Annotationary.Application.Users.DTOs;
using Jso.Annotationary.Domain.Response;
using MediatR;

namespace Jso.Annotationary.Application.Users.Queries;
public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDto?>>;
