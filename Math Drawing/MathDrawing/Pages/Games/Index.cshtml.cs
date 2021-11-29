using MathDrawing.Classes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathDrawing.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly MathDrawing.Data.ApplicationDbContext _context;

        public IndexModel(MathDrawing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get; set; }

        public async Task OnGetAsync()
        {
            Game = await _context.Games
                .Include(x => x.Creator)
                .ToListAsync();
        }
    }
}
