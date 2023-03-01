using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.TypeLoaders;

namespace TerraTyping.DataTypes
{
    public class NPCWrapper : Wrapper,
        IDefensiveElements, IOffensiveType, ITarget, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed,
        AbilityLookup.IAttractProjectileTarget
    {
        #region Biomes
        public Player GetPlayer() => Main.player[NPC.FindClosestPlayer()];
        public bool ZoneBeach { get => GetPlayer().ZoneBeach; }
        public bool ZoneCorrupt { get => GetPlayer().ZoneCorrupt; }
        public bool ZoneCrimson { get => GetPlayer().ZoneCrimson; }
        public bool ZoneDesert { get => GetPlayer().ZoneDesert; }
        public bool ZoneDirtLayerHeight { get => GetPlayer().ZoneDirtLayerHeight; }
        public bool ZoneDungeon { get => GetPlayer().ZoneDungeon; }
        public bool ZoneGlowshroom { get => GetPlayer().ZoneGlowshroom; }
        public bool ZoneHoly { get => GetPlayer().ZoneHallow; }
        public bool ZoneJungle { get => GetPlayer().ZoneJungle; }
        public bool ZoneMeteor { get => GetPlayer().ZoneMeteor; }
        public bool ZoneOldOneArmy { get => GetPlayer().ZoneOldOneArmy; }
        public bool ZoneOverworldHeight { get => GetPlayer().ZoneOverworldHeight; }
        public bool ZonePeaceCandle { get => GetPlayer().ZonePeaceCandle; }
        public bool ZoneRain { get => GetPlayer().ZoneRain; }
        public bool ZoneRockLayerHeight { get => GetPlayer().ZoneRockLayerHeight; }
        public bool ZoneSandstorm { get => GetPlayer().ZoneSandstorm; }
        public bool ZoneSkyHeight { get => GetPlayer().ZoneSkyHeight; }
        public bool ZoneSnow { get => GetPlayer().ZoneSnow; }
        public bool ZoneTowerNebula { get => GetPlayer().ZoneTowerNebula; }
        public bool ZoneTowerSolar { get => GetPlayer().ZoneTowerSolar; }
        public bool ZoneTowerStardust { get => GetPlayer().ZoneTowerStardust; }
        public bool ZoneTowerVortex { get => GetPlayer().ZoneTowerVortex; }
        public bool ZoneUndergroundDesert { get => GetPlayer().ZoneUndergroundDesert; }
        public bool ZoneUnderworldHeight { get => GetPlayer().ZoneUnderworldHeight; }
        public bool ZoneWaterCandle { get => GetPlayer().ZoneWaterCandle; }
        #endregion

        readonly int npcIndex;
        readonly int npcType;
        public NPC NPC => Main.npc[npcIndex];
        private NPCTyping NPCTyping => NPC.GetGlobalNPC<NPCTyping>();

        public ElementArray DefensiveElements => NPCTyping.Elements;
        public ElementArray OffensiveElements => NPCTypeLoader.GetOffensiveElements(NPC);

        public AbilityID GetAbility => NPCTyping.AbilityID;

        public void ModifyEffectiveness(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement) { }

        public bool Melee => true;
        public bool Ranged => false;
        public bool Magic => false;
        public bool Summon => false;

        public EntityType EntityType => EntityType.NPC;
        public bool Boss => NPC.boss;
        public int Life => NPC.life;
        public int LifeMax => NPC.lifeMax;
        public bool Active => NPC.active;
        public bool Immortal => NPC.immortal;
        public int LifeRegen { get => NPC.lifeRegen; set => NPC.lifeRegen = value; }
        public int LifeRegenTime { get => 0; set => _ = value; }
        public Rectangle Hitbox => GetRect();

        public AbilityID ModifiedAbility
        {
            get => NPCTyping.ModifiedAbility;
            set => NPCTyping.ModifiedAbility = value;
        }
        public ElementArray ModifiedElements
        {
            get => NPCTyping.ModifiedElements;
            set => NPCTyping.ModifiedElements = value;
        }

        public bool UseModifiedAbility
        {
            get => NPCTyping.UseModifiedAbility;
            set => NPCTyping.UseModifiedAbility = value;
        }
        public bool UseModifiedElements
        {
            get => NPCTyping.UseModifiedElements;
            set => NPCTyping.UseModifiedElements = value;
        }


        private NPCWrapper(NPC npc)
        {
            npcIndex = npc.whoAmI;
            npcType = npc.type;
        }

        public static NPCWrapper GetWrapper(NPC npc)
        {
            return new NPCWrapper(npc);
        }

        public void AddBuff(int type, int time, bool quiet = false)
        {
            NPC.AddBuff(type, time, quiet);
        }
        public bool HasBuff(int type)
        {
            return NPC.HasBuff(type);
        }
        public void DelBuff(int b)
        {
            NPC.DelBuff(b);
        }
        public void RemoveBuff(int type)
        {
            for (int i = 0; i < NPC.maxBuffs; i++)
            {
                if (NPC.buffType[i] > 0 && NPC.buffTime[i] > 0)
                {
                    if (BuffID.Sets.NurseCannotRemoveDebuff[NPC.buffType[i]])
                    {
                        NPC.DelBuff(i);
                        i--;
                    }
                }
            }
        }

        public Rectangle GetRect()
        {
            return NPC.getRect();
        }

        public void HealEffect(int healAmount, bool broadcast = true)
        {
            NPC.HealEffect(healAmount, broadcast);
        }

        /// <summary>
        /// Does the effect automatically
        /// </summary>
        public void Heal(int healAmount, bool broadcast = true)
        {
            int heal = Math.Min(NPC.lifeMax - NPC.life, healAmount);
            NPC.life += heal;
            NPC.HealEffect(heal, broadcast);
        }

        public int GetCombatTextCooldown()
        {
            NPCTyping npcTyping = NPC.GetGlobalNPC<NPCTyping>();
            return npcTyping.CombatTextCooldown;
        }
        public int GetCombatHealCooldown()
        {
            NPCTyping npcTyping = NPC.GetGlobalNPC<NPCTyping>();
            return npcTyping.CombatHealCooldown;
        }

        public void UseCombatTextCooldown()
        {
            NPCTyping npcTyping = NPC.GetGlobalNPC<NPCTyping>();
            npcTyping.UseCombatTextCooldown();
        }
        public void UseCombatHealCooldown()
        {
            NPCTyping npcTyping = NPC.GetGlobalNPC<NPCTyping>();
            npcTyping.UseCombatHealCooldown();
        }

        public Team GetTeam() => NPC.friendly ? Team.PlayerFriendly : Team.EnemyNPC;

        public float DamageMultiplication() => NPC.GetGlobalNPC<NPCTyping>().DamageMultiplyByBuff;
    }
}
