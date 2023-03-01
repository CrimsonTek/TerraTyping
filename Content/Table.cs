using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;

namespace TerraTyping
{
    public class Table : ILoadable
    {
        public static float Mult { get; private set; }
        public static float Divi { get; private set; }
        public static float[,] EffectivenessTable { get; private set; }

        void ILoadable.Load(Mod mod)
        {
            ServerConfig config = ModContent.GetInstance<ServerConfig>();
            Mult = config.Multiplier;
            Divi = config.Divisor;
            config.OnChangedEvent += () =>
            {
                Mult = config.Multiplier;
                Divi = config.Divisor;
            };
            EffectivenessTable = BuildTable();
        }

        void ILoadable.Unload()
        {
            EffectivenessTable = null;
        }

        public static float Effectiveness(Element attack, Element defense)
        {
            if ((int)attack < 20 && (int)defense < 20)
            {
                return EffectivenessTable[(int)attack, (int)defense];
            }
            else
            {
                return 1;
            }
        }

        public static float Effectiveness(int attack, int defense)
        {
            if (attack < 20 && defense < 20)
            {
                return EffectivenessTable[attack, defense];
            }
            else
            {
                return 1;
            }
        }

        public static float[,] BuildTable()
        {
            return new float[21, 21]
            {
            //         Nor   Fir   Wat   Ele   Gra   Ice   Fig   Poi   Gro   Fly   Psy   Bug   Roc   Gho   Dra   Dar   Ste   Fai   Blo   Bon   Non
            /* Nor */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Divi,   0f,   1f,   1f, Divi,   1f,   1f,   1f,  1f},
            /* Fir */ { 1f, Divi, Divi,   1f, Mult, Mult,   1f,   1f,   1f,   1f,   1f, Mult, Divi,   1f, Divi,   1f, Mult,   1f, Mult,   1f,  1f},
            /* Wat */ { 1f, Mult, Divi,   1f, Divi,   1f,   1f,   1f, Mult,   1f,   1f,   1f, Mult,   1f, Divi,   1f,   1f,   1f, Mult,   1f,  1f},
            /* Ele */ { 1f,   1f, Mult, Divi, Divi,   1f,   1f,   1f,   0f, Mult,   1f,   1f,   1f,   1f, Divi,   1f,   1f,   1f,   1f,   1f,  1f},
            /* Gra */ { 1f, Divi, Mult,   1f, Divi,   1f,   1f, Divi, Mult, Divi,   1f, Divi, Mult,   1f, Divi,   1f, Divi,   1f,   1f, Divi,  1f},
            /* Ice */ { 1f, Divi, Divi,   1f, Mult, Divi,   1f,   1f, Mult, Mult,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   1f,   1f,   1f,  1f},
            /* Fig */ { 2f,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   1f, Divi, Divi, Divi, Mult,   0f,   1f, Mult, Mult, Divi, Mult, Mult,  1f},
            /* Poi */ { 1f,   1f,   1f,   1f, Mult,   1f,   1f, Divi, Divi,   1f,   1f,   1f, Divi, Divi,   1f,   1f,   0f, Mult, Mult,   0f,  1f},
            /* Gro */ { 1f, Mult,   1f, Mult, Divi,   1f,   1f, Mult,   1f,   0f,   1f, Divi, Mult,   1f,   1f,   1f, Mult,   1f,   1f,   1f,  1f},
            /* Fly */ { 1f,   1f,   1f, Divi, Mult,   1f, Mult,   1f,   1f,   1f,   1f, Mult, Divi,   1f,   1f,   1f, Divi,   1f,   1f,   1f,  1f},
            /* Psy */ { 1f,   1f,   1f,   1f,   1f,   1f, Mult, Mult,   1f,   1f, Divi,   1f,   1f,   1f,   1f,   0f, Divi,   1f, Divi, Divi,  1f},
            /* Bug */ { 1f, Divi,   1f,   1f, Mult,   1f, Divi, Divi,   1f, Divi, Mult,   1f,   1f, Divi,   1f, Mult, Divi, Divi,   1f,   1f,  1f},
            /* Roc */ { 1f, Mult,   1f,   1f,   1f, Mult, Divi,   1f, Divi, Mult,   1f, Mult,   1f,   1f,   1f,   1f, Divi,   1f,   1f, Mult,  1f},
            /* Gho */ { 0f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f,   1f, Mult,   1f, Divi,   1f,   1f, Divi, Divi,  1f},             
            /* Dra */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   0f, Divi, Mult,  1f},
            /* Dar */ { 1f,   1f,   1f,   1f,   1f,   1f, Divi,   1f,   1f,   1f, Mult,   1f,   1f, Mult,   1f, Divi,   1f, Divi, Divi,   1f,  1f},
            /* Ste */ { 1f, Divi, Divi, Divi,   1f, Mult,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f,   1f,   1f, Divi, Mult,   1f, Mult,  1f},
            /* Fai */ { 1f, Divi,   1f,   1f,   1f,   1f, Mult, Divi,   1f,   1f,   1f,   1f,   1f,   1f, Mult, Mult, Divi,   1f, Mult,   1f,  1f},
            /* Blo */ { 1f,   1f, Divi,   1f,   1f, Mult, Mult, Mult,   1f,   1f, Mult,   1f,   1f, Divi,   1f,   1f, Divi, Divi, Divi, Mult,  1f},
            /* Bon */ { 1f, Divi,   1f,   1f,   1f, Divi, Mult,   1f, Mult,   1f, Mult,   1f, Divi, Divi,   1f, Mult, Divi,   1f, Mult, Divi,  1f},
            /* Non */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,  1f},
            };
        }
    }
}
