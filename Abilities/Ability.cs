using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.Abilities.Buffs;
using TerraTyping.DataTypes;
using static TerraTyping.Abilities.AbilityLookup;

namespace TerraTyping.Abilities
{
    public class Ability
    {
        public readonly AbilityID id;

        public ModifyDamage modifyDamageIncoming;
        public ForceStabWithItem forceStabWithItem;
        public ModifyAttackType modifyAttackType;
        public ModifyEffectivenessOutgoing modifyEffectivenessOutgoing;
        public ModifyEffectivenessIncoming modifyEffectivenessIncoming;
        public ModifyStabAmount modifyStabAmount;
        public PowerupType powerupType;
        public UpdateLifeRegen updateLifeRegen;
        public BuffOnHit buffOnHit;
        public MessageOnHit messageOnHit;
        public MessageHitEnemy messageHitEnemy;
        public AttractProjectile attractProjectile;
        public ModifyOpponentsAbility modifyOpponentsAbility;

        public Ability(AbilityID abilityID)
        {
            id = abilityID;
        }

        public ModifyDamageReturn ModifyDamageIncoming(ModifyDamageParameters parameters) =>
            modifyDamageIncoming?.Invoke(parameters) ?? new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
        
        public ForceStabWithItemReturn ForceStabWithItem(ForceStabWithItemParameters parameters) =>
            forceStabWithItem?.Invoke(parameters) ?? ForceStabWithItemReturn.DoNothing();
        
        public ElementArray ModifyAttackType(ElementArray @default) =>
            modifyAttackType?.Invoke(@default) ?? @default;
        
        public float ModifyEffectivenessOutgoing(ModifyEffectivenessOutgoingParameters parameters) =>
            modifyEffectivenessOutgoing?.Invoke(parameters) ?? parameters.normalEffectiveness;
        
        public float ModifyEffectivenessIncoming(ModifyEffectivenessIncomingParameters parameters) =>
            modifyEffectivenessIncoming?.Invoke(parameters) ?? parameters.normalEffectiveness;
        
        public float ModifyStabAmount(float defaultStab, bool stab) =>
            modifyStabAmount?.Invoke(defaultStab, stab) ?? defaultStab;
        
        public PowerupTypeReturn PowerupType(PowerupTypeParameters parameters) =>
            powerupType?.Invoke(parameters) ?? new PowerupTypeReturn(1);
        
        public void UpdateLifeRegen(ITarget target, TargetType targetType) =>
            updateLifeRegen?.Invoke(target, targetType);
        
        public void BuffOnHit(BuffOnHitParameters parameters) =>
            buffOnHit?.Invoke(parameters);
        
        public MessageOnHitReturn MessageOnHit(MessageOnHitParameters parameters) =>
            messageOnHit?.Invoke(parameters) ?? MessageOnHitReturn.None;
        
        public MessageHitEnemyReturn MessageHitEnemy(MessageHitEnemyParameters parameters) =>
            messageHitEnemy?.Invoke(parameters) ?? new MessageHitEnemyReturn(Message.None);
        
        public bool AttractProjectile(AttractProjectileParameters parameters) =>
            attractProjectile?.Invoke(parameters) ?? false;

        public AbilityID ModifyOpponentsAbility(AbilityID abilityID) =>
            modifyOpponentsAbility?.Invoke(abilityID) ?? abilityID;

        public void ModifyOpponentsAbility(ref AbilityID abilityID)
        {
            if (modifyOpponentsAbility is not null)
            {
                abilityID = modifyOpponentsAbility.Invoke(abilityID);
            }
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}
