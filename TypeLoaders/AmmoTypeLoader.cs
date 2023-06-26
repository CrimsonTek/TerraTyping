using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Core;

namespace TerraTyping.TypeLoaders;

public class AmmoTypeLoader : TypeLoader
{
    Dictionary<int, AmmoTypeInfo> typeInfos;

    protected override string CSVFileName => CSVFileNames.Ammo;
    public static AmmoTypeLoader Instance { get; private set; }

    public static ElementArray GetElements(Item item)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out AmmoTypeInfo ammoTypeInfo))
        {
            return ammoTypeInfo.elements;
        }
        else
        {
            return ElementArray.Default;
        }
    }
    public static ElementArray GetElements(int itemType)
    {
        if (Instance.typeInfos.TryGetValue(itemType, out AmmoTypeInfo ammoTypeInfo))
        {
            return ammoTypeInfo.elements;
        }
        else
        {
            return ElementArray.Default;
        }
    }
    public static SpecialTooltip[] GetSpecialTooltips(Item item, out bool overrideTypeTooltip)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out AmmoTypeInfo weaponTypeInfo))
        {
            overrideTypeTooltip = weaponTypeInfo.overrideTypeTooltip;
            return weaponTypeInfo.specialTooltips;
        }
        else
        {
            overrideTypeTooltip = false;
            return Array.Empty<SpecialTooltip>();
        }
    }
    public override void InitTypeInfoCollection()
    {
        typeInfos = new Dictionary<int, AmmoTypeInfo>();
    }
    protected override bool ParseHeader(string[] cells, string fileName, out LineParser lineParser)
    {
        return new HeaderParser()
            .NewIndexHeader(HeaderKeys.InternalName, true)
            .NewRangeHeader(HeaderKeys.GenericElement, true)
            .NewIndexHeader(HeaderKeys.SpecialTooltip, false)
            .ParseHeader(Context, out lineParser, this);
    }
    protected override bool ParseLine(LineParser lineParser)
    {
        if (Context.Cells.Length > 3)
        {
            return false;
        }

        if (!TryParseLineGeneric(lineParser.GetRange(HeaderKeys.GenericElement), lineParser.GetIndex(HeaderKeys.InternalName), out ElementArray elements, out int itemID))
        {
            return false;
        }

        (SpecialTooltip[] specialTooltips, bool overrideSpecialTooltip) = GetSpecialTooltips(Context.Cells, lineParser);
        typeInfos[itemID] = new AmmoTypeInfo(elements, specialTooltips, overrideSpecialTooltip);
        return true;
    }
    protected override bool ParseLineMod(Mod modToGiveTypes, LineParser lineParser)
    {
        if (!TryParseLineGeneric(modToGiveTypes, lineParser.GetRange(HeaderKeys.GenericElement), lineParser.GetIndex(HeaderKeys.InternalName), out ElementArray elements, out ModItem modItem))
        {
            return false;
        }

        (SpecialTooltip[] specialTooltips, bool overrideSpecialTooltip) = GetSpecialTooltips(Context.Cells, lineParser);
        typeInfos[modItem.Item.type] = new AmmoTypeInfo(elements, specialTooltips, overrideSpecialTooltip);

        return true;
    }

    public override void Load()
    {
        Instance = this;
    }
    public override void Unload()
    {
        Instance = null;
    }

    class AmmoTypeInfo
    {
        internal readonly ElementArray elements;
        internal readonly SpecialTooltip[] specialTooltips;
        internal readonly bool overrideTypeTooltip;

        public AmmoTypeInfo(ElementArray elements, SpecialTooltip[] specialTooltips, bool overrideTypeTooltip)
        {
            this.elements = elements;
            this.specialTooltips = specialTooltips;
            this.overrideTypeTooltip = overrideTypeTooltip;
        }
    }
}
