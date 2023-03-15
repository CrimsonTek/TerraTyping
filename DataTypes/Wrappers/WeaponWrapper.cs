using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using TerraTyping.TypeLoaders;

namespace TerraTyping.DataTypes
{
    public class WeaponWrapper : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
    {
        public readonly Item item;
        public int Player { get; }

        public bool GetsStab => WeaponTypeLoader.GetsStab(item);
        public ElementArray OffensiveElements => WeaponTypeLoader.GetElements(item);
        public AbilityID GetAbility
        {
            get
            {
                if (Player > Main.maxPlayers)
                {
                    return AbilityID.None;
                }
                Player thisPlayer = Main.player[Player];

                if (thisPlayer == null || !thisPlayer.active)
                {
                    return AbilityID.None;
                }
                PlayerWrapper playerWrapper = PlayerWrapper.GetWrapper(thisPlayer);

                return playerWrapper.GetAbility;
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                //Main.NewText(Main.player[Player].HeldItem.TopLeft);
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

        public Team GetTeam() => Team.PlayerFriendly;

        public float DamageMultiplication()
        {
            //return Main.player[player].GetModPlayer<PlayerTyping>().DamageMultiplyByBuff;
            return 1;
        }
    }
}
