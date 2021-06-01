using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.Abilities.Buffs;
using TerraTyping.Accessories.AbilityAccessories;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    public class PlayerTyping : ModPlayer
    {
        public TypeSet newTypeSet = new TypeSet(Element.normal, Element.none);

        public Ability GetAbility()
        {
            Ability ability = AbilityLookup.GetAbility(newTypeSet.GetAbility);
            return ability;
        }

        public Element ModifyType { get; set; }

        public ModItem AbilityAccessory { get; set; } = null;

        private const int CombatTextCooldownConst = 30;
        private const int CombatHealCooldownConst = 30;
        #region CombatTextCooldown
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

        public override void ResetEffects()
        {
            base.ResetEffects();
            DecrementCombatCD();
            AbilityAccessory = null;
        }

        public override void OnEnterWorld(Player player)
        {
            Main.NewText($"Welcome to TerraTyping version {ModContent.GetInstance<TerraTyping>().Version}. Use '/TerraTyping' for more info.");

            base.OnEnterWorld(player);
        }

        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();
        }

        public override void PostUpdateRunSpeeds()
        {
            if (player.HasBuff(ModContent.BuffType<MotorDrive>()))
            {
                MotorDrive.ModifySpeed(player);
            }
        }

        public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
        {
            base.ModifyWeaponDamage(item, ref add, ref mult, ref flat);
        }

        public override void UpdateBadLifeRegen()
        {
            GetAbility().UpdateLifeRegen(new PlayerWrapper(player), TargetType.Player);
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            base.UpdateEquips(ref wallSpeedBuff, ref tileSpeedBuff, ref tileRangeBuff);

            TypeSet typeSet = TypeSet.PlayerDefault;
            typeSet = ArmorType(typeSet); // low priority
            typeSet = AccessoryType(typeSet); // medium priority
            typeSet = BuffType(typeSet); // high priority
            newTypeSet = typeSet;
        }

        private TypeSet ArmorType(TypeSet typeSet)
        {
            TypeSet helmet = typeSet;
            TypeSet chestplate = typeSet;
            TypeSet leggings = typeSet;
            if (DictionaryHelper.Armor(player.armor[0]).TryGetValue(player.armor[0].type, out ArmorTypeInfo helmetType))
            {
                helmet = new TypeSet(helmetType);
            }
            if (DictionaryHelper.Armor(player.armor[1]).TryGetValue(player.armor[1].type, out ArmorTypeInfo chestplateType))
            {
                chestplate = new TypeSet(chestplateType);
            }
            if (DictionaryHelper.Armor(player.armor[2]).TryGetValue(player.armor[2].type, out ArmorTypeInfo leggingsType))
            {
                leggings = new TypeSet(leggingsType);
            }

            bool primaryMatch = helmet.Primary == chestplate.Primary && helmet.Primary == leggings.Primary;
            bool secondaryMatch = helmet.Secondary == chestplate.Secondary && helmet.Secondary == leggings.Secondary;
            bool typeMatch = helmet.GetAbility == chestplate.GetAbility && helmet.GetAbility == leggings.GetAbility;
            if (primaryMatch && secondaryMatch)
            {
                typeSet.Primary = helmet.Primary;
                typeSet.Secondary = helmet.Secondary;
                if (typeMatch)
                {
                    typeSet.GetAbility = helmet.GetAbility;
                }
            }

            return typeSet;
        }
        private TypeSet AccessoryType(TypeSet typeSet)
        {
            if (AbilityAccessory != null && !AbilityAccessory.item.IsAir)
            {
                if (AbilityAccessory is IAbilityAccessory abilityAccessory)
                {
                    typeSet.GetAbility = abilityAccessory.GivenAbility;
                }
            }

            return typeSet;
        }
        private TypeSet BuffType(TypeSet typeSet)
        {
            for (int i = 0; i < player.buffType.Length; i++)
            {
                ModBuff modBuff = ModContent.GetModBuff(player.buffType[i]);
                if (modBuff != null)
                {
                    if (modBuff is IBuffModifyType buffModify)
                    {
                        buffModify.ModifyType(new Abilities.Buffs.ModifyTypeParameters(typeSet, new PlayerWrapper(this)));
                    }
                    else if (modBuff is IUseModifiedType)
                    {
                        typeSet.Primary = ModifyType;
                        typeSet.Secondary = Element.none;
                    }
                }
            }
            return typeSet;
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            float f = default;
            NPCWrapper npcWrapper = new NPCWrapper(npc);
            PlayerWrapper playerWrapper = new PlayerWrapper(player);
            Calc.OnHit(npcWrapper, playerWrapper);
            Calc.ModifyHitBy(npcWrapper, playerWrapper, ref damage, ref f, ref crit);
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            NPCWrapper npcWrapper = new NPCWrapper(npc);
            PlayerWrapper playerWrapper = new PlayerWrapper(player);
            Calc.OnHit(npcWrapper, playerWrapper);
            return Calc.CanBeHitBy(npcWrapper, playerWrapper, base.CanBeHitByNPC(npc, ref cooldownSlot), false);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            float f = default;
            ProjectileWrapper projWrapper = new ProjectileWrapper(proj);
            PlayerWrapper playerWrapper = new PlayerWrapper(player);
            Calc.OnHit(projWrapper, playerWrapper);
            Calc.ModifyHitBy(projWrapper, playerWrapper, ref damage, ref f, ref crit);
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            ProjectileWrapper projWrapper = new ProjectileWrapper(proj);
            PlayerWrapper playerWrapper = new PlayerWrapper(player);
            Calc.OnHit(projWrapper, playerWrapper);
            return Calc.CanBeHitBy(projWrapper, playerWrapper, base.CanBeHitByProjectile(proj), false);
        }
    }
}
