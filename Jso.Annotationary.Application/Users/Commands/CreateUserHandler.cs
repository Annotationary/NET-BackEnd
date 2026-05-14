using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Errors;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Domain.Response;
using MediatR;

namespace Jso.Annotationary.Application.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // No duplicated email
            var emailExists = await _userRepository.CheckEmailExistsAsync(request.Email);
            if (emailExists)
            {
                return Result<User>.Failure(DomainErrors.User.EmailInUse);
            }
            
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.UserName,
                Email = request.Email,
                Password = request.Password,
                AvatarUrl = request.AvatarUrl,
                CoverImageUrl =  request.CoverImageUrl,
                Specialization = request.Specialization,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRepository.AddAsync(user);
            return Result<User>.Success(user);
        }
    }
}
