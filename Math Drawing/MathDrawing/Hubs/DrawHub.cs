using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathDrawing.Classes;
using MathDrawing.Pages.Games;
using MathDrawing.Data.Migrations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace MathDrawing.Hubs
{
    public class DrawHub : Hub
    {
        private readonly MathDrawing.Data.ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        private string _creator { get; set; }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public DrawHub(MathDrawing.Data.ApplicationDbContext context, UserManager<IdentityUser> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }
        public async Task StartGame(string roomid)
        {
            await Clients.Group(roomid).SendAsync("ReceiveMessage", Context.User.Identity.Name, "Started the game");
        }
        public async Task Ujoin(string roomid)
        {
            await Clients.Group(roomid).SendAsync("ReceiveMessage", Context.User.Identity.Name, "joined the game");

        }
        public async Task Umay(string roomid)
        {
            await Clients.Group(roomid).SendAsync("ReceiveMessage", "Manager", "You may press START to start the time");
        }
        public async Task Subscribe(string roomid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomid);
        }
        public async Task SendResult(string answer, string roomid)
        {
            if (answer == "skip")
                await Clients.Group(roomid).SendAsync("ReceiveMessage", Context.User.Identity.Name, "skipped a question. No points for them! :(");
             else if (answer == "true")
            {
                await Clients.Group(roomid).SendAsync("ReceiveMessage", Context.User.Identity.Name, "answered correctly. They get 10 points! :)");
            }
            else
                await Clients.Group(roomid).SendAsync("ReceiveMessage", Context.User.Identity.Name, "answered incorrectly. No points for them! :(");
        }
        public async Task EmptyEq(string roomid)
        {
            await Clients.Group(roomid).SendAsync("ReceiveMessage", Context.User.Identity.Name, "Good Job! You finished all of the Questions!");

        }

        public async Task endGame(string roomid, int score)
        {
            string txt = "Total score is " + score;
            await Clients.Group(roomid).SendAsync("ReceiveMessage", Context.User.Identity.Name, "ended the game");
            await Clients.Group(roomid).SendAsync("ReceiveMessage", Context.User.Identity.Name, txt);
        }
    }

}
