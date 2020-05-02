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
         IRequestHandler<ListUsersQuery, IEnumerable<Users>>
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
        public async Task<IEnumerable<Users>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.ListAsync();
        }
    }
}
