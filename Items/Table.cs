using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping
{
    public class Table
    {
        static readonly float Mult = Config.Multiplier;
        static readonly float Divi = Config.Divisor;

        public static float[,] Effectiveness = new float[22, 22]
        {
            //         Nor   Fir   Wat   Ele   Gra   Ice   Fig   Poi   Gro   Fly   Psy   Bug   Roc   Gho   Dra   Dar   Ste   Fai   Blo   Bon   Lev   Non
            /* Nor */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Divi,   0f,   1f,   1f, Divi,   1f,   1f,   1f,   1f,  1f},

            /* Fir */ { 1f, Divi, Divi,   1f, Mult, Mult,   1f,   1f,   1f,   1f,   1f, Mult, Divi,   1f, Divi,   1f, Mult,   1f, Mult,   1f,   1f,  1f},

            /* Wat */ { 1f, Mult, Divi,   1f, Divi,   1f,   1f,   1f, Mult,   1f,   1f,   1f, Mult,   1f, Divi,   1f,   1f,   1f, Mult,   1f,   1f,  1f},

            /* Ele */ { 1f,   1f, Mult, Divi, Divi,   1f,   1f,   1f,   0f, Mult,   1f,   1f,   1f,   1f, Divi,   1f,   1f,   1f,   1f,   1f,   1f,  1f},

            /* Gra */ { 1f, Divi, Mult,   1f, Divi,   1f,   1f, Divi, Mult, Divi,   1f, Divi, Mult,   1f, Divi,   1f, Divi,   1f,   1f, Divi,   1f,  1f},

            /* Ice */ { 1f, Divi, Divi,   1f, Mult, Divi,   1f,   1f, Mult, Mult,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   1f,   1f,   1f,   1f,  1f},

            /* Fig */ { 2f,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   1f, Divi, Divi, Divi, Mult,   0f,   1f, Mult, Mult, Divi, Mult, Mult,   1f,  1f},

            /* Poi */ { 1f,   1f,   1f,   1f, Mult,   1f,   1f, Divi, Divi,   1f,   1f,   1f, Divi, Divi,   1f,   1f,   0f, Mult, Mult,   0f,   1f,  1f},

            /* Gro */ { 1f, Mult,   1f, Mult, Divi,   1f,   1f, Mult,   1f,   0f,   1f, Divi, Mult,   1f,   1f,   1f, Mult,   1f,   1f,   1f,   0f,  1f},

            /* Fly */ { 1f,   1f,   1f, Divi, Mult,   1f, Mult,   1f,   1f,   1f,   1f, Mult, Divi,   1f,   1f,   1f, Divi,   1f,   1f,   1f,   1f,  1f},

            /* Psy */ { 1f,   1f,   1f,   1f,   1f,   1f, Mult, Mult,   1f,   1f, Divi,   1f,   1f,   1f,   1f,   0f, Divi,   1f, Divi, Divi,   1f,  1f},

            /* Bug */ { 1f, Divi,   1f,   1f, Mult,   1f, Divi, Divi,   1f, Divi, Mult,   1f,   1f, Divi,   1f, Mult, Divi, Divi,   1f,   1f,   1f,  1f},

            /* Roc */ { 1f, Mult,   1f,   1f,   1f, Mult, Divi,   1f, Divi, Mult,   1f, Mult,   1f,   1f,   1f,   1f, Divi,   1f,   1f, Mult,   1f,  1f},

            /* Gho */ { 0f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f,   1f, Mult,   1f, Divi,   1f,   1f, Divi, Divi,   1f,  1f},
             
            /* Dra */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   0f, Divi, Mult,   1f,  1f},

            /* Dar */ { 1f,   1f,   1f,   1f,   1f,   1f, Divi,   1f,   1f,   1f, Mult,   1f,   1f, Mult,   1f, Divi,   1f, Divi, Divi,   1f,   1f,  1f},

            /* Ste */ { 1f, Divi, Divi, Divi,   1f, Mult,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f,   1f,   1f, Divi, Mult,   1f, Mult,   1f,  1f},

            /* Fai */ { 1f, Divi,   1f,   1f,   1f,   1f, Mult, Divi,   1f,   1f,   1f,   1f,   1f,   1f, Mult, Mult, Divi,   1f, Mult,   1f,   1f,  1f},

            /* Blo */ { 1f,   1f, Divi,   1f,   1f, Mult, Mult, Mult,   1f,   1f, Mult,   1f,   1f, Divi,   1f,   1f, Divi, Divi, Divi, Mult,   1f,  1f},

            /* Bon */ { 1f, Divi,   1f,   1f,   1f, Divi, Mult,   1f, Mult,   1f, Mult,   1f, Divi, Divi,   1f, Mult, Divi,   1f, Mult, Divi,   1f,  1f},

            
            /* Lev */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,  1f},

            /* Non */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,  1f},

        };
    }
}
