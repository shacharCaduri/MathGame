using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathDrawing.Classes
{
    public class Player
    {
        public int PlayerID { get; set; }
        public IdentityUser PlayerUser { get; set; }
        public int Score { get; set; }
        public int GameId { get; set; }
    }
}
