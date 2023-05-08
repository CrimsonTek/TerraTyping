using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Terraria;
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

    /// <summary>
    /// Prints if a condition is true.
    /// </summary>
    public static void PRINTIF(bool condition, string message)
    {
        if (condition)
        {
            TerraTyping.Instance.Logger.Debug(message);
        }
    }

    public static void NEWTEXT(object obj)
    {
        Main.NewText(obj);
    }

    public static void PRINT(object obj)
    {
        if (TerraTyping.Instance is null)
        {
            string message = $"{nameof(TerraTyping)}.{nameof(TerraTyping.Instance)} is null.";

            if (ModContent.GetInstance<TerraTyping>() is null)
            {
                message += $" No instance of {typeof(TerraTyping)} exists.";
            }
            else
            {
                message += $" An instance of {typeof(TerraTyping)} exists.";
            }

            throw new Exception(message);
        }
        else if (TerraTyping.Instance.Logger is null)
        {
            throw new Exception($"{nameof(TerraTyping)}.{nameof(TerraTyping.Instance)}.{nameof(TerraTyping.Instance.Logger)} is null.");
        }

        TerraTyping.Instance.Logger.Debug(obj);
    }

    public static void PRINTANDLABEL(object obj, [CallerArgumentExpression("obj")] string argumentExpression = "")
    {
        PRINT($"{argumentExpression}: {obj}");
    }

    public static void HERE([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        TerraTyping.Instance.Logger.Debug($"Made it to '{filePath}.{memberName} ({lineNumber})'");
    }

    public static void HERE(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        TerraTyping.Instance.Logger.Debug($"Made it to '{filePath}.{memberName} ({lineNumber})' with message:\t\n\"{message}\"");
    }

    public static void HEREIF(bool condition, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        if (condition)
        {
            TerraTyping.Instance.Logger.Debug($"Made it to '{filePath}.{memberName} ({lineNumber})'");
        }
    }

    public override void PreUpdateTime()
    {

    }
}

[Obsolete]
public class PRINTER
{
    public string name;
    public int counter;

    public PRINTER()
    {
        counter = 0;
    }

    public PRINTER(string name)
    {
        counter = 0;
        this.name = name;
    }

    public void PRINTNEXT()
    {
        if (string.IsNullOrEmpty(name))
        {
            DEBUG.PRINT(counter++);
        }
        else
        {
            DEBUG.PRINT($"{name}: {counter++}");
        }
    }
}