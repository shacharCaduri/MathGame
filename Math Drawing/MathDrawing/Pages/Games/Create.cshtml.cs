using MathDrawing.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace MathDrawing.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly MathDrawing.Data.ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        public CreateModel(MathDrawing.Data.ApplicationDbContext context, UserManager<IdentityUser> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Game Game { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
          

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Game.Creator = await _userManager.GetUserAsync(User);

            if (IsNameValid(Game.Name))
            {
                _context.Games.Add(Game);
                await _context.SaveChangesAsync();
            }
            else {
                //StringWriter str = new StringWriter();
                //str.Write("Invalid Name! Plaese Enter New Name!");
                //Console.WriteLine(str);
                return Page();
            }
            return RedirectToPage("./Index");
        }

        public bool IsNameValid(string name)
        {
            var namesGame = _context.Games.Select(x => x.Name).ToList();

            foreach (var n in namesGame)
            {
                if (name == n)
                    return false;
            }
            return true;
        }
    }
}
