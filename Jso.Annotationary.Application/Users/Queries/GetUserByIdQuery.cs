using Jso.Annotationary.Application.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Application.Users.Queries;
public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto?>;
