﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoMod.Cil;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using System.Reflection;
using TerraTyping.Abilities;
using TerraTyping.Helpers;

namespace TerraTyping.TypeLoaders;

public class WeaponTypeLoader : TypeLoader
{
    Dictionary<int, WeaponTypeInfo> typeInfos;

    protected override string CSVFileName => CSVFileNames.Weapons;
    public static WeaponTypeLoader Instance { get; private set; }

    public static ElementArray GetElements(Item item)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out WeaponTypeInfo weaponTypeInfo))
        {
            return weaponTypeInfo.elements;
        }
        else
        {
            return ElementArray.Default;
        }
    }
    public static ElementArray GetElements(int itemType)
    {
        if (Instance.typeInfos.TryGetValue(itemType, out WeaponTypeInfo weaponTypeInfo))
        {
            return weaponTypeInfo.elements;
        }
        else
        {
            return ElementArray.Default;
        }
    }
    public static SpecialTooltip[] GetSpecialTooltips(Item item, out bool overrideTypeTooltip)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out WeaponTypeInfo weaponTypeInfo))
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
    public static bool GetsStab(Item item)
    {
        return item is not null && Instance.typeInfos.ContainsKey(item.type);
    }
    public override void InitTypeInfoCollection()
    {
        typeInfos = new Dictionary<int, WeaponTypeInfo>();
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
        typeInfos[itemID] = new WeaponTypeInfo(elements, specialTooltips, overrideSpecialTooltip);

        return true;
    }
    protected override bool ParseLineMod(Mod modToGiveTypes, LineParser lineParser)
    {
        if (!TryParseLineGeneric(modToGiveTypes, lineParser.GetRange(HeaderKeys.GenericElement), lineParser.GetIndex(HeaderKeys.InternalName), out ElementArray elements, out ModItem modItem))
        {
            return false;
        }

        (SpecialTooltip[] specialTooltips, bool overrideSpecialTooltip) = GetSpecialTooltips(Context.Cells, lineParser);
        typeInfos[modItem.Item.type] = new WeaponTypeInfo(elements, specialTooltips, overrideSpecialTooltip);

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

    class WeaponTypeInfo
    {
        internal readonly ElementArray elements;
        internal readonly SpecialTooltip[] specialTooltips;
        internal readonly bool overrideTypeTooltip;

        public WeaponTypeInfo(ElementArray elements, SpecialTooltip[] specialTooltips, bool overrideTypeTooltip)
        {
            this.elements = elements;
            this.specialTooltips = specialTooltips;
            this.overrideTypeTooltip = overrideTypeTooltip;
        }
    }
}
