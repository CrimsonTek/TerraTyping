using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Core.Abilities;
using TerraTyping.DataTypes;

namespace TerraTyping.Common.TModLoaderGlobals
{
    public class ProjectileTyping : GlobalProjectile
    {
        private const int homeRange = 250;
        private const double maxRotateSpeed = 0.08;

        public override bool InstancePerEntity => true;

        public override bool PreAI(Projectile projectile)
        {
            ProjectileWrapper projWrapper = ProjectileWrapper.GetWrapper(projectile);

            if (projWrapper.GetTeam() == Team.PlayerFriendly || projWrapper.GetTeam() == Team.Unknown)
            {
                ClosestTarget closest = ClosestTarget.Null;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc != null && npc.active && npc.type != NPCID.None)
                    {
                        NPCWrapper npcWrapper = NPCWrapper.GetWrapper(npc);
                        if (npc.GetGlobalNPC<NPCTyping>().GetAbility.AttractProjectile(
                            new AbilityLookup.AttractProjectileParameters(projWrapper, npcWrapper)))
                        {
                            ClosestTarget newClosest = new ClosestTarget(npc, projectile);
                            if (newClosest.distance < closest.distance)
                            {
                                closest = newClosest;
                            }
                        }
                    }
                }
                if (closest.id >= 0)
                {
                    ProjectileHome(projectile, closest, Main.npc[closest.id].Center);
                }
            }
            else if (projWrapper.GetTeam() == Team.EnemyNPC)
            {
                ClosestTarget closest = ClosestTarget.Null;
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (player != null && player.active)
                    {
                        PlayerWrapper playerWrapper = PlayerWrapper.GetWrapper(player);
                        if (playerWrapper.PlayerTyping.GetAbility().AttractProjectile(
                            new AbilityLookup.AttractProjectileParameters(projWrapper, playerWrapper)))
                        {
                            ClosestTarget newClosest = new ClosestTarget(player, projectile);
                            if (newClosest.distance < closest.distance)
                            {
                                closest = newClosest;
                            }
                        }
                    }
                }
                if (closest.id >= 0)
                {
                    ProjectileHome(projectile, closest, Main.player[closest.id].Center);
                }
            }

            return base.PreAI(projectile);
        }

        private static void ProjectileHome(Projectile projectile, ClosestTarget closest, Vector2 target)
        {
            if (closest.distance <= homeRange)
            {
                Vector2 direction = target - projectile.Center;
                Vector2 normal = new Vector2(direction.Y, -direction.X);
                float dotProduct = Vector2.Dot(normal, projectile.velocity);
                float angleToTarget = direction.ToRotation();
                float angleDifference = projectile.velocity.ToRotation() - angleToTarget;
                double rotateSpeed = projectile.velocity.Length() * 0.004 * ((homeRange - closest.distance) * 0.01 + 1);
                rotateSpeed = Math.Min(rotateSpeed, maxRotateSpeed);
                if (Math.Abs(angleDifference) >= rotateSpeed)
                {
                    if (dotProduct == 0)
                    {
                        projectile.velocity = projectile.velocity.RotatedBy(rotateSpeed);
                    }
                    else if (dotProduct > 0)
                    {
                        projectile.velocity = projectile.velocity.RotatedBy(rotateSpeed);
                        //projectile.AngleTo()
                    }
                    else if (dotProduct < 0)
                    {
                        projectile.velocity = projectile.velocity.RotatedBy(-rotateSpeed);
                    }
                }
                else
                {
                    projectile.velocity = Vector2.Normalize(target - projectile.Center) * projectile.velocity.Length();
                }
            }
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            ProjectileWrapper wrapper = ProjectileWrapper.GetWrapper(projectile);
            wrapper.OnSpawn(projectile, source);
        }

        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= ServerConfig.Instance.BalanceConfigInstance.EnemyProjectileDamageScaling;
        }

        struct ClosestTarget
        {
            public double distance;
            public int id;

            public static ClosestTarget Null => new ClosestTarget()
            {
                distance = double.PositiveInfinity,
                id = -1
            };

            public ClosestTarget(NPC npc, Projectile projectile)
            {
                id = npc.whoAmI;
                distance = Vector2.Distance(npc.Center, projectile.Center);
            }

            public ClosestTarget(Player player, Projectile projectile)
            {
                id = player.whoAmI;
                distance = Vector2.Distance(player.Center, projectile.Center);
            }
        }
    }
}
