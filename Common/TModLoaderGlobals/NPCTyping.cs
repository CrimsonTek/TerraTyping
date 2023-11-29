using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.TypeLoaders;
using TerraTyping.Helpers;
using TerraTyping.Common.Configs;
using TerraTyping.Core;
using TerraTyping.Core.Abilities;
using System;

namespace TerraTyping.Common.TModLoaderGlobals
{
    public class NPCTyping : GlobalNPC
    {
        /// <summary>
        /// Ability set by armor and accessories
        /// </summary>
        private Ability baseAbility;
        /// <summary>
        /// Elements set by armor and accessories
        /// </summary>
        private ElementArray baseElements = ElementArray.Default;

        /// <summary>
        /// Ability set by buffs
        /// </summary>
        public Ability ModifiedAbility { get; set; }
        /// <summary>
        /// Elements set by buffs
        /// </summary>
        public ElementArray ModifiedElements { get; set; } = ElementArray.Default;

        /// <summary>
        /// Whether or not <see cref="ModifiedAbility"/> should be used. Set this every frame to use.
        /// </summary>
        public bool UseModifiedAbility { get; set; }
        /// <summary>
        /// Whether or not <see cref="ModifiedElements"/> should be used. Set this every frame to use.
        /// </summary>
        public bool UseModifiedElements { get; set; }

        public Ability AbilityID => UseModifiedAbility ? ModifiedAbility : baseAbility;
        public ElementArray Elements => UseModifiedElements ? ModifiedElements : baseElements;

        private bool initialized;

        public bool waterCompactionBuff;

        public event Action<NPC> OnKillEvent;

        public NPC NPC { get; internal set; }

        public AbilityFlyweight GetAbility => AbilityLookup.GetAbility(AbilityID);

        public float damageMultiplyByBuff = 1;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
            NPC = npc;
            initialized = false;
            npc.lifeMax = (int)(npc.lifeMax * ServerConfig.Instance.BalanceConfigInstance.EnemyHealthScaling);
        }

        public override void ResetEffects(NPC npc)
        {
            TryInitializeAbility(npc);

            ResetDefensiveElements(npc);

            DecrementCombatCD();

            damageMultiplyByBuff = 1;

            UseModifiedAbility = false;
            UseModifiedElements = false;

            waterCompactionBuff = false;
        }

        void TryInitializeAbility(NPC npc)
        {
            if (initialized || npc is null)
            {
                return;
            }

            baseAbility = SetAbility(NPCTypeLoader.GetAbilities(npc.netID));

            initialized = true;
        }

        void ResetDefensiveElements(NPC npc)
        {
            baseElements = NPCTypeLoader.GetDefensiveElements(npc);
        }

        private static Ability SetAbility(AbilityContainer abilityContainer)
        {
            float haChance = ServerConfig.Instance.HiddenAbilityChancePercent;

            if (abilityContainer.HiddenAbilities.Length > 0 && Main.rand.NextDouble() < haChance * 0.01)
            {
                return abilityContainer.HiddenAbilities.Random();
            }

            if (abilityContainer.BasicAbilities.Length > 0)
            {
                return abilityContainer.BasicAbilities.Random();
            }

            return Ability.None;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            GetAbility.UpdateLifeRegen(NPCWrapper.GetWrapper(npc), TargetType.NPC);
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

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (waterCompactionBuff)
            {
                modifiers.Defense.Flat += ServerConfig.Instance.AbilityConfigInstance.WaterCompactionDefenseBoostNPC;
            }

            Calc.ModifyHitBy(new WeaponWrapper(item, player), NPCWrapper.GetWrapper(npc), ref modifiers.FinalDamage, ref modifiers.Knockback);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (waterCompactionBuff)
            {
                modifiers.Defense.Flat += ServerConfig.Instance.AbilityConfigInstance.WaterCompactionDefenseBoostNPC;
            }

            Calc.NPCModifyHitBy(ProjectileWrapper.GetWrapper(projectile), NPCWrapper.GetWrapper(npc), ref modifiers);
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            Calc.OnHit(new WeaponWrapper(item, player), NPCWrapper.GetWrapper(npc), hit);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            Calc.OnHit(ProjectileWrapper.GetWrapper(projectile), NPCWrapper.GetWrapper(npc), hit);
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= ServerConfig.Instance.BalanceConfigInstance.EnemyContactDamageScaling;
        }

        public override void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= ServerConfig.Instance.BalanceConfigInstance.EnemyContactDamageScaling;
        }

        public override void OnKill(NPC npc)
        {
            OnKillEvent?.Invoke(npc);
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
