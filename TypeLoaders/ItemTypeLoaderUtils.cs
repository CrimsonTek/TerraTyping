using System;

namespace TerraTyping.TypeLoaders;

internal static class ItemTypeLoaderUtils
{
    public static void GetSpecialTooltips(string str, out SpecialTooltip[] specialTooltips, out bool overrideTooltip)
    {
        overrideTooltip = false;
        if (!string.IsNullOrWhiteSpace(str))
        {
            specialTooltips = SpecialTooltip.Parse(str, out overrideTooltip);
        }
        else
        {
            specialTooltips = Array.Empty<SpecialTooltip>();
        }
    }

    public static (SpecialTooltip[] specialTooltips, bool overrideTooltip) GetSpecialTooltips(string str)
    {
        GetSpecialTooltips(str, out SpecialTooltip[] specialTooltips, out bool overrideTooltip);
        return (specialTooltips, overrideTooltip);
    }
}
