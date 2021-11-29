using MathDrawing.Classes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MathDrawing.Pages.Games
{
    public class ScoreTableModel : PageModel
    {
        private readonly MathDrawing.Data.ApplicationDbContext _context;

        public ScoreTableModel(MathDrawing.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string GameName { get; set; }

        public IList<Player> PlayersInGame { get; set; }

        public async Task OnGetAsync(int roomId = 0)
        {
            PlayersInGame = await _context.Players
                .Include(x => x.PlayerUser).Where(x => x.GameId == roomId)
                .ToListAsync();
            GameName= _context.Games.Where(x => x.GameID == roomId).FirstOrDefault().Name;
        }
    }
}










//using System;
//using System.CodeDom;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using MathDrawing.Classes;
//using MathDrawing.Data;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.SignalR;
//using SQLitePCL;





//namespace MathDrawing.Pages.Games
//{
//    public class ScoreTableModel : PageModel
//    {
//        private readonly ApplicationDbContext _context;
//        private UserManager<IdentityUser> _userManager;

//        public ScoreTableModel(ApplicationDbContext context, UserManager<IdentityUser> UserManager)
//        {
//            _context = context;
//            _userManager = UserManager;
//        }

//        public List<Player> PlayersInGame { get; set; }
//        public List<CurrentState> GameState { get; set; }
//        //public string GameName { get; set; }

//        public void OnGet(int roomId = 0)
//        {
//            PlayersInGame = _context.Players.Where(x => x.GameId == roomId).ToList();  
//            GameState = _context.MyState.Where(x => x.MyGameID == roomId).ToList();
//        }
//    }
//}