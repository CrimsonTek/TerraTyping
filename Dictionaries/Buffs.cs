//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using TerraTyping.Attributes;

//namespace TerraTyping
//{
//    [Load]
//    [Unload]
//    public static class Buffs
//    {
//        public static void Load()
//        {
//            Type = new Dictionary<int, Element>
//            {
//                //BuffID, Element, DrainsHealth?, HealthDrain
//                {BuffID.Poisoned, Element.poison },
//                {BuffID.Darkness, Element.dark },
//                {BuffID.Cursed, Element.ghost },
//                {BuffID.OnFire, Element.fire },
//                {BuffID.Bleeding, Element.blood },
//                {BuffID.Confused, Element.psychic },
//                {BuffID.Slow, Element.normal },
//                {BuffID.Weak, Element.normal },
//                {BuffID.Silenced, Element.normal },
//                {BuffID.BrokenArmor, Element.normal },
//                {BuffID.Horrified, Element.psychic },
//                {BuffID.TheTongue, Element.blood },
//                {BuffID.CursedInferno, Element.fire },
//                {BuffID.Chilled, Element.ice },
//                {BuffID.Frozen, Element.ice },
//                {BuffID.Burning, Element.fire },
//                {BuffID.Suffocation, Element.normal },
//                {BuffID.Ichor, Element.blood },
//                {BuffID.Blackout, Element.dark },
//                {BuffID.WaterCandle, Element.water },
//                {BuffID.ChaosState, Element.fairy },
//                {BuffID.Wet, Element.water },
//                {BuffID.Stinky, Element.normal },
//                {BuffID.Slimed, Element.water },
//                {BuffID.Electrified, Element.electric },
//                {BuffID.MoonLeech, Element.dark },
//                {148, Element.normal }, // feral bite
//                {BuffID.Webbed, Element.normal },
//                {BuffID.ShadowFlame, Element.fire },
//                {BuffID.Stoned, Element.rock },
//                {BuffID.Dazed, Element.psychic },
//                {BuffID.Obstructed, Element.psychic },
//                {164, Element.flying }, // distorted
//                {169, Element.bone }, // penetrated
//                {BuffID.WindPushed, Element.flying },
//                {BuffID.WitheredArmor, Element.dark },
//                {BuffID.WitheredWeapon, Element.dark },
//                {BuffID.Venom, Element.poison },
//                {BuffID.Frostburn, Element.ice },
//            };

//            Regen = new Dictionary<int, int>
//            {
//                {BuffID.Poisoned, -4 },
//                {BuffID.Venom, -12 },
//                {BuffID.OnFire, -8 },
//                {BuffID.Frostburn, -12 },
//                //{BuffID. }
//                {BuffID.Burning, -60 },
//                {BuffID.Suffocation, -40 },
//                //electrified, -8 or if (this.controlLeft || this.controlRight) -32
//                {BuffID.TheTongue, -100 },
//            };
//        }

//        public static void Unload()
//        {
//            Type = null;
//            Regen = null;
//        }

//        public static Dictionary<int, Element> Type;
//        public static Dictionary<int, int> Regen;
//    }
//}
