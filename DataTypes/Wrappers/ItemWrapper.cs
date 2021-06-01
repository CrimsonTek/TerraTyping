using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;

namespace TerraTyping.DataTypes
{
    public class ItemWrapper : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
    {
        readonly Item item;
        public int Player { get; }

        public Element Offensive
        {
            get
            {
                Element element = Element.none;
                if (DictionaryHelper.Item(item).ContainsKey(item.type))
                {
                    element = DictionaryHelper.Item(item)[item.type].Offensive;
                }

                return element;
            }
        }
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
                PlayerWrapper playerWrapper = new PlayerWrapper(thisPlayer);

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

        public bool Melee => item.melee;
        public bool Ranged => item.ranged;
        public bool Magic => item.magic;
        public bool Summon => item.summon;


        public ItemWrapper(Item item, Player player)
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
