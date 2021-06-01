using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Abilities;

namespace TerraTyping.DataTypes
{
    public class NPCWrapper : Wrapper,
        IPrimaryType, ISecondaryType, IOffensiveType, ITarget, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed, 
        AbilityLookup.IAttractProjectileTarget
    {
        readonly int npc;
        public NPC GetNPC() => Main.npc[npc];

        NPCTypeInfo GetTypeInfo()
        {
            NPCTypeInfo typeInfo = new NPCTypeInfo(Element.none, Element.none, Element.none);
            if (DictionaryHelper.NPC(GetNPC()).ContainsKey(GetNPC().type))
            {
                typeInfo = DictionaryHelper.NPC(GetNPC())[GetNPC().type];
            }
            return typeInfo;
        }

        public ThreeType GetThreeType()
        {
            NPCTypeInfo info = GetTypeInfo();
            return new ThreeType(info.Primary, info.Secondary, info.Offensive);
        }
        public ModifyTypeParameters GetTypeParameters()
        {
            return new ModifyTypeParameters(GetThreeType(), GetNPC());
        }

        public Element Primary => GetNPC().GetGlobalNPC<NPCTyping>().Primary;
        public Element Secondary => GetNPC().GetGlobalNPC<NPCTyping>().Secondary;
        public Element Offensive => GetNPC().GetGlobalNPC<NPCTyping>().Offensive;
        public AbilityID GetAbility
        {
            get
            {
                NPCTyping npcAbility = GetNPC().GetGlobalNPC<NPCTyping>();
                return npcAbility.CurrentAbilityID;
            }
        }

        public bool Melee => true;
        public bool Ranged => false;
        public bool Magic => false;
        public bool Summon => false;

        public EntityType EntityType => EntityType.NPC;
        public bool Boss => GetNPC().boss;
        public int Life => GetNPC().life;
        public int LifeMax => GetNPC().lifeMax;
        public bool Active => GetNPC().active;
        public bool Immortal => GetNPC().immortal;
        public int LifeRegen { get => GetNPC().lifeRegen; set => GetNPC().lifeRegen = value; }
        public int LifeRegenTime { get => 0; set => _ = value; }
        public Rectangle Hitbox => GetRect();
        public Element ModifyType { get => GetNPC().GetGlobalNPC<NPCTyping>().ModifyType; set => GetNPC().GetGlobalNPC<NPCTyping>().ModifyType = value; }

        #region Biomes
        public Player GetPlayer() => Main.player[GetNPC().FindClosestPlayer()];
        public bool ZoneBeach { get => GetPlayer().ZoneBeach; }
        public bool ZoneCorrupt { get => GetPlayer().ZoneCorrupt; }
        public bool ZoneCrimson { get => GetPlayer().ZoneCrimson; }
        public bool ZoneDesert { get => GetPlayer().ZoneDesert; }
        public bool ZoneDirtLayerHeight { get => GetPlayer().ZoneDirtLayerHeight; }
        public bool ZoneDungeon { get => GetPlayer().ZoneDungeon; }
        public bool ZoneGlowshroom { get => GetPlayer().ZoneGlowshroom; }
        public bool ZoneHoly { get => GetPlayer().ZoneHoly; }
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


        public NPCWrapper(NPC npc)
        {
            this.npc = npc.whoAmI;
        }

        public void AddBuff(int type, int time, bool quiet = false)
        {
            GetNPC().AddBuff(type, time, quiet);
        }
        public bool HasBuff(int type)
        {
            return GetNPC().HasBuff(type);
        }
        public void DelBuff(int b)
        {
            GetNPC().DelBuff(b);
        }
        public void RemoveBuff(int type)
        {
            for (int i = 0; i < NPC.maxBuffs; i++)
            {
                if (GetNPC().buffType[i] > 0 && GetNPC().buffTime[i] > 0 && BuffLoader.CanBeCleared(GetNPC().buffType[i]))
                {
                    GetNPC().DelBuff(i);
                    i--;
                }
            }
        }

        public Rectangle GetRect()
        {
            return GetNPC().getRect();
        }

        public void HealEffect(int healAmount, bool broadcast = true)
        {
            GetNPC().HealEffect(healAmount, broadcast);
        }

        /// <summary>
        /// Does the effect automatically
        /// </summary>
        public void Heal(int healAmount, bool broadcast = true)
        {
            int heal = Math.Min(GetNPC().lifeMax - GetNPC().life, healAmount);
            GetNPC().life += heal;
            GetNPC().HealEffect(heal, broadcast);
        }

        public int GetCombatTextCooldown()
        {
            NPCTyping npcTyping = GetNPC().GetGlobalNPC<NPCTyping>();
            return npcTyping.CombatTextCooldown;
        }
        public int GetCombatHealCooldown()
        {
            NPCTyping npcTyping = GetNPC().GetGlobalNPC<NPCTyping>();
            return npcTyping.CombatHealCooldown;
        }

        public void UseCombatTextCooldown()
        {
            NPCTyping npcTyping = GetNPC().GetGlobalNPC<NPCTyping>();
            npcTyping.UseCombatTextCooldown();
        }
        public void UseCombatHealCooldown()
        {
            NPCTyping npcTyping = GetNPC().GetGlobalNPC<NPCTyping>();
            npcTyping.UseCombatHealCooldown();
        }

        public Team GetTeam() => GetNPC().friendly ? Team.PlayerFriendly : Team.EnemyNPC;

        public float DamageMultiplication() => GetNPC().GetGlobalNPC<NPCTyping>().DamageMultiplyByBuff;
    }
}
