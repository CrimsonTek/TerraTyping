using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;

namespace TerraTyping.TypeLoaders;

public class SpecialItemTypeLoader : TypeLoader
{
    Dictionary<int, ItemTypeInfo> typeInfos;

    protected override string CSVFileName => CSVFileNames.SpecialItems;
    public static SpecialItemTypeLoader Instance { get; private set; }

    public static ElementArray GetElements(Item item)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out ItemTypeInfo itemTypeInfo))
        {
            return itemTypeInfo.elements;
        }
        else
        {
            return ElementArray.Default;
        }
    }
    public static ElementArray GetElements(int itemType)
    {
        if (Instance.typeInfos.TryGetValue(itemType, out ItemTypeInfo itemTypeInfo))
        {
            return itemTypeInfo.elements;
        }
        else
        {
            return ElementArray.Default;
        }
    }
    public static SpecialTooltip[] GetSpecialTooltips(Item item, out bool overrideTypeTooltip)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out ItemTypeInfo itemTypeInfo))
        {
            overrideTypeTooltip = itemTypeInfo.overrideTypeTooltip;
            return itemTypeInfo.specialTooltips;
        }
        else
        {
            overrideTypeTooltip = false;
            return Array.Empty<SpecialTooltip>();
        }
    }
    public override void InitTypeInfoCollection()
    {
        typeInfos = new Dictionary<int, ItemTypeInfo>();
    }
    protected override bool ParseHeader(string[] cells, string fileName, out LineParser lineParser)
    {
        bool parsed = new HeaderParser()
            .NewIndexHeader(HeaderKeys.InternalName, true)
            .NewRangeHeader(HeaderKeys.GenericElement, true)
            .NewIndexHeader(HeaderKeys.SpecialTooltip, false)
            .ParseHeader(Context, out lineParser, this);
        return parsed;
    }
    protected override bool ParseLine(LineParser lineParser)
    {
        if (!TryParseLineGeneric(lineParser.GetRange(HeaderKeys.GenericElement), lineParser.GetIndex(HeaderKeys.InternalName), out ElementArray elements, out int itemID))
        {
            return false;
        }

        (SpecialTooltip[] specialTooltips, bool overrideSpecialTooltip) = ItemTypeLoaderUtils.GetSpecialTooltips(Context.Cells.SafeGet(lineParser.GetIndex(HeaderKeys.SpecialTooltip)));
        typeInfos[itemID] = new ItemTypeInfo(elements, specialTooltips, overrideSpecialTooltip);
        return true;
    }
    protected override bool ParseLineMod(Mod modToGiveTypes, LineParser lineParser)
    {
        if (!TryParseLineGeneric(modToGiveTypes, lineParser.GetRange(HeaderKeys.GenericElement), lineParser.GetIndex(HeaderKeys.InternalName), out ElementArray elements, out ModItem modItem))
        {
            return false;
        }

        (SpecialTooltip[] specialTooltips, bool overrideSpecialTooltip) = ItemTypeLoaderUtils.GetSpecialTooltips(Context.Cells.SafeGet(lineParser.GetIndex(HeaderKeys.SpecialTooltip)));
        typeInfos[modItem.Item.type] = new ItemTypeInfo(elements, specialTooltips, overrideSpecialTooltip);
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

    class ItemTypeInfo
    {
        public readonly ElementArray elements;
        public readonly SpecialTooltip[] specialTooltips;
        public readonly bool overrideTypeTooltip;

        public ItemTypeInfo(ElementArray elements, SpecialTooltip[] specialTooltips, bool overrideTypeTooltip)
        {
            this.elements = elements;
            this.specialTooltips = specialTooltips;
            this.overrideTypeTooltip = overrideTypeTooltip;
        }
    }
}
