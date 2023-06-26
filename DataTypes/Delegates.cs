using TerraTyping.Core;

namespace TerraTyping.DataTypes;

public delegate void ModifyEffectivenessDelegate(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement);
public delegate ElementArray ModifyTypeDelegate<T>(T t);