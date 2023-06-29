using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.Core;
using TerraTyping.Helpers;

namespace TerraTyping.DataTypes.Wrappers;

public class OtherWrapper : Wrapper, IOffensiveType, IAbility, IDamageClass, IStatsBuffed
{
    private readonly int otherDamageSourceIndex;
    private readonly ElementArray elementArray;

    public OtherWrapper(int otherDamageSourceIndex, Element element)
    {
        this.otherDamageSourceIndex = otherDamageSourceIndex;
        elementArray = ElementArray.Get(element);
    }

    public ElementArray OffensiveElements => elementArray;


    public Ability GetAbility => Ability.None;

    public bool Melee => otherDamageSourceIndex == OtherDamageID.CompanionCubeStabbed;

    public bool Ranged => false;

    public bool Magic => false;

    public bool Summon => false;

    public float DamageMultiplication() => 1;

    public void ModifyEffectiveness(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement) { }
}
