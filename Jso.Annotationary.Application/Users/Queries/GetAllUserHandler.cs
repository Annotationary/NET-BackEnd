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
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, Result<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var mappedUsers = _mapper.Map<List<UserDto>>(users);
            return Result<List<UserDto>>.Success(mappedUsers);
        }
    }
}
