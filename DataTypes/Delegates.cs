using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TerraTyping.DataTypes;

public delegate void ModifyEffectivenessDelegate(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement);
public delegate ElementArray ModifyTypeDelegate<T>(T t);