using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerraTyping.DataTypes.Structs;
using Microsoft.Xna.Framework;

namespace TerraTyping.DataTypes
{
    public class ProjectileWrapper : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
    {
        readonly int projectile;
        /// <summary>
        /// Only works for player projectiles
        /// </summary>
        int OwnerIndex => GetProjectile().owner;
        public Owner OwnerType
        {
            get
            {
                if (GetProjectile().npcProj)
                {
                    return Owner.TownsPerson;
                }
                else if (GetProjectile().hostile)
                {
                    return Owner.NPC;
                }
                else if (GetProjectile().friendly)
                {
                    return Owner.Player;
                }
                else
                {
                    return Owner.Unknown;
                }
            }
        }
        public Projectile GetProjectile() => Main.projectile[projectile];

        public Element Offensive
        {
            get
            {
                Element element = Element.none;
                if (DictionaryHelper.Projectile(GetProjectile()).ContainsKey(GetProjectile().type))
                {
                    element = DictionaryHelper.Projectile(GetProjectile())[GetProjectile().type].Offensive;
                }

                return element;
            }
        }
        public AbilityID GetAbility
        {
            get
            {
                switch (OwnerType)
                {
                    case Owner.Player:
                        if (OwnerIndex > Main.maxPlayers)
                        {
                            return AbilityID.None;
                        }
                        Player player = Main.player[OwnerIndex];
                        if (player == null || !player.active)
                        {
                            return AbilityID.None;
                        }
                        PlayerWrapper playerWrapper = new PlayerWrapper(player);
                        return playerWrapper.GetAbility;
                    case Owner.TownsPerson:
                    case Owner.NPC:
                        //if (OwnerIndex > Main.maxNPCs)
                        //{
                        //    return AbilityID.None;
                        //}
                        //NPC npc = Main.npc[OwnerIndex];
                        //if (npc == null || !npc.active)
                        //{
                        //    return AbilityID.None;
                        //}
                        //NPCWrapper npcWrapper = new NPCWrapper(npc);
                        //return npcWrapper.GetAbility;
                    case Owner.Unknown:
                    default:
                        return AbilityID.None;
                }
            }
        }

        public Rectangle Hitbox => GetProjectile().Hitbox;

        public bool Melee => GetProjectile().melee;
        public bool Ranged => GetProjectile().ranged;
        public bool Magic => GetProjectile().magic;
        public bool Summon => false; // update in 1.14

        public ProjectileWrapper(Projectile proj)
        {
            projectile = proj.whoAmI;
        }

        public Team GetTeam()
        {
            if (GetProjectile().npcProj || GetProjectile().friendly)
            {
                return Team.PlayerFriendly;
            }
            else if (GetProjectile().hostile)
            {
                return Team.EnemyNPC;
            }
            else
            {
                return Team.Unknown;
            }
        }

        public float DamageMultiplication()
        {
            //if (OwnerType == Owner.Player)
            //{
            //    Player player = Main.player[OwnerIndex];
            //    if (player != null && player.active)
            //    {
            //        return player.GetModPlayer<PlayerTyping>().DamageMultiplyByBuff;
            //    }
            //}

            return 1;
        }

        public void Kill()
        {
            GetProjectile().Kill();
        }
    }
}
