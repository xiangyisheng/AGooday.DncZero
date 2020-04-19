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
    public class MenusRepository : Repository<Menus,Guid>, IMenusRepository
    {
        public MenusRepository(DncZeroDbContext context)
            : base(context)
        {
        }
    }
}
