using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;

namespace TerraTyping.TypeLoaders;

public class ParseContext
{
    public string FileName { get; internal set; }
    public int LineCount { get; internal set; }
    public string Line { get; internal set; }
    public string[] Cells { get; internal set; }
    /// <summary>
    /// May be null.
    /// </summary>
    public Mod ModSource { get; internal set; }
    /// <summary>
    /// May be null.
    /// </summary>
    public IDictionary<string, MethodInfo> Delegates { get; internal set; }

    public ParseContext()
    {
        FileName = string.Empty;
        LineCount = -2;
        Line = string.Empty;
        Cells = Array.Empty<string>();
        ModSource = null;
    }
}
