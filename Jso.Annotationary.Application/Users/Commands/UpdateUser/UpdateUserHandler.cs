using AutoMapper;
using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Errors;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Domain.Response;
using MediatR;

namespace Jso.Annotationary.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user is active
            var activeUser = await _userRepository.CheckIsUserActiveAsync(request.UserId);

            if (!activeUser)
            {
                return Result<User>.Failure(DomainErrors.User.UserInActive);
            }
            
            // Get user
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<User>.Failure(DomainErrors.User.NotFound);
            }
            
            // Map request -> existing tracked entity
            _mapper.Map(request, user);
            
            // Update fields
            await _userRepository.UpdateAsync(user);
            
            return Result<User>.Success(user);
        }
    } 
}