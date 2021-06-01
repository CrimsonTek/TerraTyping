using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    public class ProjectileTyping : GlobalProjectile
    {
        private const int homeRange = 250;
        private const double maxRotateSpeed = 0.08;

        public override bool PreAI(Projectile projectile)
        {
            ProjectileWrapper projWrapper = new ProjectileWrapper(projectile);

            if (projWrapper.GetTeam() == Team.PlayerFriendly || projWrapper.GetTeam() == Team.Unknown)
            {
                ClosestTarget closest = ClosestTarget.Null;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc != null && npc.active)
                    {
                        NPCWrapper npcWrapper = new NPCWrapper(npc);
                        if (npc.GetGlobalNPC<NPCTyping>().GetAbility().AttractProjectile(
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
                        PlayerWrapper playerWrapper = new PlayerWrapper(player);
                        if (playerWrapper.GetModPlayer<PlayerTyping>().GetAbility().AttractProjectile(
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
                double rotateSpeed = (projectile.velocity.Length() * 0.004) * (((homeRange - closest.distance) * 0.01) + 1);
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
                    projectile.velocity = Vector2.Normalize((target - projectile.Center)) * projectile.velocity.Length();
                }
            }
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
