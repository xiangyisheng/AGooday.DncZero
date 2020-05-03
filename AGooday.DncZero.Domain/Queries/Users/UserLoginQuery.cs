using AGooday.DncZero.Domain.Communication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Queries.Users
{
    public class UserLoginQuery : IRequest<Response<Models.Users>>
    {
        public UserLoginQuery(string identifier, string credential)
        {
            Identifier = identifier;
            Credential = credential;
        }
        public string Identifier { get; private set; }
        public string Credential { get; private set; }
    }
}
