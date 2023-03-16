using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.Abilities;
using System.Collections.Generic;
using System.Linq;
using TerraTyping.Abilities.Buffs;
using TerraTyping.DataTypes.Structs;
using System;
using TerraTyping.Abilities.Buffs.TypeReplace;
using System.CodeDom;
using TerraTyping.TypeLoaders;
using TerraTyping.Helpers;
using TerraTyping.Common.Configs;

namespace TerraTyping
{
    public class NPCTyping : GlobalNPC
    {
        /// <summary>
        /// Ability set by armor and accessories
        /// </summary>
        private AbilityID baseAbility;
        /// <summary>
        /// Elements set by armor and accessories
        /// </summary>
        private ElementArray baseElements;

        /// <summary>
        /// Ability set by buffs
        /// </summary>
        public AbilityID ModifiedAbility { get; set; }
        /// <summary>
        /// Elements set by buffs
        /// </summary>
        public ElementArray ModifiedElements { get; set; }

        /// <summary>
        /// Whether or not <see cref="ModifiedAbility"/> should be used. Set this every frame to use.
        /// </summary>
        public bool UseModifiedAbility { get; set; }
        /// <summary>
        /// Whether or not <see cref="ModifiedElements"/> should be used. Set this every frame to use.
        /// </summary>
        public bool UseModifiedElements { get; set; }

        public AbilityID AbilityID => UseModifiedAbility ? ModifiedAbility : baseAbility;
        public ElementArray Elements => UseModifiedElements ? ModifiedElements : baseElements;

        private bool initialized;

        public bool waterCompactionBuff;

        public NPC NPC { get; internal set; }

        public Ability GetAbility => AbilityLookup.GetAbility(AbilityID);

        public float damageMultiplyByBuff = 1;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
            NPC = npc;
            initialized = false;
        }

        public override void ResetEffects(NPC npc)
        {
            TryInitialize(npc);

            DecrementCombatCD();

            damageMultiplyByBuff = 1;

            UseModifiedAbility = false;
            UseModifiedElements = false;

            waterCompactionBuff = false;
        }

        void TryInitialize(NPC npc)
        {
            if (initialized || npc is null)
            {
                return;
            }

            int type = npc.type;
            AbilityContainer abilityContainer = NPCTypeLoader.GetAbilities(type);
            float chance = ModContent.GetInstance<ServerConfig>()?.HiddenAbilityChancePercent ?? 0;

            AbilityID ability = abilityContainer.PrimaryAbility;
            if (abilityContainer.SecondaryAbility != AbilityID.None)
            {
                if (Main.rand.NextDouble() < 0.5)
                {
                    ability = abilityContainer.SecondaryAbility;
                }
            }

            if (abilityContainer.HiddenAbility != AbilityID.None)
            {
                if (Main.rand.NextDouble() < (chance * 0.01))
                {
                    ability = abilityContainer.HiddenAbility;
                }
            }

            baseAbility = ability;

            baseElements = NPCTypeLoader.GetDefensiveElements(npc);

            initialized = true;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            GetAbility.UpdateLifeRegen(NPCWrapper.GetWrapper(npc), TargetType.NPC);
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (waterCompactionBuff)
            {
                damage -= (ServerConfig.Instance.AbilityConfigInstance.WaterCompactionDefenseBoostNPC / 2);
            }

            ModifyHitBy(new WeaponWrapper(item, player), NPCWrapper.GetWrapper(npc), ref damage, ref knockback, ref crit, npc);

        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (waterCompactionBuff)
            {
                damage -= (ServerConfig.Instance.AbilityConfigInstance.WaterCompactionDefenseBoostNPC / 2);
            }

            ModifyHitBy(ProjectileWrapper.GetWrapper(projectile), NPCWrapper.GetWrapper(npc), ref damage, ref knockback, ref crit, npc);
        }

        private static void ModifyHitBy<A, D>(A attacker, D defender, ref int damage, ref float knockback, ref bool crit, NPC npc)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
            where D : Wrapper, ITarget, IDefensiveElements, IAbility, ITeam
        {
            // damage is before defense
            // estimate damage with defense
            damage -= (int)(npc.defense * 0.5f);
            // do calculation
            Calc.OnHit(attacker, defender);
            Calc.ModifyHitBy(attacker, defender, ref damage, ref knockback, ref crit);
            // add damage from defense
            damage += (int)(npc.defense * 0.5f);
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            WeaponWrapper itemWrapper = new WeaponWrapper(item, player);
            NPCWrapper npcWrapper = NPCWrapper.GetWrapper(npc);
            Calc.OnHit(itemWrapper, npcWrapper);
            return Calc.CanBeHitBy(itemWrapper, npcWrapper) ? null : false;
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            ProjectileWrapper projectileWrapper = ProjectileWrapper.GetWrapper(projectile);
            NPCWrapper npcWrapper = NPCWrapper.GetWrapper(npc);
            Calc.OnHit(projectileWrapper, npcWrapper);
            return Calc.CanBeHitBy(projectileWrapper, npcWrapper) ? null : false;
        }

        #region CombatTextCooldown
        private const int CombatTextCooldownConst = 30;
        private const int CombatHealCooldownConst = 30;

        public int CombatTextCooldown { get; internal set; }
        public int CombatHealCooldown { get; internal set; }

        private void DecrementCombatCD()
        {
            if (CombatTextCooldown > 0)
            {
                CombatTextCooldown--;
            }
            if (CombatTextCooldown < 0)
            {
                CombatTextCooldown = 0;
            }

            if (CombatHealCooldown > 0)
            {
                CombatHealCooldown--;
            }
            if (CombatHealCooldown < 0)
            {
                CombatHealCooldown = 0;
            }
        }

        public void UseCombatTextCooldown()
        {
            CombatTextCooldown = CombatTextCooldownConst;
        }
        public void UseCombatHealCooldown()
        {
            CombatHealCooldown = CombatHealCooldownConst;
        }
        #endregion
    }
}
