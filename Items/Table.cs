using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.Items
{
    public class Table
    {
        public static float[,] Effectiveness = new float[21, 22]
        {
            //         Nor  Fir   Wat  Ele   Gra  Ice   Fig  Poi   Gro  Fly   Psy  Bug   Roc  Gho   Dra  Dar   Ste  Fai   Blo  Bon   Lev  Non
            /* Nor */ { 1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,Config.Divisor,  0f,   1f,  1f,Config.Divisor,  1f,   1f,  1f,   1f,  1f},

            /* Fir */ { 1f,Config.Divisor,Config.Divisor,  1f,Config.Multiplier,Config.Multiplier,   1f,  1f,   1f,  1f,   1f,Config.Multiplier,Config.Divisor,  1f,Config.Divisor,  1f, Config.Multiplier,  1f, Config.Multiplier,  1f,   1f,  1f},

            /* Wat */ { 1f,  1f,Config.Divisor,  1f,Config.Divisor,  1f,   1f,  1f, Config.Multiplier,  1f,   1f,  1f, Config.Multiplier,  1f,Config.Divisor,  1f,   1f,  1f, Config.Multiplier,  1f,   1f,  1f},

            /* Ele */ { 1f,  1f, Config.Multiplier,Config.Divisor,Config.Divisor,  1f,   1f,  1f,   0f,Config.Multiplier,   1f,  1f,   1f,  1f,Config.Divisor,  1f,   1f,  1f,   1f,  1f,   1f,  1f},

            /* Gra */ { 1f,Config.Divisor, Config.Multiplier,  1f,Config.Divisor,  1f,   1f,Config.Divisor, Config.Multiplier,Config.Divisor,   1f,Config.Divisor, Config.Multiplier,  1f,Config.Divisor,  1f,Config.Divisor,  1f,   1f,  1f,   1f,  1f},

            /* Ice */ { 1f,Config.Divisor,Config.Divisor,  1f, Config.Multiplier,Config.Divisor,   1f,  1f, Config.Multiplier,Config.Multiplier,   1f,  1f,   1f,  1f, Config.Multiplier,  1f,Config.Divisor,  1f,   1f,  1f,   1f,  1f},

            /* Fig */ { 2f,  1f,   1f,  1f,   1f,Config.Multiplier,   1f,Config.Divisor,   1f,Config.Divisor,Config.Divisor,Config.Divisor, Config.Multiplier,  0f,   1f,Config.Multiplier, Config.Multiplier,Config.Divisor,   1f,Config.Divisor,   1f,  1f},

            /* Poi */ { 1f,  1f,   1f,  1f, Config.Multiplier,  1f,   1f,Config.Divisor,Config.Divisor,  1f,   1f,  1f,Config.Divisor,Config.Divisor,   1f,  1f,   0f,Config.Multiplier, Config.Multiplier,  0f,   1f,  1f},

            /* Gro */ { 1f,Config.Multiplier,   1f,Config.Multiplier,Config.Divisor,  1f,   1f,Config.Multiplier,   1f,  0f,   1f,Config.Divisor, Config.Multiplier,  1f,   1f,  1f, Config.Multiplier,  1f,   1f,  1f,   0f,  1f},

            /* Fly */ { 1f,  1f,   1f,Config.Divisor, Config.Multiplier,  1f, Config.Multiplier,  1f,   1f,  1f,   1f,Config.Multiplier,Config.Divisor,  1f,   1f,  1f,Config.Divisor,  1f,   1f,  1f,   1f,  1f},

            /* Psy */ { 1f,  1f,   1f,  1f,   1f,  1f, Config.Multiplier,Config.Multiplier,   1f,  1f,Config.Divisor,  1f,   1f,  1f,   1f,  0f,Config.Divisor,  1f,Config.Divisor,Config.Divisor,   1f,  1f},

            /* Bug */ { 1f,Config.Divisor,   1f,  1f, Config.Multiplier,  1f,Config.Divisor,Config.Divisor,   1f,Config.Divisor, Config.Multiplier,  1f,   1f,Config.Divisor,   1f,Config.Multiplier,Config.Divisor,Config.Divisor,   1f,  1f,   1f,  1f},

            /* Roc */ { 1f,Config.Multiplier,   1f,  1f,   1f,Config.Multiplier,Config.Divisor,  1f,Config.Divisor,Config.Multiplier,   1f,Config.Multiplier,   1f,  1f,   1f,  1f,Config.Divisor,  1f,   1f,Config.Multiplier,   1f,  1f},

            /* Gho */ { 0f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f, Config.Multiplier,  1f,   1f,Config.Multiplier,   1f,Config.Divisor,   1f,  1f,Config.Divisor,  1f,   1f,  1f},

            /* Dra */ { 1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f, Config.Multiplier,  1f,Config.Divisor,  0f,   1f,Config.Multiplier,   1f,  1f},

            /* Dar */ { 1f,  1f,   1f,  1f,   1f,  1f,Config.Divisor,  1f,   1f,  1f, Config.Multiplier,  1f,   1f,Config.Multiplier,   1f,Config.Divisor,   1f,Config.Divisor,Config.Divisor,  1f,   1f,  1f},

            /* Ste */ { 1f,Config.Divisor,Config.Divisor,Config.Divisor,   1f,Config.Multiplier,   1f,  1f,   1f,  1f,   1f,  1f, Config.Multiplier,  1f,   1f,  1f,Config.Divisor,Config.Multiplier,   1f,Config.Multiplier,   1f,  1f},

            /* Fai */ { 1f,Config.Divisor,   1f,  1f,   1f,  1f, Config.Multiplier,Config.Divisor,   1f,  1f,   1f,  1f,   1f,  1f, Config.Multiplier,Config.Multiplier,Config.Divisor,  1f, Config.Multiplier,  1f,   1f,  1f},

            /* Blo */ { 1f,  1f,Config.Divisor,  1f,   1f,  1f,Config.Divisor,Config.Multiplier,   1f,  1f, Config.Multiplier,  1f,   1f,Config.Divisor,   1f,  1f,Config.Divisor,Config.Divisor,Config.Divisor,  1f,   1f,  1f},

            /* Bon */ { 1f,Config.Divisor,   1f,  1f,   1f,Config.Divisor,   1f,  1f,   1f,  1f, Config.Multiplier,  1f,Config.Divisor,Config.Divisor,   1f,  1f,Config.Divisor,  1f,   1f,Config.Divisor,   1f,  1f},

            /* Non */ { 1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f,   1f,  1f},

        };
    }
}
