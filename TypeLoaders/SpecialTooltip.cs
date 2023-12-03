using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.ModLoader;
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

    private static SpecialTooltip ParseJToken2(JToken jToken)
    {
        SpecialTooltip specialTooltip = new SpecialTooltip();

        DelayedTooltip delayedTooltip = jToken.ToObject<DelayedTooltip>();

        List<Element> elements = new List<Element>();
        string elementString = jToken.Value<string>("Element");
        if (Enum.TryParse(elementString, true, out Element result))
        {
            elements.Add(result);
        }
        IEnumerable<string> elementStrings = jToken.Values<string>("Elements");
        foreach (string str in elementStrings)
        {
            if (Enum.TryParse(str, true, out Element result1))
            {
                elements.Add(result1);
            }
        }


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
                ElementArray elements = ElementArray.Default;
                elements = result.GetElements();

                result.specialTooltip.TooltipString = string.Format(result.Tooltip, string.Join(", ", elements));
                result.specialTooltip.Colors = elements.Select(TerraTypingColors.GetColor).ToArray();
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
        public string Mod;
        public string Name;

        public ElementArray GetElements()
        {
            if (TypeFrom is TypeFrom typeFromNotNull)
            {
                if (Id >= 0)
                {
                    return typeFromNotNull switch
                    {
                        SpecialTooltip.TypeFrom.Projectile => ProjectileTypeLoader.GetElements(Id),
                        SpecialTooltip.TypeFrom.Item => WeaponTypeLoader.GetElements(Id),
                        _ => ElementArray.Default,
                    };
                }
                else if ((!string.IsNullOrWhiteSpace(Mod)) && (!string.IsNullOrEmpty(Name)))
                {
                    if (ModLoader.TryGetMod(Mod, out Mod mod))
                    {
                        switch (typeFromNotNull)
                        {
                            case SpecialTooltip.TypeFrom.Projectile:
                                if (mod.TryFind(Name, out ModProjectile modProjectile))
                                {
                                    return ProjectileTypeLoader.GetElements(modProjectile.Type);
                                }
                                break;

                            case SpecialTooltip.TypeFrom.Item:
                                if (mod.TryFind(Name, out ModItem modItem))
                                {
                                    return WeaponTypeLoader.GetElements(modItem.Type);
                                }
                                break;
                        }
                    }
                }
            }

            return ElementArray.Default;
        }
    }

    internal enum TypeFrom
    {
        Projectile,
        Item,

    }
}