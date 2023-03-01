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
using TerraTyping.Attributes;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    // Todo: Merge with Weapons?
    [Load]
    [Unload]
    public static class Ammos
    {
        public static void Load()
        {
            Type = new Dictionary<int, ItemTypeInfo>
            {
                // ammo
                {ItemID.WoodenArrow, new ItemTypeInfo(Element.normal) },
                {ItemID.FlamingArrow, new ItemTypeInfo(Element.fire) },
                {ItemID.UnholyArrow, new ItemTypeInfo(Element.dark) },
                {ItemID.JestersArrow, new ItemTypeInfo(Element.flying) },
                {ItemID.MusketBall, new ItemTypeInfo(Element.normal) },
                {ItemID.SilverBullet, new ItemTypeInfo(Element.normal) },
                {ItemID.FallenStar, new ItemTypeInfo(Element.flying) },
                {ItemID.MeteorShot, new ItemTypeInfo(Element.rock) },
                {ItemID.HellfireArrow, new ItemTypeInfo(Element.fire) },
                {ItemID.SandBlock, new ItemTypeInfo(Element.ground) },
                {ItemID.EbonsandBlock, new ItemTypeInfo(Element.dark) },
                {ItemID.CrimsandBlock, new ItemTypeInfo(Element.blood) },
                {ItemID.PearlsandBlock, new ItemTypeInfo(Element.fairy) },
                {ItemID.Seed, new ItemTypeInfo(Element.grass) },
                {ItemID.CrystalBullet, new ItemTypeInfo(Element.fairy) },
                {ItemID.HolyArrow, new ItemTypeInfo(Element.fairy) },
                {ItemID.CursedArrow, new ItemTypeInfo(Element.ghost) },
                {ItemID.CursedBullet, new ItemTypeInfo(Element.ghost) },
                {ItemID.Snowball, new ItemTypeInfo(Element.ice) },
                {ItemID.BoneArrow, new ItemTypeInfo(Element.bone) },
                {ItemID.FrostburnArrow, new ItemTypeInfo(Element.ice) },
                {ItemID.RocketI, new ItemTypeInfo(Element.normal) },
                {ItemID.RocketII, new ItemTypeInfo(Element.normal) },
                {ItemID.RocketIII, new ItemTypeInfo(Element.normal) },
                {ItemID.RocketIV, new ItemTypeInfo(Element.normal) },
                {ItemID.Flare, new ItemTypeInfo(Element.fire) },
                {ItemID.BlueFlare, new ItemTypeInfo(Element.fire) },
                {ItemID.CopperCoin, new ItemTypeInfo(Element.normal) },
                {ItemID.SilverCoin, new ItemTypeInfo(Element.normal) },
                {ItemID.GoldCoin, new ItemTypeInfo(Element.normal) },
                {ItemID.PlatinumCoin, new ItemTypeInfo(Element.normal) },
                {ItemID.Gel, new ItemTypeInfo(Element.fire) },
                {ItemID.StyngerBolt, new ItemTypeInfo(Element.fighting) },
                {ItemID.PoisonDart, new ItemTypeInfo(Element.poison) },
                {ItemID.IchorArrow, new ItemTypeInfo(Element.blood) },
                {ItemID.IchorBullet, new ItemTypeInfo(Element.blood) },
                {ItemID.ExplosiveBunny, new ItemTypeInfo(Element.normal) },
                {ItemID.VenomArrow, new ItemTypeInfo(Element.poison) },
                {ItemID.VenomBullet, new ItemTypeInfo(Element.poison) },
                {ItemID.PartyBullet, new ItemTypeInfo(Element.fairy) },
                {ItemID.NanoBullet, new ItemTypeInfo(Element.psychic) },
                {ItemID.ExplodingBullet, new ItemTypeInfo(Element.normal) },
                {ItemID.GoldenBullet, new ItemTypeInfo(Element.steel) },
                {ItemID.CandyCorn, new ItemTypeInfo(Element.normal) },
                {ItemID.Stake, new ItemTypeInfo(Element.normal) },
                {ItemID.ExplosiveJackOLantern, new ItemTypeInfo(Element.normal) },
                {ItemID.CrystalDart, new ItemTypeInfo(Element.fairy) },
                {ItemID.CursedDart, new ItemTypeInfo(Element.ghost) },
                {ItemID.IchorDart, new ItemTypeInfo(Element.blood) },
                {ItemID.Nail, new ItemTypeInfo(Element.steel) },
                {ItemID.Ale, new ItemTypeInfo(Element.fighting) },
                {ItemID.ChlorophyteBullet, new ItemTypeInfo(Element.grass) },
                {ItemID.ChlorophyteArrow, new ItemTypeInfo(Element.grass) },
                {ItemID.EndlessMusketPouch, new ItemTypeInfo(Element.normal) },
                {ItemID.EndlessQuiver, new ItemTypeInfo(Element.normal) },
                {ItemID.MoonlordBullet, new ItemTypeInfo(Element.electric) },
                {ItemID.MoonlordArrow, new ItemTypeInfo(Element.electric) },
                {ItemID.HighVelocityBullet, new ItemTypeInfo(Element.fighting) }
            };
        }

        public static void Unload()
        {
            Type = null;
        }

        public static Dictionary<int, ItemTypeInfo> Type;
    }
}
