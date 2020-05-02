using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Queries.Users
{
    public class ListUsersQuery : IRequest<IEnumerable<Models.Users>>
    {
    }
}
