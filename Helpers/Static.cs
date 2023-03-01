using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Attributes;
using TerraTyping.DataTypes;
using TerraTyping.DataTypes.Structs;
using TerraTyping.Helpers;

namespace TerraTyping;

/// <summary>
/// A class for handling the order and storing objects and data which should be maintained throughout the life of the mod.
/// </summary>
public class Static
{
    public static ModifyEffectivenessDelegate ModifyEffectivenessDelegateDefault { get; private set; }

    internal static void Load()
    {
        ElementHelper.Load();
        ElementArray.Load();
        ModifyEffectivenessDelegateDefault = (ref float _, Element _, Element _) => { };
    }

    internal static void PostSetupContent()
    {
        ProjectileWrapper.PostSetupContent();
    }

    internal static void Unload()
    {
        TerraTyping.Instance.Logger.Debug($"Starting to unload {nameof(Static)}.");

        ProjectileWrapper.Unload();
        ModifyEffectivenessDelegateDefault = null;
        ElementArray.Unload();
        ElementHelper.Unload();

        TerraTyping.Instance.Logger.Debug($"Finished unloading {nameof(Static)}.");
    }
}
