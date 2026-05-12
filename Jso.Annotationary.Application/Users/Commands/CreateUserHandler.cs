using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Application.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.UserName,
                Email = request.Email,
                Password = request.Password,
                AvatarUrl = request.AvatarUrl,
                Specialization = request.Specialization,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRepository.AddAsync(user);
            return user.UserId;
        }
    }
}
