using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Content.Accessories.AbilityAccessories;
using TerraTyping.Core;
using TerraTyping.Core.Abilities;
using TerraTyping.Core.Abilities.Buffs;
using TerraTyping.DataTypes;
using TerraTyping.DataTypes.Wrappers;
using TerraTyping.Helpers;
using TerraTyping.TypeLoaders;

namespace TerraTyping.Common.TModLoaderGlobals
{
    public class PlayerTyping : ModPlayer
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

        public ModItem AbilityAccessory { get; set; } = null;

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
        }

        public AbilityFlyweight GetAbility()
        {
            return AbilityLookup.GetAbility(AbilityID);
        }

        public override void ResetEffects()
        {
            DecrementCombatCD();
            AbilityAccessory = null;

            UseModifiedAbility = false;
            UseModifiedElements = false;
        }

        public override void OnEnterWorld()
        {
            if (ClientConfig.Instance.WelcomeMessage)
            {
                LocalizedText localizedText = Language.GetOrRegister("Mods.TerraTyping.Flavor.WelcomeMessage");
                Main.NewText(localizedText.Format(TerraTyping.Instance.Version));
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
            baseAbility = Ability.None;

            ArmorType(); // low priority
            AccessoryType(); // high priority
        }

        private void ArmorType()
        {
            Item[] armor = Player.armor;
            if (IsWizardSet(armor[0].type, armor[1].type))
            {
                baseElements = ElementArray.Get(Element.psychic);
                baseAbility = Ability.None;
                return;
            }

            ElementArray helmElements = ArmorTypeLoader.GetElements(armor[0]);
            if (helmElements.ExactMatch(ArmorTypeLoader.GetElements(armor[1]))
                && helmElements.ExactMatch(ArmorTypeLoader.GetElements(armor[2])))
            {
                baseElements = helmElements;

                Ability helmAbility = ArmorTypeLoader.GetAbility(armor[0]);
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

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            Calc.PlayerModifyHitBy(NPCWrapper.GetWrapper(npc), PlayerWrapper.GetWrapper(Player), ref modifiers);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            Calc.PlayerModifyHitBy(ProjectileWrapper.GetWrapper(proj), PlayerWrapper.GetWrapper(Player), ref modifiers);
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            int sourceOtherIndex = modifiers.DamageSource.SourceOtherIndex;
            Element element = sourceOtherIndex switch
            {
                OtherDamageID.FallDamage => Element.ground,
                OtherDamageID.Drowned => Element.water,
                OtherDamageID.Lava => Element.fire,
                OtherDamageID.Default => Element.none,
                OtherDamageID.DemonAlterHurt => Element.dark,
                OtherDamageID.FallDamageWhilePetrified => Element.ground,
                OtherDamageID.CompanionCubeStabbed => Element.dark,
                OtherDamageID.Suffocated => Element.ground,
                OtherDamageID.Burned => Element.fire,
                OtherDamageID.Poisoned => Element.poison,
                OtherDamageID.Electrocuted => Element.electric,
                OtherDamageID.TriedToEscape => Element.none,
                OtherDamageID.WasLicked => Element.blood,
                OtherDamageID.Teleport_1 or OtherDamageID.Teleport_2_Female or OtherDamageID.Teleport_2_Male => Element.none,
                OtherDamageID.Inferno => Element.fire,
                OtherDamageID.DiedInTheDark => Element.dark,
                OtherDamageID.Starved => Element.normal,
                _ => Element.none,
            };
            if (element == Element.none)
            {
                return;
            }

            Calc.OtherModifyHitBy(new OtherWrapper(sourceOtherIndex, element), PlayerWrapper.GetWrapper(Player), ref modifiers);
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return Calc.CanBeHitBy(NPCWrapper.GetWrapper(npc), PlayerWrapper.GetWrapper(Player));
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return Calc.CanBeHitBy(ProjectileWrapper.GetWrapper(proj), PlayerWrapper.GetWrapper(Player));
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            Calc.OnHit(NPCWrapper.GetWrapper(npc), PlayerWrapper.GetWrapper(Player), hurtInfo);
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            Calc.OnHit(ProjectileWrapper.GetWrapper(proj), PlayerWrapper.GetWrapper(Player), hurtInfo);
        }
    }
}
