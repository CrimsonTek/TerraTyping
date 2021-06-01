using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.DataTypes;
using static TerraTyping.Abilities.AbilityLookup;

namespace TerraTyping
{
    public class Calc
    {
        public static DamageReturn Damage<A, D>(A attacker, D target)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed
            where D : Wrapper, ITarget, IPrimaryType, ISecondaryType, IAbility
        {
            #region Testing

            //bool testing = false;
            //if (target is NPCWrapper npc && attacker is ProjectileWrapper proj)
            //{
            //    switch (npc.GetNPC().type)
            //    {
            //        case NPCID.BlueJellyfish:
            //        case NPCID.GreenJellyfish:
            //        case NPCID.PinkJellyfish:
            //        case NPCID.BloodJelly:
            //        case NPCID.FungoFish:
            //            if (proj.Projectile.type == ProjectileID.WaterBolt)
            //            {
            //                testing = true;
            //            }
            //            break;
            //    }
            //}

            #endregion

            float knockback = 1;

            Ability attackerAbility = GetAbility(attacker);
            Ability defenderAbility = GetAbility(attackerAbility.ModifyOpponentsAbility(target.GetAbility));

            Element defensePrimary = target.Primary;
            Element defenseSecondary = target.Secondary;

            Element offensive = attacker.Offensive;
            offensive = attackerAbility.ModifyAttackType(offensive);

            float multiplier1 = Table.Eff((int)offensive, (int)defensePrimary);
            float multiplier2 = Table.Eff((int)offensive, (int)defenseSecondary);
            multiplier1 = attackerAbility.ModifyEffectivenessOutgoing(new ModifyEffectivenessOutgoingParameters(offensive, defensePrimary, multiplier1));
            //float powerup = attackerAbility.PowerupType(new PowerupTypeParameters(offensive, attacker, target)).powerupMultiplier;

            float damage = multiplier1 * multiplier2;
            ModifyDamageReturn modifyDamageReturn = defenderAbility.ModifyDamage(
                new ModifyDamageParameters(offensive, damage, target, knockback, attacker));
            damage = modifyDamageReturn.newDamage;
            knockback = modifyDamageReturn.newKnockback;

            return new DamageReturn(damage, modifyDamageReturn.heal, knockback);
        }

        public static float Stab<A, D>(A offensiveType, D user)
            where A : IOffensiveType
            where D : ITarget, IPrimaryType, ISecondaryType, IAbility
        {
            Element defensePrimary = user.Primary;
            Element defenseSecondary = user.Secondary;
            AbilityID abilityID = user.GetAbility;
            Ability ability = GetAbility(abilityID);

            Element offensive = offensiveType.Offensive;

            float multiplier = 1.0f;
            bool stab = false;

            if (offensive != Element.none)
            {
                if (defensePrimary == offensive)
                    stab = true;
                if (defenseSecondary == offensive)
                    stab = true;
            }

            if (ability.ForceStab(offensive))
            {
                stab = true;
            }

            if (stab)
            {
                multiplier *= ModContent.GetInstance<Config>().STAB;
            }

            return multiplier;
        }

        public static void ModifyHitBy<A, D>(A attacker, D target, ref int damage, ref float knockback, ref bool crit)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed
            where D : Wrapper, ITarget, IPrimaryType, ISecondaryType, IAbility
        {
            #region Testing

            //bool testing = false;
            //if (defenseWrapper is NPCWrapper npc && attackWrapper is ProjectileWrapper proj)
            //{
            //    switch (npc.GetNPC().type)
            //    {
            //        case NPCID.BlueJellyfish:
            //        case NPCID.GreenJellyfish:
            //        case NPCID.PinkJellyfish:
            //        case NPCID.BloodJelly:
            //        case NPCID.FungoFish:
            //            if (proj.Projectile.type == ProjectileID.WaterBolt)
            //            {
            //                testing = true;
            //            }
            //            break;
            //    }
            //}

            #endregion

            DamageReturn damageReturn = Damage(attacker, target);

            damage = (int)(damage * damageReturn.damage);
            if (attacker is NPCWrapper)
            {
                damage = (int)(damage * attacker.DamageMultiplication());
            }
            knockback = (knockback * damageReturn.knockback);

            float dmg = damageReturn.damage;
            string text = ((float)(int)(dmg * 100) / 100).ToString() + "x!";

            Color color = new Color(new Vector3(1, 1, 1));
            if (dmg != 1)
            {
                if (dmg == 0)
                    color = new Color(new Vector3(0.2f, 0.2f, 0.2f));

                else if (dmg > 1)
                    color = new Color(new Vector3(1 - (dmg / 3), 1, 1 - (dmg / 3)));

                else if (dmg < 1)
                    color = new Color(new Vector3((dmg * 4) / 5 + 0.2f, (dmg) / 5 + 0.2f, 0));
            }

            CombatText.NewText(target.GetRect(), color, text, false, true);
        }

