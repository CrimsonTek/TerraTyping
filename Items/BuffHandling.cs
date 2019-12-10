//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace TerraTyping
//{
//    //public class Buffy : GlobalBuff
//    //{
//    //    public override void Update(int type, Player player, ref int buffIndex)
//    //    {
//    //        if (type == BuffID.OnFire)
//    //            player.lifeRegen = 
//    //    }
//    //}

//    public class Buffy : GlobalBuff
//    {
//        public override void Update(int type, Player player, ref int buffIndex)
//        {
//        }
//    }

//    public class PlayerBuffHandling : ModPlayer
//    {
//        public override void UpdateBadLifeRegen()
//        {
//            //Tuple<Element, Element, Element> typeSet = ArmorPlayer.typeSet;
//            //foreach (var buff in Buffs.Type)
//            //{
//            //    if (player.HasBuff(buff.Key) && !player.buffImmune[buff.Key])
//            //        player.lifeRegen -= Calc.LifeRegen(buff.Key, ArmorPlayer.typeSet, );
//            //}

//            //if (player.HasBuff(BuffID.Poisoned) && !player.buffImmune[BuffID.Poisoned])
//            //{
//            //    player.lifeRegen -= Calc.BadRegen(BuffID.Poisoned, typeSet, 4);
//            //}
//            //if (player.HasBuff(BuffID.Venom) && !player.buffImmune[BuffID.Venom])
//            //{
//            //    player.lifeRegen -= Calc.BadRegen(BuffID.Venom, typeSet, 12);
//            //}
//            //if (player.HasBuff(BuffID.OnFire) && !player.buffImmune[BuffID.OnFire])
//            //{
//            //    player.lifeRegen -= Calc.BadRegen(BuffID.OnFire, typeSet, 8);
//            //}
//            //if (player.HasBuff(BuffID.Burning) && !player.buffImmune[BuffID.Burning])
//            //if (player.HasBuff(BuffID.Venom))
//            //{
//            //    float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.Venom], (int)ArmorPlayer.typeSet.Item1];
//            //    float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.Venom], (int)ArmorPlayer.typeSet.Item2];
//            //    player.lifeRegen = (int)(player.lifeRegen * (multiplier1 * multiplier2));
//            //}
//            //if (player.HasBuff(BuffID.CursedInferno))
//            //{
//            //    float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.CursedInferno], (int)ArmorPlayer.typeSet.Item1];
//            //    float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.CursedInferno], (int)ArmorPlayer.typeSet.Item2];
//            //    player.lifeRegen = (int)(player.lifeRegen * (multiplier1 * multiplier2));
//            //}
//            //if (player.HasBuff(BuffID.Burning))
//            //{
//            //    float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.Burning], (int)ArmorPlayer.typeSet.Item1];
//            //    float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.Burning], (int)ArmorPlayer.typeSet.Item2];
//            //    player.lifeRegen = (int)(player.lifeRegen * (multiplier1 * multiplier2));
//            //}
//            //if (player.HasBuff(BuffID.Frostburn))
//            //{
//            //    float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.Frostburn], (int)ArmorPlayer.typeSet.Item1];
//            //    float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.Frostburn], (int)ArmorPlayer.typeSet.Item2];
//            //    player.lifeRegen = (int)(player.lifeRegen * (multiplier1 * multiplier2));
//            //}
//            //if (player.HasBuff(BuffID.ShadowFlame))
//            //{
//            //    float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.ShadowFlame], (int)ArmorPlayer.typeSet.Item1];
//            //    float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.ShadowFlame], (int)ArmorPlayer.typeSet.Item2];
//            //    player.lifeRegen = (int)(player.lifeRegen * (multiplier1 * multiplier2));
//            //}
//        }
//    }
//    public class NPCBuffHandling : GlobalNPC
//    {
//        public override void UpdateLifeRegen(NPC npc, ref int damage)
//        {
//            if (npc.HasBuff(BuffID.OnFire))
//            {
//                float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.OnFire], (int)Enemies.Type[npc.type].Item1];
//                float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.OnFire], (int)Enemies.Type[npc.type].Item2];
//                npc.lifeRegen = (int)(npc.lifeRegen * (multiplier1 * multiplier2));
//            }
//            if (npc.HasBuff(BuffID.Venom))
//            {
//                float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.Venom], (int)Enemies.Type[npc.type].Item1];
//                float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.Venom], (int)Enemies.Type[npc.type].Item2];
//                npc.lifeRegen = (int)(npc.lifeRegen * (multiplier1 * multiplier2));
//            }
//            if (npc.HasBuff(BuffID.CursedInferno))
//            {
//                float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.CursedInferno], (int)Enemies.Type[npc.type].Item1];
//                float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.CursedInferno], (int)Enemies.Type[npc.type].Item2];
//                npc.lifeRegen = (int)(npc.lifeRegen * (multiplier1 * multiplier2));
//            }
//            if (npc.HasBuff(BuffID.Burning))
//            {
//                float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.Burning], (int)Enemies.Type[npc.type].Item1];
//                float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.Burning], (int)Enemies.Type[npc.type].Item2];
//                npc.lifeRegen = (int)(npc.lifeRegen * (multiplier1 * multiplier2));
//            }
//            if (npc.HasBuff(BuffID.Frostburn))
//            {
//                float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.Frostburn], (int)Enemies.Type[npc.type].Item1];
//                float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.Frostburn], (int)Enemies.Type[npc.type].Item2];
//                npc.lifeRegen = (int)(npc.lifeRegen * (multiplier1 * multiplier2));
//            }
//            if (npc.HasBuff(BuffID.ShadowFlame))
//            {
//                float multiplier1 = Table.Effectiveness[(int)Buffs.Type[BuffID.ShadowFlame], (int)Enemies.Type[npc.type].Item1];
//                float multiplier2 = Table.Effectiveness[(int)Buffs.Type[BuffID.ShadowFlame], (int)Enemies.Type[npc.type].Item2];
//                npc.lifeRegen = (int)(npc.lifeRegen * (multiplier1 * multiplier2));
//            }
//        }
//    }
//}
