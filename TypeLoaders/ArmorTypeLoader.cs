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
    public override void InitTypeInfoCollection()
    {
        typeInfos = new Dictionary<int, ArmorTypeInfo>();
    }
    protected override bool ParseHeader(string[] cells, string fileName, out LineParser lineParser)
    {
        bool parsed = new HeaderParser()
            .NewIndexHeader(HeaderKeys.InternalName, true)
            .NewRangeHeader(HeaderKeys.GenericElement, true)
            .NewIndexHeader(HeaderKeys.BasicAbility, true)
            .ParseHeader(Context, out lineParser, this);

        return parsed;
    }
    protected override bool ParseLine(LineParser lineParser)
    {
        if (!int.TryParse(Context.Cells.SafeGet(lineParser.GetIndex(HeaderKeys.InternalName), ""), out int itemID))
        {
            return false;
        }

        AbilityID abilityID = AbilityID.None;
        if (lineParser.TryGetIndex(HeaderKeys.BasicAbility, out int abilityIndex))
        {
            if (Enum.TryParse(Context.Cells.SafeGet(abilityIndex), true, out AbilityID resultAbility))
            {
                abilityID = resultAbility;
            }
        }

        typeInfos[itemID] = new ArmorTypeInfo(ParseAtLeastOneElement(Context.Cells[lineParser.GetRange(HeaderKeys.GenericElement)]), abilityID);

        return true;
    }
    protected override bool ParseLineMod(Mod modToGiveTypes, LineParser lineParser)
    {
        AbilityID abilityID = AbilityID.None;

        if (!TryParseLineGeneric(modToGiveTypes, lineParser.GetRange(HeaderKeys.GenericElement), lineParser.GetIndex(HeaderKeys.InternalName), out ElementArray elements, out ModItem modItem))
        {
            return false;
        }

        if (lineParser.TryGetIndex(HeaderKeys.BasicAbility, out int abilityIndex))
        {
            if (Enum.TryParse(Context.Cells.SafeGet(abilityIndex), out AbilityID result))
            {
                abilityID = result;
            }
        }

        typeInfos[modItem.Item.type] = new ArmorTypeInfo(elements, abilityID);
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
