using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraTyping
{
    public class WeaponOutProjectiles
    {
        public static Dictionary<int, Element> Type = new Dictionary<int, Element>() { };
        public static Dictionary<string, Element> _Type = new Dictionary<string, Element>()
        {
            {"LeatherWhip", Element.normal},
            {"Whiplash", Element.blood},
            {"NotchedWhip", Element.dark},
            {"BoneWhip", Element.bone},
            {"CoiledThorns", Element.grass},
            {"MoltenChains", Element.fire},
            {"EelWhip", Element.grass},
            {"PuzzlingCutter", Element.fire},
            {"CrystalVileLash", Element.water},

            {"APARocketI", Element.normal },
            {"APARocketII", Element.normal },
            {"APARocketIII", Element.normal },
            {"APARocketIV", Element.normal },
            {"DemonBlast", Element.dark },
            {"HoneyBee", Element.bug },
            {"HoneyBeeBig", Element.bug },
            {"ManaBlast", Element.psychic },
            {"ManaRestoreBeam", Element.fighting },
            {"MeteorBreakshatter", Element.fire },
            {"MeteorBreakshot", Element.fire },
            {"PsyWave", Element.ghost },
            {"Reverb", Element.psychic },
            {"ScatterShot", Element.fairy },
            {"Skelebro", Element.bone },

            {"SpiritBeam", Element.fairy },
            {"SpiritBlast", Element.ghost },
            {"SpiritComet", Element.rock },
            {"SpiritDragon", Element.dragon },
            {"SpiritExplosion", Element.normal },
            {"SpiritGuardian", Element.dragon },
            {"SpiritIcicle", Element.ice },
            {"SpiritLeaf", Element.grass },
            {"SpiritMartianFist", Element.psychic },
            {"SpiritPumpkinsplosion", Element.normal },
            {"SpiritQuake", Element.normal },
            {"SpiritShadow", Element.dark },
            {"SpiritThornBall", Element.grass },
            {"SpiritWindstream", Element.flying },
            {"SplinterShot", Element.grass },
        };
    }
}
