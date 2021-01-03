using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AGooday.DncZero.RazorPagesMovie.Data;
using AGooday.DncZero.RazorPagesMovie.Models;

namespace AGooday.DncZero.RazorPagesMovie.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly AGooday.DncZero.RazorPagesMovie.Data.AGoodayDncZeroRazorPagesMovieContext _context;

        public CreateModel(AGooday.DncZero.RazorPagesMovie.Data.AGoodayDncZeroRazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
