using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

namespace TerraTyping.TypeLoaders;

public class AmmoTypeLoader : TypeLoader
{
    Dictionary<int, AmmoTypeInfo> typeInfos;

    protected override string CSVFileName => "ammoTypes";

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

    protected override void InitTypeInfoCollection()
    {
        typeInfos = new Dictionary<int, AmmoTypeInfo>();
    }

    protected override void ParseLine(string line, string[] cells, int lineCount)
    {
        if (cells.Length <= ColumnToIndex.C)
        {
            return;
        }

        try
        {
            int itemID = int.Parse(cells[0]);

            typeInfos[itemID] = new AmmoTypeInfo(ParseAtLeastOneElement(cells[ColumnToIndex.B..ColumnToIndex.D]));
        }
        catch (Exception)
        {
            //Logger.Error($"Threw exception while parsing ammo. Line: '{line}'.", e);
        }
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
        public readonly ElementArray elements;

        public AmmoTypeInfo(ElementArray elements)
        {
            this.elements = elements;
        }
    }
}
