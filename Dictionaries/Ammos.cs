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
    public class Ammos
    {
        public static Dictionary<int, Element.Type> Type = new Dictionary<int, Element.Type>
        {
            // ammo
            {ItemID.WoodenArrow, Element.Type.normal },
            {ItemID.FlamingArrow, Element.Type.fire },
            {ItemID.UnholyArrow, Element.Type.dark },
            {ItemID.JestersArrow, Element.Type.flying },
            {ItemID.MusketBall, Element.Type.normal },
            {ItemID.Star, Element.Type.flying },
            {ItemID.MeteorShot, Element.Type.rock },
            {ItemID.HellfireArrow, Element.Type.fire },
            {ItemID.SandBlock, Element.Type.ground },
            {ItemID.Seed, Element.Type.grass },
            {ItemID.EbonsandBlock, Element.Type.ground },
            {ItemID.PearlsandBlock, Element.Type.ground },
            {ItemID.CrystalBullet, Element.Type.fairy },
            {ItemID.HolyArrow, Element.Type.fairy },
            {ItemID.CursedArrow, Element.Type.ghost },
            {ItemID.CursedBullet, Element.Type.ghost },
            {ItemID.Snowball, Element.Type.ice },
            {ItemID.BoneArrow, Element.Type.bone },
            {ItemID.FrostburnArrow, Element.Type.ice },
            {ItemID.RocketI, Element.Type.normal },
            {ItemID.RocketII, Element.Type.normal },
            {ItemID.RocketIII, Element.Type.normal },
            {ItemID.RocketIV, Element.Type.normal },
            {ItemID.Flare, Element.Type.fire },
            {ItemID.BlueFlare, Element.Type.fire },
            {ItemID.CopperCoin, Element.Type.normal },
            {ItemID.SilverCoin, Element.Type.normal },
            {ItemID.GoldCoin, Element.Type.normal },
            {ItemID.PlatinumCoin, Element.Type.normal },
            {ItemID.Gel, Element.Type.fire },
            {ItemID.StyngerBolt, Element.Type.fighting },
            {ItemID.PoisonDart, Element.Type.poison },
            {ItemID.IchorArrow, Element.Type.blood },
            {ItemID.IchorBullet, Element.Type.blood },
            {ItemID.ExplosiveBunny, Element.Type.normal },
            {ItemID.VenomArrow, Element.Type.poison },
            {ItemID.VenomBullet, Element.Type.poison },
            {ItemID.PartyBullet, Element.Type.fairy },
            {ItemID.NanoBullet, Element.Type.psychic },
            {ItemID.ExplodingBullet, Element.Type.normal },
            {ItemID.GoldenBullet, Element.Type.steel },
            {ItemID.CandyCorn, Element.Type.normal },
            {ItemID.Stake, Element.Type.normal },
            {ItemID.CrystalDart, Element.Type.fairy },
            {ItemID.CursedDart, Element.Type.ghost },
            {ItemID.IchorDart, Element.Type.blood },
            {ItemID.Nail, Element.Type.steel },
            {ItemID.Ale, Element.Type.fighting },
            {ItemID.ChlorophyteBullet, Element.Type.grass },
            {ItemID.ChlorophyteArrow, Element.Type.grass },
            {ItemID.EndlessMusketPouch, Element.Type.normal },
            {ItemID.EndlessQuiver, Element.Type.normal },
        };
    }
}
