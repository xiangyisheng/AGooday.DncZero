using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AGooday.DncZero.RazorPagesMovie.Data;
using AGooday.DncZero.RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AGooday.DncZero.RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly AGooday.DncZero.RazorPagesMovie.Data.AGoodayDncZeroRazorPagesMovieContext _context;

        public IndexModel(AGooday.DncZero.RazorPagesMovie.Data.AGoodayDncZeroRazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                /**
                 * 备注
                 * Contains 方法在数据库中运行，而不是在 C# 代码中运行。 查询是否区分大小写取决于数据库和排序规则。 
                 * 在 SQL Server 上，Contains 映射到 SQL LIKE，这是不区分大小写的。 
                 * 在 SQLite 中，由于使用了默认排序规则，因此需要区分大小写。
                 */
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());

            //Movie = await _context.Movie.ToListAsync();
            Movie = await movies.ToListAsync();
        }
    }
}
