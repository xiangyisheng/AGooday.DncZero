using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Queries
{
    public class GetByIdQuery<TEntity, TPrimaryKey> : IRequest<TEntity>
    {
        public TPrimaryKey Id { get; private set; }

        public GetByIdQuery(TPrimaryKey id)
        {
            this.Id = id;
        }
    }
}
