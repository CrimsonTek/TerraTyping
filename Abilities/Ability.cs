using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.DataTypes;
using static TerraTyping.Abilities.AbilityLookup;

namespace TerraTyping.Abilities
{
    public class Ability
    {
        public AbilityID ID { get; }

        public ModifyDamage ModifyDamage { get; set; } = ModifyDamageDefault;
        public ForceStab ForceStab { get; set; } = ForceStabDefault;
        public ModifyAttackType ModifyAttackType { get; set; } = ModifyAttackTypeDefault;
        public ModifyEffectivenessIncoming ModifyEffectivenessIncoming { get; set; } = ModifyEffectivenessIncomingDefault;
        public ModifyEffectivenessOutgoing ModifyEffectivenessOutgoing { get; set; } = ModifyEffectivenessOutgoingDefault;
        public ModifyStabAmount ModifyStabAmount { get; set; } = ModifyStabAmountDefault;
        public PowerupType PowerupType { get; set; } = PowerupTypeDefault;
        public UpdateLifeRegen UpdateLifeRegen { get; set; } = UpdateLifeRegenDefault;
        public BuffOnHit BuffOnHit { get; set; } = BuffOnHitDefault;
        public MessageOnHit MessageOnHit { get; set; } = MessageOnHitDefault;
        public MessageHitEnemy MessageHitEnemy { get; set; } = MessageHitEnemyDefault;
        public AttractProjectile AttractProjectile { get; set; } = AttractProjectileDefault;
        public ModifyOpponentsAbility ModifyOpponentsAbility { get; set; } = ModifyOpponentsAbilityDefault;

        public Ability(AbilityID abilityID)
        {
            this.ID = abilityID;
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
