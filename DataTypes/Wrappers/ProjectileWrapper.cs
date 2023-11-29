using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerraTyping.TypeLoaders;
using TerraTyping.Core;
using Terraria.DataStructures;
using System;
using TerraTyping.Common.TModLoaderGlobals;
using Terraria.ID;
using System.Composition;

namespace TerraTyping.DataTypes
{
    public class ProjectileWrapper : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
    {
        readonly int projectileIndex;

        static ProjectileWrapper[] projectileWrappers;

        int? npcOwnerIndex;
        /// <summary>
        /// Only works for player projectiles
        /// </summary>
        int playerOwnerIndex;

        public Owner OwnerType { get; private set; }
        public Projectile Projectile => Main.projectile[projectileIndex];
        public ElementArray OffensiveElements => ProjectileTypeLoader.GetElements(Projectile);
        public Ability GetAbility
        {
            get
            {
                if (OwnerType is Owner.Player)
                {
                    if (playerOwnerIndex > Main.maxPlayers)
                    {
                        return Ability.None;
                    }
                    Player player = Main.player[playerOwnerIndex];
                    if (player == null || !player.active)
                    {
                        return Ability.None;
                    }
                    PlayerWrapper playerWrapper = PlayerWrapper.GetWrapper(player);
                    return playerWrapper.GetAbility;
                }
                else
                {
                    return Ability.None;
                }
            }
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

        public void ModifyEffectiveness(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement)
        {
            ProjectileTypeLoader.ModifyEffectiveness(ref baseEffectiveness, offensiveElement, defensiveElement, Projectile);
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
        public float DamageMultiplication() => 1;
        public void Kill()
        {
            Projectile.Kill();
            npcOwnerIndex = null;
            playerOwnerIndex = 0;
        }
        public void OnSpawn(Projectile projectile, IEntitySource source)
        {
            playerOwnerIndex = projectile.owner;

            if (Projectile.hostile)
            {
                OwnerType = Owner.EnemyNPC;
            }
            else if (Projectile.npcProj)
            {
                OwnerType = Owner.TownsPerson;
            }
            else if (Projectile.friendly)
            {
                OwnerType = Owner.Player;
            }
            else
            {
                OwnerType = Owner.Unknown;
            }

            if (source is EntitySource_Parent parent)
            {
                if (parent.Entity is NPC npc && npc.TryGetGlobalNPC(out NPCTyping globalNPC))
                {
                    npcOwnerIndex = npc.whoAmI;
                    globalNPC.OnKillEvent += (NPC _npc) =>
                    {
                        npcOwnerIndex = null;
                    };

                    if (projectile.hostile ^ projectile.npcProj)
                    {
                        OwnerType = projectile.hostile ? Owner.EnemyNPC : Owner.TownsPerson;
                    }
                    else
                    {
                        OwnerType = !npc.friendly ? Owner.EnemyNPC : Owner.TownsPerson;
                    }
                }
                else if (parent.Entity is Player player)
                {
                    playerOwnerIndex = player.whoAmI;
                    OwnerType = Owner.Player;
                }
                else if (parent.Entity is Projectile projParent)
                {
                    ProjectileWrapper wrapper = GetWrapper(projParent);
                    playerOwnerIndex = wrapper.playerOwnerIndex;
                    npcOwnerIndex = wrapper.npcOwnerIndex;
                    OwnerType = wrapper.OwnerType;
                    if (npcOwnerIndex is int i && Main.npc.IndexInRange(i) && Main.npc[i].TryGetGlobalNPC(out NPCTyping globalNPC2))
                    {
                        globalNPC2.OnKillEvent += (NPC _npc) =>
                        {
                            npcOwnerIndex = null;
                        };
                    }
                }
            }
        }
    }
}
