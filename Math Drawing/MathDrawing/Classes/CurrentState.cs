using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MathDrawing.Classes
{
    public class CurrentState
    {
        [Key]
        public int MyStateID { get; set; }
        public int MyPlayerID { get; set; }
        public IdentityUser UserMail { get; set; }
        public String GameName { get; set; }
        public int MyGameID { get; set; }
        public int MyCurrEq { get; set; }
        public int MyCurrScore { get; set; }
        public string MyMin { get; set; }
        public string MySec { get; set; }
    }
}
