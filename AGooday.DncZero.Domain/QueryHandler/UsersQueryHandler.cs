using AGooday.DncZero.Domain.Communication;
using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Domain.Queries;
using AGooday.DncZero.Domain.Queries.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AGooday.DncZero.Domain.QueryHandler
{
    public class UsersQueryHandler :
         IRequestHandler<GetByIdQuery<Users>, Users>,
         IRequestHandler<UserLoginQuery, Response<Users>>,
         IRequestHandler<UserListQuery, IEnumerable<Users>>
    {
        private readonly IUsersRepository _usersRepository;

        public UsersQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Users> Handle(GetByIdQuery<Users> request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            return await _usersRepository.FindByIdAsync(request.Id);
        }

        public async Task<IEnumerable<Users>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.ListAsync();
        }

        public async Task<Response<Users>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.LoginAsync(request.Identifier, request.Credential);

            if (user == null)
            {
                return new Response<Users>("User not found.");
            }

            return new Response<Users>(user);
        }
    }
}
