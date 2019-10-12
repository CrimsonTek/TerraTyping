using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items
{
    public class Buffs
    {
        public static Dictionary<int, Element.Type> Type = new Dictionary<int, Element.Type>
        {
            {BuffID.Poisoned, Element.Type.poison },
            {BuffID.Darkness, Element.Type.dark },
            {BuffID.Cursed, Element.Type.ghost },
            {BuffID.OnFire, Element.Type.fire },
            {BuffID.Bleeding, Element.Type.blood },
            {BuffID.Confused, Element.Type.psychic },
            {BuffID.Slow, Element.Type.normal },
            {BuffID.Weak, Element.Type.normal },
            {BuffID.Silenced, Element.Type.normal },
            {BuffID.BrokenArmor, Element.Type.normal },
            {BuffID.Horrified, Element.Type.psychic },
            {BuffID.TheTongue, Element.Type.blood },
            {BuffID.CursedInferno, Element.Type.fire },
            {BuffID.Chilled, Element.Type.ice },
            {BuffID.Frozen, Element.Type.ice },
            {BuffID.Burning, Element.Type.fire },
            {BuffID.Suffocation, Element.Type.normal },
            {BuffID.Ichor, Element.Type.blood },
            {BuffID.Blackout, Element.Type.dark },
            {BuffID.WaterCandle, Element.Type.water },
            {BuffID.ChaosState, Element.Type.fairy },
            {BuffID.Wet, Element.Type.water },
            {BuffID.Stinky, Element.Type.normal },
            {BuffID.Slimed, Element.Type.water },
            {BuffID.Electrified, Element.Type.electric },
            {BuffID.MoonLeech, Element.Type.dark },
            {148, Element.Type.normal }, // feral bite
            {BuffID.Webbed, Element.Type.normal },
            {BuffID.ShadowFlame, Element.Type.fire },
            {BuffID.Stoned, Element.Type.rock },
            {BuffID.Dazed, Element.Type.psychic },
            {BuffID.Obstructed, Element.Type.psychic },
            {164, Element.Type.flying }, // distorted
            {169, Element.Type.bone }, // penetrated
            {BuffID.WindPushed, Element.Type.flying },
            {BuffID.WitheredArmor, Element.Type.dark },
            {BuffID.WitheredWeapon, Element.Type.dark }
        };
    }
}
