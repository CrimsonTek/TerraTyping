using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.Items
{
    public class Config
    {
        public static float Multiplier { get; } = 2.0f;
        // the multiplier that effective types use (default = 2.0f)
        // don't go less than 1.0f

        public static float Divisor { get; } = 0.5f;
        // the divisor that not effective types use (default = 0.5f)
        // don't go more than 1.0f

        public static float STAB { get; } = 1.5f;
        // wearing armor will multiply weapons of the same type by the STAB value (default 1.5f)
        // don't go less than 1.0f

        public static float Knockback { get; } = 1f;

        public static float RainMultiplier { get; } = 1.25f;
        // the multiplier of water type moves during rain, blood type moves during blood moons, etc
        // don't go less than 1.0f;
    }
}
