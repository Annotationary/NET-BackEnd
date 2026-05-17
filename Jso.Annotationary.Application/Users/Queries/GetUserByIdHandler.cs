using AutoMapper;
using Jso.Annotationary.Application.Users.DTOs;
using Jso.Annotationary.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jso.Annotationary.Domain.Response;

namespace Jso.Annotationary.Application.Users.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto?>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            var mappedUser = _mapper.Map<UserDto>(user);

            if (user == null)
            {
                return null;
            }
            
            return Result<UserDto?>.Success(mappedUser);
        }
    }
}
