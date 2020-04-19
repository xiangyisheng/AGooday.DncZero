using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGooday.DncZero.Infrastructure.Repository
{
    public class RolesRepository : Repository<Roles, Guid>, IRolesRepository
    {
        public RolesRepository(DncZeroDbContext context)
            : base(context)
        {
        }
    }
}
