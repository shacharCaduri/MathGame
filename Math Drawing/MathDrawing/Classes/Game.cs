using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MathDrawing.Classes
{
    public class Game
    {
        public int GameID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public String Name { get; set; }
        public Level Level { get; set; }
        public IdentityUser Creator { get; set; }
    }
}
