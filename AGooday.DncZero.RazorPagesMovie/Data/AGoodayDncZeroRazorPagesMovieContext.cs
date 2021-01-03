using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AGooday.DncZero.RazorPagesMovie.Models;

namespace AGooday.DncZero.RazorPagesMovie.Data
{
    public class AGoodayDncZeroRazorPagesMovieContext : DbContext
    {
        public AGoodayDncZeroRazorPagesMovieContext (DbContextOptions<AGoodayDncZeroRazorPagesMovieContext> options)
            : base(options)
        {
        }

        public DbSet<AGooday.DncZero.RazorPagesMovie.Models.Movie> Movie { get; set; }
    }
}
