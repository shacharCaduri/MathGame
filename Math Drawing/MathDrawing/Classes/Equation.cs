using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathDrawing.Classes
{
    public class Equation
    {
        public int EquationID { get; set; }
        public String EquationQ { get; set; }
        public int Result { get; set; }
        public Level Level { get; set; }
    }
}
