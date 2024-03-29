﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Core;
using TerraTyping.Helpers;

namespace TerraTyping.TypeLoaders;

public class SpecialItemTypeLoader : TypeLoader
{
    Dictionary<int, ItemTypeInfo> typeInfos;

    private Dictionary<int, ItemTypeInfo> TypeInfos { get => typeInfos ??= new Dictionary<int, ItemTypeInfo>(); set => typeInfos = value; }
    protected override string CSVFileName => CSVFileNames.SpecialItems;
    public static SpecialItemTypeLoader Instance { get; private set; }

    public static ElementArray GetElements(Item item)
    {
        if (item is not null && Instance.TypeInfos.TryGetValue(item.type, out ItemTypeInfo itemTypeInfo))
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
        if (Instance.TypeInfos.TryGetValue(itemType, out ItemTypeInfo itemTypeInfo))
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
        if (item is not null && Instance.TypeInfos.TryGetValue(item.type, out ItemTypeInfo itemTypeInfo))
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
        TypeInfos ??= new Dictionary<int, ItemTypeInfo>();
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
        TypeInfos[itemID] = new ItemTypeInfo(elements, specialTooltips, overrideSpecialTooltip);
        return true;
    }
    protected override bool ParseLineMod(Mod modToGiveTypes, LineParser lineParser)
    {
        if (!TryParseLineGeneric(modToGiveTypes, lineParser.GetRange(HeaderKeys.GenericElement), lineParser.GetIndex(HeaderKeys.InternalName), out ElementArray elements, out ModItem modItem))
        {
            return false;
        }

        (SpecialTooltip[] specialTooltips, bool overrideSpecialTooltip) = ItemTypeLoaderUtils.GetSpecialTooltips(Context.Cells.SafeGet(lineParser.GetIndex(HeaderKeys.SpecialTooltip)));
        TypeInfos[modItem.Item.type] = new ItemTypeInfo(elements, specialTooltips, overrideSpecialTooltip);
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
