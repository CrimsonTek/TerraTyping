using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

namespace TerraTyping.TypeLoaders;

public class ArmorTypeLoader : TypeLoader
{
    Dictionary<int, ArmorTypeInfo> typeInfos;

    protected override string CSVFileName => CSVFileNames.Armor;

    public static ArmorTypeLoader Instance { get; private set; }

    public static ElementArray GetElements(Item item)
    {
        if (item is null || !Instance.typeInfos.TryGetValue(item.type, out ArmorTypeInfo armorTypeInfo))
        {
            return ElementArray.Default;
        }

        return armorTypeInfo.Elements;
    }

    public static AbilityID GetAbility(Item item)
    {
        if (item is null || !Instance.typeInfos.TryGetValue(item.type, out ArmorTypeInfo armorTypeInfo))
        {
            return AbilityID.None;
        }

        return armorTypeInfo.AbilityID;
    }

    protected override void InitTypeInfoCollection()
    {
        typeInfos = new Dictionary<int, ArmorTypeInfo>();
    }

    protected override void ParseLine(string line, string[] cells, int lineCount)
    {
        if (cells.Length < ColumnToIndex.F)
        {
            return;
        }

        int itemID = int.Parse(cells[0]);

        if (!Enum.TryParse(cells[ColumnToIndex.F], true, out AbilityID abilityID))
        {
            abilityID = AbilityID.None;
        }

        typeInfos[itemID] = new ArmorTypeInfo(ParseAtLeastOneElement(cells[ColumnToIndex.B..ColumnToIndex.E]), abilityID);
        
    }

    public override void Load()
    {
        Instance = this;
    }

    public override void Unload()
    {
        Instance = null;
    }

    private class ArmorTypeInfo
    {
        public ElementArray Elements { get; set; }

        public AbilityID AbilityID { get; set; }

        public static ArmorTypeInfo PlayerDefault => new ArmorTypeInfo(ElementArray.Get(Element.normal));

        public static ArmorTypeInfo ArmorDefault => new ArmorTypeInfo(ElementArray.Default);

        [Obsolete("Use a different ctor")]
        public ArmorTypeInfo(Element primary, Element secondary)
        {
            AbilityID = AbilityID.None;

            Elements = ElementArray.Get();
        }

        public ArmorTypeInfo(ElementArray elements, AbilityID abilityID = AbilityID.None)
        {
            Elements = elements;
            AbilityID = abilityID;
        }
    }
}
