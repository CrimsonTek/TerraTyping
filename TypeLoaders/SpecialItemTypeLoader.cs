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

    public static SpecialTooltip[] GetSpecialTooltips(Item item)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out ItemTypeInfo itemTypeInfo))
        {
            return itemTypeInfo.specialTooltips;
        }
        else
        {
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
        if (!string.IsNullOrWhiteSpace(cells[ColumnToIndex.D]))
        {
            specialTooltips = SpecialTooltip.Parse(cells[ColumnToIndex.D]);
        }

        typeInfos[itemID] = new ItemTypeInfo(ParseAtLeastOneElement(cells[ColumnToIndex.B..ColumnToIndex.D]), specialTooltips);
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

        public ItemTypeInfo(ElementArray elements, SpecialTooltip[] specialTooltips)
        {
            this.elements = elements;
            this.specialTooltips = specialTooltips;
        }
    }
}
