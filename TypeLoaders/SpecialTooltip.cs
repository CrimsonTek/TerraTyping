using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TerraTyping.Core;
using TerraTyping.Helpers;

namespace TerraTyping.TypeLoaders;

public class SpecialTooltip
{
    private static Stack<DelayedTooltip> delayedTooltips;
    private Color[] colors;

    private static Stack<DelayedTooltip> DelayedTooltips
    {
        get => (delayedTooltips ??= new Stack<DelayedTooltip>());
        set => delayedTooltips = value;
    }

    public string TooltipString { get; private set; }

    public Color[] Colors { get => colors ?? Array.Empty<Color>(); private set => colors = value; }

    public static SpecialTooltip[] Parse(string input, out bool overrideSpecialTooltip)
    {
        List<SpecialTooltip> specialTooltips = new List<SpecialTooltip>();
        try
        {
            JObject jObject = (JsonConvert.DeserializeObject(input) as JObject) ?? new JObject();

            bool dontOverride = jObject.Value<bool>("DontOverride");

            JArray jArray = jObject.Value<JArray>("Tooltips") ?? new JArray();
            for (int i = 0; i < jArray.Count; i++)
            {
                specialTooltips.Add(ParseJToken(jArray[i]));
            }

            if (dontOverride)
            {
                overrideSpecialTooltip = false;
            }
            else
            {
                overrideSpecialTooltip = specialTooltips.Count > 0;
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Threw exception while parsing \"{input}\"", e);
        }

        if (specialTooltips.Count == 0)
        {
            return Array.Empty<SpecialTooltip>();
        }

        return specialTooltips.ToArray();
    }

    public static bool TryParse(string input, out bool overrideSpecialTooltip, [NotNullWhen(true)] out SpecialTooltip[] specialTooltips, [NotNullWhen(false)] out Exception error)
    {
        List<SpecialTooltip> specialTooltipList = new List<SpecialTooltip>();
        try
        {
            JObject jObject = (JsonConvert.DeserializeObject(input) as JObject) ?? new JObject();

            bool dontOverride = jObject.Value<bool>("DontOverride");

            JArray jArray = jObject.Value<JArray>("Tooltips") ?? new JArray();
            for (int i = 0; i < jArray.Count; i++)
            {
                specialTooltipList.Add(ParseJToken(jArray[i]));
            }

            if (dontOverride)
            {
                overrideSpecialTooltip = false;
            }
            else
            {
                overrideSpecialTooltip = specialTooltipList.Count > 0;
            }
        }
        catch (Exception e)
        {
            overrideSpecialTooltip = false;
            specialTooltips = null;
            error = new Exception($"Threw exception while parsing \"{input}\"", e);
            return false;
        }

        if (specialTooltipList.Count == 0)
        {
            specialTooltips = Array.Empty<SpecialTooltip>();
            error = null;
            return true;
        }

        specialTooltips = specialTooltipList.ToArray();
        error = null;
        return true;
    }

    private static SpecialTooltip ParseJToken(JToken jToken)
    {
        SpecialTooltip specialTooltip = new SpecialTooltip();

        DelayedTooltip delayedTooltip = jToken.ToObject<DelayedTooltip>();
        delayedTooltip.specialTooltip = specialTooltip;
        DelayedTooltips.Push(delayedTooltip);

        return specialTooltip;
    }

    internal static void Finish()
    {
        while (DelayedTooltips.TryPop(out var result))
        {
            if (result.TypeFrom is TypeFrom typeFrom)
            {
                ElementArray elements = typeFrom switch
                {
                    TypeFrom.Projectile => ProjectileTypeLoader.GetElements(result.Id),
                    TypeFrom.Item => WeaponTypeLoader.GetElements(result.Id),
                    _ => throw new Exception($"Unexpected switch case: {typeFrom}."),
                };

                result.specialTooltip.TooltipString = string.Format(result.Tooltip, string.Join(", ", elements));
                result.specialTooltip.Colors = elements.Select(element => TerraTypingColors.GetColor(element)).ToArray();
            }
            else
            {
                result.specialTooltip.TooltipString = result.Tooltip;
            }
        }

        DelayedTooltips = null;
    }

    internal static void StaticLoad()
    {
        DelayedTooltips ??= new Stack<DelayedTooltip>();
    }

    internal static void StaticUnload()
    {
        DelayedTooltips = null;
    }

    internal class DelayedTooltip
    {
        public SpecialTooltip specialTooltip;

        public int Id = -1;
        public TypeFrom? TypeFrom = null;
        public string Tooltip = string.Empty;
    }

    internal enum TypeFrom
    {
        Projectile,
        Item,
    }
}