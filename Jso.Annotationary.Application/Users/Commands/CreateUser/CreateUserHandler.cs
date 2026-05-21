using AutoMapper;
using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Errors;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Domain.Response;
using MediatR;

namespace Jso.Annotationary.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // No duplicated email
            var emailExists = await _userRepository.CheckEmailExistsAsync(request.Email);
            if (emailExists)
            {
                return Result<User>.Failure(DomainErrors.User.EmailInUse);
            }
            
            var user = _mapper.Map<User>(request);

            await _userRepository.AddAsync(user);
            return Result<User>.Success(user);
        }
    }
}
