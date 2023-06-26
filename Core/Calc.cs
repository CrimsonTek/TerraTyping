using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common;
using TerraTyping.Common.Configs;
using TerraTyping.Core.Abilities;
using TerraTyping.DataTypes;
using static TerraTyping.Core.Abilities.AbilityLookup;

namespace TerraTyping.Core
{
    public class Calc
    {
        public static DamageReturn Damage<A, D>(A attacker, D target)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed
            where D : Wrapper, ITarget, IDefensiveElements, IAbility
        {
            if (attacker is null)
            {
                throw new ArgumentNullException(nameof(attacker));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            float knockback = 1;

            AbilityFlyweight attackerAbility = GetAbility(attacker);
            AbilityFlyweight defenderAbility = GetAbility(attackerAbility.ModifyOpponentsAbility(target.GetAbility));

            ElementArray defensiveArr = target.DefensiveElements;
            ElementArray offensiveArr = attackerAbility.ModifyAttackType(attacker.OffensiveElements);

            //float powerup = attackerAbility.PowerupType(new PowerupTypeParameters(offensive, attacker, target)).powerupMultiplier;

            float damage = 1;

            bool conditionWasSatisfied = false;
            for (int i = 0; i < defensiveArr.Length; i++)
            {
                for (int j = 0; j < offensiveArr.Length; j++)
                {
                    bool condition = offensiveArr[j] == Element.water && defensiveArr[i] == Element.water;
                    conditionWasSatisfied |= condition;

                    float baseEffectiveness = Table.EffectivenessScaled(offensiveArr[j], defensiveArr[i]);
                    attacker.ModifyEffectiveness(ref baseEffectiveness, offensiveArr[j], defensiveArr[i]);
                    float postDefenderModifierEffectiveness = defenderAbility.ModifyEffectivenessIncoming(new ModifyEffectivenessIncomingParameters(offensiveArr[j], defensiveArr[i], baseEffectiveness));
                    float postAttackerModifierEffectiveness = attackerAbility.ModifyEffectivenessOutgoing(new ModifyEffectivenessOutgoingParameters(offensiveArr[j], defensiveArr[i], postDefenderModifierEffectiveness));
                    damage *= postAttackerModifierEffectiveness;
                }
            }

            ModifyDamageReturn modifyDamageReturn = defenderAbility.ModifyDamageIncoming(new ModifyDamageParameters(offensiveArr, damage, target, knockback, attacker, attacker));
            damage = modifyDamageReturn.newDamage;
            knockback = modifyDamageReturn.newKnockback;
            return new DamageReturn(damage, modifyDamageReturn.heal, knockback);
        }

        public static float Stab<A, D>(A offensiveType, D user)
            where A : Wrapper, IOffensiveType
            where D : ITarget, IDefensiveElements, IAbility
        {
            Ability abilityID = user.GetAbility;
            AbilityFlyweight ability = GetAbility(abilityID);

            int stabCount = 0;
            bool countNormally = true;

            if (offensiveType is WeaponWrapper weaponWrapper)
            {
                ForceStabWithItemReturn forceStabWithItemReturn = ability.ForceStabWithItem(new ForceStabWithItemParameters(weaponWrapper));

                if (forceStabWithItemReturn.ReplaceStab)
                {
                    countNormally = false;
                    stabCount = forceStabWithItemReturn.ReplaceCount;
                }
                else
                {
                    countNormally = true;
                    stabCount = forceStabWithItemReturn.AddCount;
                }
            }

            if (countNormally)
            {
                ElementArray defensiveElements = user.DefensiveElements;
                ElementArray offensiveElements = offensiveType.OffensiveElements;

                for (int i = 0; i < defensiveElements.Length; i++)
                {
                    Element element = defensiveElements[i];
                    if (offensiveElements.HasElement(element))
                    {
                        stabCount++;
                    }
                }
            }

            if (stabCount > 1)
            {
                ServerConfig config = ModContent.GetInstance<ServerConfig>();
                float stabAmount = config.STAB;
                float dimishingReturnScalar = config.STABDiminishingReturnScalar;
                if (dimishingReturnScalar == 0)
                {
                    return stabAmount;
                }
                else if (dimishingReturnScalar == 1)
                {
                    return stabCount * (stabAmount - 1) + 1;
                }
                else
                {
                    return (float)(Math.Pow(stabCount, dimishingReturnScalar) * (stabAmount - 1)) + 1;
                }
            }
            else if (stabCount == 1)
            {
                return ModContent.GetInstance<ServerConfig>().STAB;
            }
            else
            {
                return 1;
            }
        }

        public static void NPCModifyHitBy<A, D>(A attacker, D target, ref NPC.HitModifiers modifiers)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed
            where D : Wrapper, ITarget, IDefensiveElements, IAbility
        {
            DamageReturn damageReturn = Damage(attacker, target);
            float damageMultiplier = damageReturn.damage;
            if (attacker is NPCWrapper)
            {
                damageMultiplier *= attacker.DamageMultiplication();
            }
            modifiers.FinalDamage *= damageMultiplier;
            modifiers.Knockback *= damageReturn.knockback;

            DoCombatText(damageReturn.damage, target.GetRect());
        }

        public static void PlayerModifyHitBy<A, D>(A attacker, D target, ref Player.HurtModifiers modifiers)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed
            where D : Wrapper, ITarget, IDefensiveElements, IAbility
        {
            DamageReturn damageReturn = Damage(attacker, target);
            float damageMultiplier = damageReturn.damage;
            if (attacker is NPCWrapper)
            {
                damageMultiplier *= attacker.DamageMultiplication();
            }
            modifiers.FinalDamage *= damageMultiplier;
            modifiers.Knockback *= damageReturn.knockback;

            DoCombatText(damageReturn.damage, target.GetRect());
        }

        public static bool CanBeHitBy<A, D>(A attack, D defense)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed
            where D : Wrapper, ITarget, IDefensiveElements, IAbility
        {
            return Damage(attack, defense).damage > 0;
        }

        public static void OnHit<A, D>(A attacker, D defender, NPC.HitInfo hit)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
            where D : Wrapper, ITarget, IDefensiveElements, IAbility, ITeam
        {
            OnHit(attacker, defender);
        }

        public static void OnHit<A, D>(A attacker, D defender, Player.HurtInfo hit)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
            where D : Wrapper, ITarget, IDefensiveElements, IAbility, ITeam
        {
            OnHit(attacker, defender);
        }

        public static void OnHit<A, D>(A attacker, D defender)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
            where D : Wrapper, ITarget, IDefensiveElements, IAbility, ITeam
        {
            if (attacker.Hitbox.Intersects(defender.GetRect()) && defender.Active && attacker.GetTeam() != defender.GetTeam())
            {
                AbilityFlyweight attackerAbility = GetAbility(attacker);
                AbilityFlyweight defenderAbility = GetAbility(attackerAbility.ModifyOpponentsAbility(defender.GetAbility));

                DamageReturn damageReturn = Damage(attacker, defender);
                defenderAbility.BuffOnHit(new BuffOnHitParameters(attacker.OffensiveElements, defender, attacker));

                if (defender.GetCombatTextCooldown() <= 0)
                {
                    Message messageOnHit = defenderAbility.MessageOnHit(new MessageOnHitParameters(attacker.OffensiveElements, defender, attacker, attacker)).message;
                    Message messageHitEnemy = GetAbility(attacker.GetAbility).MessageHitEnemy(
                        new MessageHitEnemyParameters(attacker.OffensiveElements, defender)).message;
                    void ReadMessage(Message message)
                    {
                        if (message.hasMessage && !string.IsNullOrEmpty(message.text))
                        {
                            Color color = message.setColor ? message.textColor : new Color(255, 255, 255);
                            int ct = CombatText.NewText(defender.GetRect(), color, message.text);
                            if (ct != Main.maxCombatText)
                            {
                                ModContent.GetInstance<MySystem>().TrackCombatText(ct, message.elements);
                            }
                            defender.UseCombatTextCooldown();
                        }
                    }
                    ReadMessage(messageOnHit);
                    ReadMessage(messageHitEnemy);
                }

                if (defender.GetCombatHealCooldown() <= 0)
                {
                    if (damageReturn.heal)
                    {
                        float healPercent;
                        switch (defender.EntityType)
                        {
                            case EntityType.Player:
                                healPercent = 0.10f;
                                break;
                            case EntityType.NPC:
                                if (defender.Boss)
                                {
                                    healPercent = 0.04f;
                                }
                                else
                                {
                                    healPercent = 0.125f;
                                }
                                break;
                            default:
                                healPercent = 0;
                                break;
                        }
                        int healAmount = Math.Max((int)(defender.LifeMax * healPercent), 1);
                        if (defender.Life < defender.LifeMax)
                        {
                            defender.Heal(healAmount);
                        }
                    }
                    defender.UseCombatHealCooldown();
                }

                if (attacker is ProjectileWrapper projectileWrapper && defender is IAttractProjectileTarget attractProjectileTarget)
                {
                    if (defenderAbility.AttractProjectile(new AttractProjectileParameters(projectileWrapper, defender)))
                    {
                        projectileWrapper.Kill();
                    }
                }
            }
        }

        private static void DoCombatText(float baseDamageMultiplier, Rectangle location)
        {
            Color color = baseDamageMultiplier switch
            {
                0 => new Color(r: 0.2f, g: 0.2f, b: 0.2f),
                > 1 => new Color(r: -(baseDamageMultiplier / 3) + 1, g: 1, b: -(baseDamageMultiplier / 3) + 1),
                < 1 => new Color(r: baseDamageMultiplier * 4 / 5 + 0.2f, g: baseDamageMultiplier / 5 + 0.2f, b: 0),
                _ => new Color(r: 1f, g: 1f, b: 1f),
            };

            CombatText.NewText(location, color, $"{baseDamageMultiplier:0.##}x!", false, true);
        }

        //public static float Damage(DamageInterface attacker, TargetInterface target)
        //{
        //    Element defensePrimary = ElementHelper.Primary(target);
        //    Element defenseSecondary = ElementHelper.Secondary(target);
        //    AbilityID ability = ElementHelper.GetAbility(target);
        //    Element offensive = ElementHelper.OldOffensive(attacker);

        //    float multiplier1 = Table.Eff((int)offensive, (int)defensePrimary);
        //    float multiplier2 = Table.Eff((int)offensive, (int)defenseSecondary);
        //    float baseDamageMultiplier = multiplier1 * multiplier2;
        //    baseDamageMultiplier = AbilityLookup.GetAbility(ability).ModifyDamage(offensive, baseDamageMultiplier, target);

        //    return baseDamageMultiplier;
        //}

        //public static float Damage<a, d>(a attacker, d defender)
        //{
        //    Element defensePrimary = ElementHelper.Primary(defender);
        //    Element defenseSecondary = ElementHelper.Secondary(defender);
        //    AbilityID ability = ElementHelper.GetAbility(defender);
        //    Element offensive = ElementHelper.OldOffensive(attacker);

        //    float multiplier1 = Table.Eff((int)offensive, (int)defensePrimary);
        //    float multiplier2 = Table.Eff((int)offensive, (int)defenseSecondary);
        //    float baseDamageMultiplier = multiplier1 * multiplier2;
        //    baseDamageMultiplier = AbilityLookup.GetAbility(ability).ModifyDamage(offensive, baseDamageMultiplier, new TargetInterface(defender));

        //    return baseDamageMultiplier;
        //}

        //public static float Damage(object attacker, object defender)
        //{
        //    Element defensePrimary = ElementHelper.Primary(defender);
        //    Element defenseSecondary = ElementHelper.Secondary(defender);
        //    AbilityID ability = ElementHelper.GetAbility(defender);
        //    Element offensive = ElementHelper.OldOffensive(attacker);

        //    float multiplier1 = Table.Eff((int)offensive, (int)defensePrimary);
        //    float multiplier2 = Table.Eff((int)offensive, (int)defenseSecondary);
        //    float baseDamageMultiplier = multiplier1 * multiplier2;
        //    baseDamageMultiplier = AbilityLookup.GetAbility(ability).ModifyDamage(offensive, baseDamageMultiplier, new TargetInterface(defender));

        //    return baseDamageMultiplier;
        //}

        //[Obsolete]
        //public static int OldBuff(object buff, object defense, int lifeRegen)
        //{
        //    int defensePrimary = (int)ElementHelper.Primary(defense);
        //    int defenseSecondary = (int)ElementHelper.Secondary(defense);
        //    int defenseTertiary = (int)ElementHelper.OldTertiary(defense);
        //    int attackQuatrinary = (int)ElementHelper.OldOffensive(buff);

        //    float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
        //    float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
        //    float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

        //    float mult = -lifeRegen;
        //    mult += (lifeRegen * multiplier1 * multiplier2 * multiplier3);
        //    return (int)mult;
        //}

        //[Obsolete]
        //public static int OldLifeRegen(object buff, object defense, int lifeRegen)
        //{
        //    int defensePrimary = (int)ElementHelper.Primary(defense);
        //    int defenseSecondary = (int)ElementHelper.Secondary(defense);
        //    int defenseTertiary = (int)ElementHelper.OldTertiary(defense);
        //    int attackQuatrinary = (int)ElementHelper.OldOffensive(buff);

        //    float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
        //    float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
        //    float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

        //    return (int)(lifeRegen * multiplier1 * multiplier2 * multiplier3);
        //}

        //[Obsolete]
        //public static int OldBadRegen(object buff, object defense, int buffBadRegen)
        //{
        //    int defensePrimary = (int)ElementHelper.Primary(defense);
        //    int defenseSecondary = (int)ElementHelper.Secondary(defense);
        //    int defenseTertiary = (int)ElementHelper.OldTertiary(defense);
        //    int attackQuatrinary = (int)ElementHelper.OldOffensive(buff);

        //    float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
        //    float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
        //    float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

        //    float multiplier = multiplier1 * multiplier2 * multiplier3;
        //    int lifeRegen = -buffBadRegen;
        //    lifeRegen = (int)(buffBadRegen * multiplier);
        //    return lifeRegen;
        //}
    }

    public struct DamageReturn
    {
        public float damage;
        public float knockback;
        public bool heal;

        public DamageReturn(float damage, bool heal, float knockback)
        {
            this.damage = damage;
            this.heal = heal;
            this.knockback = knockback;
        }
    }
}
