using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.Abilities;
using System.Collections.Generic;
using System.Linq;
using TerraTyping.Abilities.Buffs;
using TerraTyping.DataTypes.Structs;
using System;

namespace TerraTyping
{
    public class NPCTyping : GlobalNPC
    {
        public NPC NPC { get; internal set; }

        NPCTypeInfo GetTypeInfo()
        {
            NPCTypeInfo typeInfo = new NPCTypeInfo(Element.none, Element.none, Element.none);
            if (DictionaryHelper.NPC(NPC).ContainsKey(NPC.type))
            {
                typeInfo = DictionaryHelper.NPC(NPC)[NPC.type];
            }
            return typeInfo;
        }
        DataTypes.ModifyTypeParameters GetTypeParameters()
        {
            NPCTypeInfo info = GetTypeInfo();
            return new DataTypes.ModifyTypeParameters(info, NPC);
        }

        public EntityTyping GetTypes()
        {
            ThreeType threeType = GetTypeInfo().ModifyType(GetTypeParameters());
            EntityTyping entityTyping = new EntityTyping(threeType.Primary, threeType.Secondary, threeType.Offensive);
            entityTyping = CheckBuffModifyType(entityTyping);
            return entityTyping;
        }
        public Element Primary => GetTypes().Primary;
        public Element Secondary => GetTypes().Secondary;
        public Element Offensive => GetTypes().offensiveElement;

        public EntityTyping CheckBuffModifyType(EntityTyping defTyping)
        {
            for (int i = 0; i < BuffLoader.BuffCount; i++)
            {
                if (NPC.HasBuff(i))
                {
                    ModBuff modBuff = ModContent.GetModBuff(i);
                    if (modBuff != null && modBuff is IUseModifiedType modifiedType)
                    {
                        return new EntityTyping(modifiedType.MyElement, Element.none, defTyping.offensiveElement);
                    }
                }
            }
            return defTyping;
        }

        public AbilityID CurrentAbilityID { get; private set; }
        public Ability GetAbility()
        {
            Ability ability = AbilityLookup.GetAbility(CurrentAbilityID);
            return ability;
        }

        public float DamageMultiplyByBuff { get; set; } = 1;
        public Element ModifyType { get; set; }

        public override bool InstancePerEntity => true;

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

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);
            this.NPC = npc;

            if (DictionaryHelper.NPC(npc).ContainsKey(npc.type))
            {
                NPCTypeInfo typeInfo = DictionaryHelper.NPC(npc)[npc.type];
                AbilityContainer abilityContainer = typeInfo.Container;
                float chance = ModContent.GetInstance<Config>().HiddenAbilityChancePercent;

                AbilityID ability = abilityContainer.PrimaryAbility;
                if (abilityContainer.SecondaryAbility != AbilityID.None)
                {
                    if (Main.rand.NextDouble() <= 0.5)
                    {
                        ability = abilityContainer.SecondaryAbility;
                    }
                }

                if (abilityContainer.HiddenAbility != AbilityID.None)
                {
                    if (Main.rand.NextDouble() <= (chance * 0.01))
                    {
                        ability = abilityContainer.HiddenAbility;
                    }
                }

                CurrentAbilityID = ability;

                //Main.NewText(
                //    $"NPC: {NPCID.GetUniqueKey(npc.type)}. " +
                //    $"Possible Abilities: {abilityContainer.PrimaryAbility}, {abilityContainer.SecondaryAbility}, ({abilityContainer.HiddenAbility}). " +
                //    $"Selected Ability: {AbilityID}. ");
            }
        }

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
            DecrementCombatCD();
            DamageMultiplyByBuff = 1;
            npc.defense = npc.defDefense;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            GetAbility().UpdateLifeRegen(new NPCWrapper(npc), TargetType.NPC);
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ItemWrapper itemWrapper = new ItemWrapper(item, player);
            NPCWrapper npcWrapper = new NPCWrapper(npc);
            Calc.OnHit(itemWrapper, npcWrapper);
            Calc.ModifyHitBy(itemWrapper, npcWrapper, ref damage, ref knockback, ref crit);
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            ItemWrapper itemWrapper = new ItemWrapper(item, player);
            NPCWrapper npcWrapper = new NPCWrapper(npc);
            Calc.OnHit(itemWrapper, npcWrapper);
            return Calc.CanBeHitBy(itemWrapper, npcWrapper, base.CanBeHitByItem(npc, player, item), false);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ProjectileWrapper projectileWrapper = new ProjectileWrapper(projectile);
            NPCWrapper npcWrapper = new NPCWrapper(npc);
            Calc.OnHit(projectileWrapper, npcWrapper);
            Calc.ModifyHitBy(projectileWrapper, npcWrapper, ref damage, ref knockback, ref crit);
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            ProjectileWrapper projectileWrapper = new ProjectileWrapper(projectile);
            NPCWrapper npcWrapper = new NPCWrapper(npc);
            Calc.OnHit(projectileWrapper, npcWrapper);
            return Calc.CanBeHitBy(projectileWrapper, npcWrapper, base.CanBeHitByProjectile(npc, projectile), false);
        }
    }
}