        public static B CanBeHitBy<A, D, B>(A attack, D defense, B @base, B @false)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed
            where D : Wrapper, ITarget, IPrimaryType, ISecondaryType, IAbility
        {
            DamageReturn damageReturn = Damage(attack, defense);

            if (damageReturn.damage != 0)
            {
                return @base;
            }
            else
            {
                return @false;
            }
        }

        public static void OnHit<A, D>(A attacker, D defender)
            where A : Wrapper, IOffensiveType, IAbility, IDamageClass, IHitbox, ITeam, IStatsBuffed
            where D : Wrapper, ITarget, IPrimaryType, ISecondaryType, IAbility, ITeam
        {
            if (attacker.Hitbox.Intersects(defender.GetRect()) && defender.Active && (attacker.GetTeam() != defender.GetTeam()))
            {
                Ability attackerAbility = GetAbility(attacker);
                Ability defenderAbility = GetAbility(attackerAbility.ModifyOpponentsAbility(defender.GetAbility));

                DamageReturn damageReturn = Damage(attacker, defender);
                defenderAbility.BuffOnHit(new BuffOnHitParameters(attacker.Offensive, defender, attacker));

                if (defender.GetCombatTextCooldown() <= 0)
                {
                    Message messageOnHit = defenderAbility.MessageOnHit(new MessageOnHitParameters(attacker.Offensive, defender, attacker, attacker)).message;
                    Message messageHitEnemy = GetAbility(attacker.GetAbility).MessageHitEnemy(new MessageHitEnemyParameters(attacker.Offensive, defender, defender, defender)).message;
                    void ReadMessage(Message message)
                    {
                        if (message.hasMessage && !string.IsNullOrEmpty(message.text))
                        {
                            Color color = message.setColor ? message.textColor : new Color(255, 255, 255);
                            CombatText.NewText(defender.GetRect(), color, message.text);
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

        //public static float Damage(DamageInterface attacker, TargetInterface target)
        //{
        //    Element defensePrimary = ElementHelper.Primary(target);
        //    Element defenseSecondary = ElementHelper.Secondary(target);
        //    AbilityID ability = ElementHelper.GetAbility(target);
        //    Element offensive = ElementHelper.OldOffensive(attacker);

        //    float multiplier1 = Table.Eff((int)offensive, (int)defensePrimary);
        //    float multiplier2 = Table.Eff((int)offensive, (int)defenseSecondary);
        //    float damage = multiplier1 * multiplier2;
        //    damage = AbilityLookup.GetAbility(ability).ModifyDamage(offensive, damage, target);

        //    return damage;
        //}

        //public static float Damage<a, d>(a attacker, d defender)
        //{
        //    Element defensePrimary = ElementHelper.Primary(defender);
        //    Element defenseSecondary = ElementHelper.Secondary(defender);
        //    AbilityID ability = ElementHelper.GetAbility(defender);
        //    Element offensive = ElementHelper.OldOffensive(attacker);

        //    float multiplier1 = Table.Eff((int)offensive, (int)defensePrimary);
        //    float multiplier2 = Table.Eff((int)offensive, (int)defenseSecondary);
        //    float damage = multiplier1 * multiplier2;
        //    damage = AbilityLookup.GetAbility(ability).ModifyDamage(offensive, damage, new TargetInterface(defender));

        //    return damage;
        //}

        //public static float Damage(object attacker, object defender)
        //{
        //    Element defensePrimary = ElementHelper.Primary(defender);
        //    Element defenseSecondary = ElementHelper.Secondary(defender);
        //    AbilityID ability = ElementHelper.GetAbility(defender);
        //    Element offensive = ElementHelper.OldOffensive(attacker);

        //    float multiplier1 = Table.Eff((int)offensive, (int)defensePrimary);
        //    float multiplier2 = Table.Eff((int)offensive, (int)defenseSecondary);
        //    float damage = multiplier1 * multiplier2;
        //    damage = AbilityLookup.GetAbility(ability).ModifyDamage(offensive, damage, new TargetInterface(defender));

        //    return damage;
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
