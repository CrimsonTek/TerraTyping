using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Terraria.ModLoader;

namespace TerraTyping.Helpers;

[Obsolete]
public class DEBUG : ModSystem
{
    public static void PRINTIFNULL(object obj, string name)
    {
        if (obj is null)
        {
            TerraTyping.Instance.Logger.Debug($"Object (\'{name}\') is null.");
        }
    }

    /// <summary>
    /// Checks for a condition; if the condition is false, logs the message.
    /// </summary>
    public static void ASSERT(bool condition, string message)
    {
        if (!condition)
        {
            TerraTyping.Instance.Logger.Debug(message);
        }
    }

    public static void PRINT(object obj)
    {
        TerraTyping.Instance.Logger.Debug(obj);
    }

    public override void PreUpdateTime()
    {

    }
}
