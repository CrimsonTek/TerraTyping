using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.Abilities.Buffs;
using TerraTyping.Accessories.AbilityAccessories;
using TerraTyping.Common.Configs;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;
using TerraTyping.TypeLoaders;

namespace TerraTyping
{
    public class PlayerTyping : ModPlayer
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

        public ModItem AbilityAccessory { get; set; } = null;

        /// <summary>
        /// Add (your value - 1) to this.
        /// </summary>
        public float _allDamageModifiedByAbilities;
        /// <summary>
        /// Each type's damage multiplier as modified by abilities.
        /// </summary>
        public float[] _damageModifiedByAbilities;
        public List<Boost> boostsToAllDamageByAbilities = new List<Boost>();
        public List<Boost>[] boostsToEachTypeByAbilities;

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

        public PlayerTyping()
        {
            boostsToAllDamageByAbilities = new List<Boost>();

            int elementCount = ElementHelper.ElementCount(includeNone: false);
            boostsToEachTypeByAbilities = new List<Boost>[elementCount];
            for (int i = 0; i < elementCount; i++)
            {
                boostsToEachTypeByAbilities[i] = new List<Boost>();
            }


            _damageModifiedByAbilities = new float[elementCount];
        }

        public Ability GetAbility()
        {
            return AbilityLookup.GetAbility(AbilityID);
        }

        public override void ResetEffects()
        {
            DecrementCombatCD();
            AbilityAccessory = null;

            _allDamageModifiedByAbilities = 1;
            for (int i = 0; i < _damageModifiedByAbilities.Length; i++)
            {
                _damageModifiedByAbilities[i] = 1;
            }

            UseModifiedAbility = false;
            UseModifiedElements = false;
        }

        public override void OnEnterWorld(Player player)
        {
            if (ModContent.GetInstance<ClientConfig>()?.WelcomeMessage != false)
            {
                Main.NewText($"Welcome to TerraTyping version {TerraTyping.Instance.Version}. Use '/TerraTyping' for more info.");
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            if (Player.HasBuff(ModContent.BuffType<MotorDrive>()))
            {
                MotorDrive.ModifySpeed(Player);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            GetAbility().UpdateLifeRegen(PlayerWrapper.GetWrapper(Player), TargetType.Player);
        }

        public override void UpdateEquips()
        {
            baseElements = ElementArray.Default;
            baseAbility = AbilityID.None;

            // todo: avoid unnessesary setting of elements
            ArmorType(); // low priority
            AccessoryType(); // medium priority
        }

        private void ArmorType()
        {
            Item[] armor = Player.armor;
            if (IsWizardSet(armor[0].type, armor[1].type))
            {
                baseElements = ElementArray.Get(Element.psychic);
                baseAbility = AbilityID.None;
                return;
            }

            ElementArray helmElements = ArmorTypeLoader.GetElements(armor[0]);
            if (helmElements.ExactMatch(ArmorTypeLoader.GetElements(armor[1]))
                && helmElements.ExactMatch(ArmorTypeLoader.GetElements(armor[2])))
            {
                baseElements = helmElements;

                AbilityID helmAbility = ArmorTypeLoader.GetAbility(armor[0]);
                bool abilityMatch = helmAbility == ArmorTypeLoader.GetAbility(armor[1])
                    && helmAbility == ArmorTypeLoader.GetAbility(armor[2]);
                if (abilityMatch)
                {
                    baseAbility = helmAbility;
                }
            }
        }
        private void AccessoryType()
        {
            if (AbilityAccessory != null && !AbilityAccessory.Item.IsAir)
            {
                if (AbilityAccessory is IAbilityAccessory abilityAccessory)
                {
                    baseAbility = abilityAccessory.GivenAbility;
                }
            }
        }
        private static bool IsWizardSet(int helmType, int chestType)
        {
            if (helmType is ItemID.WizardHat or ItemID.MagicHat)
            {
                if (chestType is >= ItemID.TopazRobe and <= ItemID.DiamondRobe or ItemID.AmberRobe or ItemID.GypsyRobe)
                {
                    return true;
                }
            }

            return false;
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            ModifyHit(NPCWrapper.GetWrapper(npc), PlayerWrapper.GetWrapper(Player), ref damage, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            ModifyHit(ProjectileWrapper.GetWrapper(proj), PlayerWrapper.GetWrapper(Player), ref damage, ref crit);
        }

        private void ModifyHit<A, D>(A attacker, D defender, ref int damage, ref bool crit)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed, IHitbox, ITeam
            where D : Wrapper, ITarget, IDefensiveElements, IAbility, ITeam
        {
            float kb = default;
            double armorFactor = Main.masterMode ? 1 : Main.expertMode ? 0.75 : 0.5;
            damage -= (int)(Player.statDefense * armorFactor);
            Calc.OnHit(attacker, defender);
            Calc.ModifyHitBy(attacker, defender, ref damage, ref kb, ref crit);
            damage += (int)(Player.statDefense * armorFactor);
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return CanBeHit(NPCWrapper.GetWrapper(npc), PlayerWrapper.GetWrapper(Player));
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return CanBeHit(ProjectileWrapper.GetWrapper(proj), PlayerWrapper.GetWrapper(Player));
        }

        private bool CanBeHit<A, D>(A attacker, D defender)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed, IHitbox, ITeam
            where D : Wrapper, ITarget, IDefensiveElements, IAbility, ITeam
        {
            Calc.OnHit(attacker, defender);
            return Calc.CanBeHitBy(attacker, defender);
        }
    }
}
