using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Queries
{
    public class GetByIdQuery<T> : IRequest<T>
    {
        public Guid Id { get; private set; }

        public GetByIdQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
