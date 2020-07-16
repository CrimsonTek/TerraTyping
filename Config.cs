using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace TerraTyping
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Multiplier")]
        [Tooltip("The increased damage of super effective moves.")]
        [Range(1f, 5f)]
        [Increment(0.1f)]
        [DefaultValue(2)]
        public float Multiplier = 2;

        [Label("Divisor")]
        [Tooltip("The decreased damage of uneffective moves.")]
        [Range(0f, 1f)]
        [Increment(0.1f)]
        [DefaultValue(0.5)]
        public float Divisor = 0.5f;

        [Label("STAB")]
        [Tooltip("The increased damage when wearing the same armor type as your weapon type.")]
        [Range(1f, 5f)]
        [Increment(0.1f)]
        [DefaultValue(1.5)]
        public float STAB = 1.5f;

        [Label("Weather Multiplier")]
        [Tooltip("The increased damage when using a weapon that's super effective in a specific type of weather.\n" +
            "This affects water type weapons in the rain, ground type weapons in sandstorms, and more.")]
        [Range(1f, 5f)]
        [Increment(0.05f)]
        [DefaultValue(1.25)]
        public float RainMultiplier = 1.25f;

        [Label("Weather Multiplier in Expert Only")]
        [Tooltip("Whether or not the weather multiplier should only apply in expert.")]
        [DefaultValue(false)]
        public bool RainMultOnlyExpert = false;
    }
}
