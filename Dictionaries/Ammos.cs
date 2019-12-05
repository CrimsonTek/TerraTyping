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

namespace TerraTyping
{
    public class Ammos
    {
        public static Dictionary<int, Element> Type = new Dictionary<int, Element>
        {
            // ammo
            {ItemID.WoodenArrow, Element.normal },
            {ItemID.FlamingArrow, Element.fire },
            {ItemID.UnholyArrow, Element.dark },
            {ItemID.JestersArrow, Element.flying },
            {ItemID.MusketBall, Element.normal },
            {ItemID.Star, Element.flying },
            {ItemID.MeteorShot, Element.rock },
            {ItemID.HellfireArrow, Element.fire },
            {ItemID.SandBlock, Element.ground },
            {ItemID.Seed, Element.grass },
            {ItemID.EbonsandBlock, Element.ground },
            {ItemID.PearlsandBlock, Element.ground },
            {ItemID.CrystalBullet, Element.fairy },
            {ItemID.HolyArrow, Element.fairy },
            {ItemID.CursedArrow, Element.ghost },
            {ItemID.CursedBullet, Element.ghost },
            {ItemID.Snowball, Element.ice },
            {ItemID.BoneArrow, Element.bone },
            {ItemID.FrostburnArrow, Element.ice },
            {ItemID.RocketI, Element.normal },
            {ItemID.RocketII, Element.normal },
            {ItemID.RocketIII, Element.normal },
            {ItemID.RocketIV, Element.normal },
            {ItemID.Flare, Element.fire },
            {ItemID.BlueFlare, Element.fire },
            {ItemID.CopperCoin, Element.normal },
            {ItemID.SilverCoin, Element.normal },
            {ItemID.GoldCoin, Element.normal },
            {ItemID.PlatinumCoin, Element.normal },
            {ItemID.Gel, Element.fire },
            {ItemID.StyngerBolt, Element.fighting },
            {ItemID.PoisonDart, Element.poison },
            {ItemID.IchorArrow, Element.blood },
            {ItemID.IchorBullet, Element.blood },
            {ItemID.ExplosiveBunny, Element.normal },
            {ItemID.VenomArrow, Element.poison },
            {ItemID.VenomBullet, Element.poison },
            {ItemID.PartyBullet, Element.fairy },
            {ItemID.NanoBullet, Element.psychic },
            {ItemID.ExplodingBullet, Element.normal },
            {ItemID.GoldenBullet, Element.steel },
            {ItemID.CandyCorn, Element.normal },
            {ItemID.Stake, Element.normal },
            {ItemID.CrystalDart, Element.fairy },
            {ItemID.CursedDart, Element.ghost },
            {ItemID.IchorDart, Element.blood },
            {ItemID.Nail, Element.steel },
            {ItemID.Ale, Element.fighting },
            {ItemID.ChlorophyteBullet, Element.grass },
            {ItemID.ChlorophyteArrow, Element.grass },
            {ItemID.EndlessMusketPouch, Element.normal },
            {ItemID.EndlessQuiver, Element.normal },
            {ItemID.MoonlordBullet, Element.electric },
            {ItemID.MoonlordArrow, Element.electric }
        };
    }
}
