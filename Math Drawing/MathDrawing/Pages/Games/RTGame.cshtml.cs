using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using MathDrawing.Classes;
using MathDrawing.Data;
using MathDrawing.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using SQLitePCL;

namespace MathDrawing.Pages.Games
{
    public class RTGameModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        

        
        public RTGameModel(ApplicationDbContext context, UserManager<IdentityUser> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }

        public int flagRoomNotifications { get; set; }
        public int RoomId2 { get; private set; }
        [BindProperty]
        public Equation Equation { get; set; }
        [BindProperty]
        public List<Equation> EqList { get; set; }
        [BindProperty]
        public Player Player { get; set; }
        public string TimerMin { get; set; }
        public string GName { get; set; }
        public string TimerSec { get; set; }
        public CurrentState state { get; set; }

        //handler methods: ~GET
        public async Task OnGetAsync(int roomId = 0)
        {
            flagRoomNotifications = 0;
            RoomId2 = roomId;
            Equation = null;
            TimerMin = "x";
            TimerSec = "xx";
            await setFirstPlayer(RoomId2);
            createState();
            CurrentState s = _context.MyState.Where(x => x.MyGameID == RoomId2 && x.UserMail == Player.PlayerUser).SingleOrDefault();
            if (s!=null)
            {
                Player p= _context.Players.Where(x => x.GameId == RoomId2 && x.PlayerUser == s.UserMail).SingleOrDefault();
                p.Score = 0;
                s.MyCurrScore = 0;
                s.MyMin = "x";
                s.MySec = "xx";
                s.MyCurrEq = 0;             
            }
            else
            {
                _context.MyState.Add(state);
            }
            _context.SaveChanges();
        }

        public void createState()
        {
            state = new CurrentState()
            {
                MyPlayerID = Player.PlayerID,
                UserMail = Player.PlayerUser,
                MyCurrEq = 0,
                MyCurrScore = 0,
                MyGameID = RoomId2,
                MyMin = TimerMin,
                MySec = TimerSec,
                GameName = _context.Games.Where(x => x.GameID == RoomId2).FirstOrDefault().Name,
            };
            GName = state.GameName;
        }
        public async Task setFirstPlayer(int roomId = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            Player p = _context.Players.Where(x => x.GameId == roomId && x.PlayerUser ==user ).SingleOrDefault();
            if (p == null)
            {
                Player p2 = new Player() { GameId = roomId, Score = 0, PlayerUser = await _userManager.GetUserAsync(User) };
                Player = p2;
                _context.Players.Add(p2);
                await _context.SaveChangesAsync();
            }
            else
                Player = p;
        }

        public void createListAsync(int roomId = 0)
        {
            var MyGame = _context.Games.Where(r => r.GameID == roomId);
            var roomLevel = MyGame.FirstOrDefault().Level;
            var equations = _context.Equations.Where(x => x.Level == roomLevel).ToList();
            foreach (Equation e in equations)
            {
                EqList.Add(new Equation()
                {
                    EquationID = e.EquationID,
                    EquationQ = e.EquationQ,
                    Level = e.Level,
                    Result = e.Result
                });
            }
        }

        public ActionResult OnPostStartAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            flagRoomNotifications = 1;
            var userId = Int32.Parse(Request.Form["PId"]);
            var GameId = Int32.Parse(Request.Form["RId"]);
            GName = Request.Form["GameName"];
            CurrentState state = _context.MyState.Where(x => x.MyGameID == GameId && x.MyPlayerID== userId).SingleOrDefault();
            Player = _context.Players.Where(x => x.PlayerID == state.MyPlayerID && x.GameId == state.MyGameID).SingleOrDefault();
            RoomId2 = GameId;
            TimerMin = "1";
            TimerSec = "00";
            state.MyMin = TimerMin;
            state.MySec = TimerSec;
            _context.SaveChanges();
            createListAsync(RoomId2);
            Equation = EqList[state.MyCurrEq];
            return Page();
        }

        public ActionResult OnPostResultAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var userId = Int32.Parse(Request.Form["PId"]);
            var GameId = Int32.Parse(Request.Form["RId"]);
            GName = Request.Form["GameName"];
            TimerMin = Request.Form["Min"];
            TimerSec = Request.Form["Sec"];
            RoomId2 = GameId;
            CurrentState state = _context.MyState.Where(x => x.MyGameID == GameId && x.MyPlayerID == userId).SingleOrDefault();
            var res = Int32.Parse(Request.Form["result"]);
            createListAsync(RoomId2);
            var checkRes = EqList[state.MyCurrEq].Result;
            Player p = _context.Players.Where(x => x.PlayerID == state.MyPlayerID && x.GameId == state.MyGameID).SingleOrDefault();
            Player = p;
            if (res == checkRes)
            {
                flagRoomNotifications = 2;               
                p.Score += 10;
                state.MyCurrScore +=10;               
            }
            else
                flagRoomNotifications = 3;
            if(state.MyCurrEq<(EqList.Count()-1))
            {
                state.MyCurrEq += 1;
                Equation = EqList[state.MyCurrEq];
            }
            else
                flagRoomNotifications = 4;
            state.MyMin = TimerMin;
            state.MySec = TimerSec;
            _context.SaveChanges();
            return Page();
        }

        public ActionResult OnPostSkipAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var userId = Int32.Parse(Request.Form["PId"]);
            var GameId = Int32.Parse(Request.Form["RId"]);
            GName = Request.Form["GameName"];
            TimerMin = Request.Form["Min"];
            TimerSec = Request.Form["Sec"];
            RoomId2 = GameId;
            CurrentState state = _context.MyState.Where(x => x.MyGameID == GameId && x.MyPlayerID == userId).SingleOrDefault();
            createListAsync(RoomId2);
            Player p = _context.Players.Where(x => x.PlayerID == state.MyPlayerID && x.GameId == state.MyGameID).SingleOrDefault();
            Player = p;
            if (state.MyCurrEq < (EqList.Count() - 1))
            {
                state.MyCurrEq += 1;
                Equation = EqList[state.MyCurrEq];
                flagRoomNotifications = 5;
            }
            else
                flagRoomNotifications = 4;
            state.MyMin = TimerMin;
            state.MySec = TimerSec;
            _context.SaveChanges();
            return Page();
        }
    }
}