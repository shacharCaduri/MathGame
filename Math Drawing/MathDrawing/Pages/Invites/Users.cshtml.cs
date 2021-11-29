using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathDrawing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MathDrawing.Classes;

namespace MathDrawing.Pages
{
    public class Users
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public UsersModel(ApplicationDbContext context, UserManager<IdentityUser> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }

        public IList<Users> Users = new List<Users>();
        public async Task OnGetAsync()
        {
            var appUsers = _context.Users.ToList();
            var admin = await _userManager.GetUserAsync(User);
            foreach (var user in appUsers)
            {
                Users X = new Users
                {
                    Name = user.NormalizedUserName,
                    Id = user.Id
                };
                if (X.Name != admin.NormalizedUserName)
                    Users.Add(X);
            }
        }
    }
}