using AGooday.DncZero.Common.Enumerator;
using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGooday.DncZero.Infrastructure.Repository
{
    public class UserAuthsRepository : Repository<UserAuths, Guid>, IUserAuthsRepository
    {
        public UserAuthsRepository(DncZeroDbContext context)
            : base(context)
        {
        }
    }
}
