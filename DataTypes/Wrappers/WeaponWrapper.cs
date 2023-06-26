using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using TerraTyping.TypeLoaders;
using TerraTyping.Common.TModLoaderGlobals;
using TerraTyping.Core;

namespace TerraTyping.DataTypes
{
    public class WeaponWrapper : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
    {
        public readonly Item item;
        public int Player { get; }

        public bool GetsStab => WeaponTypeLoader.GetsStab(item);
        public ElementArray OffensiveElements => WeaponTypeLoader.GetElements(item);
        public Ability GetAbility
        {
            get
            {
                if (Player > Main.maxPlayers)
                {
                    return Ability.None;
                }
                Player thisPlayer = Main.player[Player];

                if (thisPlayer == null || !thisPlayer.active)
                {
                    return Ability.None;
                }
                PlayerWrapper playerWrapper = PlayerWrapper.GetWrapper(thisPlayer);

                return playerWrapper.GetAbility;
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                return (Rectangle)item.GetGlobalItem<ItemTyping>().meleeHitbox[Player];
            }
        }

        public void ModifyEffectiveness(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement) { }

        public bool Melee => item.CountsAsClass(DamageClass.Melee);
        public bool Ranged => item.CountsAsClass(DamageClass.Ranged);
        public bool Magic => item.CountsAsClass(DamageClass.Magic);
        public bool Summon => item.CountsAsClass(DamageClass.Summon);

        public WeaponWrapper(Item item, Player player)
        {
            this.item = item;
            this.Player = player.whoAmI;
        }

        public static WeaponWrapper GetWrapper(Item item, Player player)
        {
            return new WeaponWrapper(item, player);
        }

        public Team GetTeam() => Team.PlayerFriendly;

        public float DamageMultiplication()
        {
            //return Main.player[player].GetModPlayer<PlayerTyping>().DamageMultiplyByBuff;
            return 1;
        }
    }
}
