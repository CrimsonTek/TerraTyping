using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes.Structs;
using Microsoft.Xna.Framework;
using Microsoft.VisualBasic;
using IL.Terraria.GameContent.Achievements;
using TerraTyping.TypeLoaders;
using TerraTyping.Helpers;

namespace TerraTyping.DataTypes
{
    public class ProjectileWrapper : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
    {
        readonly int projectileIndex;

        static ProjectileWrapper[] projectileWrappers;

        /// <summary>
        /// Only works for player projectiles
        /// </summary>
        int PlayerOwnerIndex => Projectile.owner;

        public Owner OwnerType
        {
            get
            {
                if (Projectile.npcProj)
                {
                    return Owner.TownsPerson;
                }
                else if (Projectile.hostile)
                {
                    return Owner.NPC;
                }
                else if (Projectile.friendly)
                {
                    return Owner.Player;
                }
                else
                {
                    return Owner.Unknown;
                }
            }
        }
        public Projectile Projectile => Main.projectile[projectileIndex];

        public ElementArray OffensiveElements => ProjectileTypeLoader.GetElements(Projectile);

        public AbilityID GetAbility
        {
            get
            {
                if (OwnerType is Owner.Player)
                {
                    if (PlayerOwnerIndex > Main.maxPlayers)
                    {
                        return AbilityID.None;
                    }
                    Player player = Main.player[PlayerOwnerIndex];
                    if (player == null || !player.active)
                    {
                        return AbilityID.None;
                    }
                    PlayerWrapper playerWrapper = PlayerWrapper.GetWrapper(player);
                    return playerWrapper.GetAbility;
                }
                else
                {
                    return AbilityID.None;
                }
            }
        }

        public void ModifyEffectiveness(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement)
        {
            ProjectileTypeLoader.ModifyEffectiveness(ref baseEffectiveness, offensiveElement, defensiveElement, Projectile);
        }

        public Rectangle Hitbox => Projectile.Hitbox;

        public bool Melee => Projectile.CountsAsClass(DamageClass.Melee);
        public bool Ranged => Projectile.CountsAsClass(DamageClass.Ranged);
        public bool Magic => Projectile.CountsAsClass(DamageClass.Magic);
        public bool Summon => Projectile.CountsAsClass(DamageClass.Summon);

        ProjectileWrapper(int index)
        {
            projectileIndex = index;
        }

        public static ProjectileWrapper GetWrapper(int index)
        {
            return projectileWrappers[index];
        }

        public static ProjectileWrapper GetWrapper(Projectile projectile)
        {
            return projectileWrappers[projectile.whoAmI];
        }

        internal static void PostSetupContent()
        {
            projectileWrappers = new ProjectileWrapper[Main.maxProjectiles];
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                projectileWrappers[i] = new ProjectileWrapper(i);
            }
        }

        internal static void Unload()
        {
            projectileWrappers = null;
        }

        public Team GetTeam()
        {
            if (Projectile.npcProj || Projectile.friendly)
            {
                return Team.PlayerFriendly;
            }
            else if (Projectile.hostile)
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
            Projectile.Kill();
        }
    }
}
