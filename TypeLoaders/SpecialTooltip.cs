using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.ModLoader;
using TerraTyping.Common;
using TerraTyping.DataTypes;

namespace TerraTyping.TypeLoaders;

public class SpecialTooltip
{
    private static Stack<DelayedTooltip> delayedTooltips;

    public string TooltipString { get; private set; }

    public Color[] Colors { get; private set; }

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

    private static SpecialTooltip ParseJToken(JToken jToken)
    {
        SpecialTooltip specialTooltip = new SpecialTooltip();

        if (TypeLoader.IsLoadingTypes)
        {
            DelayedTooltip delayedTooltip = jToken.ToObject<DelayedTooltip>();
            delayedTooltip.specialTooltip = specialTooltip;
            delayedTooltips.Push(delayedTooltip);
        }
        else
        {
            throw new Exception("Special tooltip is being created while not loading types.");
        }

        return specialTooltip;
    }

    internal static void Finish()
    {
        while (delayedTooltips.TryPop(out var result))
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

        delayedTooltips = null;
    }

    internal static void StaticLoad()
    {
        delayedTooltips = new Stack<DelayedTooltip>();
    }

    internal static void StaticUnload()
    {
        delayedTooltips = null;
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