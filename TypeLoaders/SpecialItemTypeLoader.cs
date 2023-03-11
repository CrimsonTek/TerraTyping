using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerraTyping.DataTypes;

namespace TerraTyping.TypeLoaders;

public class SpecialItemTypeLoader : TypeLoader
{
    Dictionary<int, ItemTypeInfo> typeInfos;

    protected override string CSVFileName => "specialItemTypes";

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

    protected override void InitTypeInfoCollection()
    {
        typeInfos = new Dictionary<int, ItemTypeInfo>();
    }

    protected override void ParseLine(string line, string[] cells, int lineCount)
    {
        if (cells.Length < 4)
        {
            return;
        }

        int itemID = int.Parse(cells[0]);

        SpecialTooltip[] specialTooltips = Array.Empty<SpecialTooltip>();
        bool overrideSpecialTooltip = false;
        if (!string.IsNullOrWhiteSpace(cells[ColumnToIndex.D]))
        {
            specialTooltips = SpecialTooltip.Parse(cells[ColumnToIndex.D], out overrideSpecialTooltip);
        }

        typeInfos[itemID] = new ItemTypeInfo(ParseAtLeastOneElement(cells[ColumnToIndex.B..ColumnToIndex.D]), specialTooltips, overrideSpecialTooltip);
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
