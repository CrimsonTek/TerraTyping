using System;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;
using TerraTyping.Core;

namespace TerraTyping.TypeLoaders;

public class NPCTypeLoader : TypeLoader
{
    NPCTypeInfo[] typeInfos;

    protected override string CSVFileName => CSVFileNames.NPCs;
    public static NPCTypeLoader Instance { get; private set; }

    /// <exception cref="ArgumentNullException"></exception>
    public static ElementArray GetDefensiveElements(NPC npc)
    {
        if (npc is null)
        {
            throw new ArgumentNullException(nameof(npc));
        }

        if (0 <= npc.type && npc.type < Instance.typeInfos.Length)
        {
            NPCTypeInfo npcTypeInfo = Instance.typeInfos[npc.type];
            if (npcTypeInfo is not null)
            {
                return npcTypeInfo.GetDefensiveElements(npc);
            }
        }

        return ElementArray.Default;
    }
    /// <exception cref="ArgumentNullException"></exception>
    public static ElementArray GetOffensiveElements(NPC npc)
    {
        if (npc is null)
        {
            throw new ArgumentNullException(nameof(npc));
        }

        if (0 <= npc.type && npc.type < Instance.typeInfos.Length)
        {
            NPCTypeInfo npcTypeInfo = Instance.typeInfos[npc.type];
            if (npcTypeInfo is not null)
            {
                return npcTypeInfo.GetOffensiveElements(npc);
            }
        }

        return ElementArray.Default;
    }
    /// <summary>
    /// Returns the possible abilities for this NPC.
    /// </summary>
    public static AbilityContainer GetAbilities(int npcType)
    {
        if (npcType >= 0 && npcType < Instance.typeInfos.Length)
        {
            NPCTypeInfo npcTypeInfo = Instance.typeInfos[npcType];
            if (npcTypeInfo is not null)
            {
                return npcTypeInfo.abilities;
            }
        }
        return AbilityContainer.None;
    }
    public override void InitTypeInfoCollection()
    {
        typeInfos = new NPCTypeInfo[NPCLoader.NPCCount];
    }
    protected override bool ParseHeader(string[] cells, string fileName, out LineParser lineParser)
    {
        bool parsed = new HeaderParser()
            .NewIndexHeader(HeaderKeys.InternalName, true)
            .NewRangeHeader(HeaderKeys.DefensiveElement, true)
            .NewRangeHeader(HeaderKeys.OffensiveElement, true)
            .NewRangeHeader(HeaderKeys.BasicAbility, false)
            .NewRangeHeader(HeaderKeys.HiddenAbility, false)
            .NewIndexHeader(HeaderKeys.ModifyType, false)
            .ParseHeader(Context, out lineParser, this);

        return parsed;
    }
    protected override bool ParseLine(LineParser lineParser)
    {
        if (!int.TryParse(Context.Cells.SafeGet(lineParser.GetIndex(HeaderKeys.InternalName)), out int npcID))
        {
            return false;
        }

        ElementArray defenseElements = ParseAtLeastOneElement(Context.Cells.SafeGet(lineParser.GetRange(HeaderKeys.DefensiveElement)));
        ElementArray offenseElements = ParseAtLeastOneElement(Context.Cells.SafeGet(lineParser.GetRange(HeaderKeys.OffensiveElement)));

        string[] basicAbilityStrings = Array.Empty<string>();
        if (lineParser.TryGetRange(HeaderKeys.BasicAbility, out Range basicAbilityRange))
        {
            basicAbilityStrings = Context.Cells.SafeGet(basicAbilityRange);
        }

        string[] hiddenAbilityStrings = Array.Empty<string>();
        if (lineParser.TryGetRange(HeaderKeys.BasicAbility, out Range hiddenAbiltyRange))
        {
            hiddenAbilityStrings = Context.Cells.SafeGet(hiddenAbiltyRange);
        }

        typeInfos[npcID] = new NPCTypeInfo(defenseElements, offenseElements, ParseAbilities(basicAbilityStrings, hiddenAbilityStrings), GetModifyTypeDelegate(lineParser));
        return true;
    }
    protected override bool ParseLineMod(Mod modToGiveTypes, LineParser lineParser)
    {
        string internalName = Context.Cells.SafeGet(lineParser.GetIndex(HeaderKeys.InternalName));

        if (!modToGiveTypes.TryFind(internalName, out ModNPC modNPC))
        {
            Logger.Log(Verbosity.Warn, GetType().Name, $"ModNPC named '{internalName}' could not be found in mod '{modToGiveTypes}'.", Context);
            return false;
        }

        ElementArray defenseElements = ParseAtLeastOneElement(Context.Cells.SafeGet(lineParser.GetRange(HeaderKeys.DefensiveElement)));
        ElementArray offenseElements = ParseAtLeastOneElement(Context.Cells.SafeGet(lineParser.GetRange(HeaderKeys.OffensiveElement)));

        string[] basicAbilityStrings = Array.Empty<string>();
        if (lineParser.TryGetRange(HeaderKeys.BasicAbility, out Range basicAbilityRange))
        {
            basicAbilityStrings = Context.Cells.SafeGet(basicAbilityRange);
        }

        string[] hiddenAbilityStrings = Array.Empty<string>();
        if (lineParser.TryGetRange(HeaderKeys.BasicAbility, out Range hiddenAbiltyRange))
        {
            hiddenAbilityStrings = Context.Cells.SafeGet(hiddenAbiltyRange);
        }

        typeInfos[modNPC.NPC.type] = new NPCTypeInfo(defenseElements, offenseElements, ParseAbilities(basicAbilityStrings, hiddenAbilityStrings), GetModifyTypeDelegate(lineParser));
        return true;
    }
    private ModifyTypeByEnvironment GetModifyTypeDelegate(LineParser lineParser)
    {
        ModifyTypeByEnvironment modifyTypeByEnvironment = null;
        if (lineParser.TryGetIndex(HeaderKeys.ModifyType, out int modifyTypeIndex))
        {
            string modifyTypeDelegateName = Context.Cells.SafeGet(modifyTypeIndex);
            modifyTypeByEnvironment = GetModifyTypeDelegate(modifyTypeDelegateName);
            if (!string.IsNullOrWhiteSpace(modifyTypeDelegateName) && modifyTypeByEnvironment is null)
            {
                Logger.Log(Verbosity.Warn, GetType().Name, $"Unrecognized modify type delegate name: {modifyTypeDelegateName}", Context);
            }
        }

        return modifyTypeByEnvironment;
    }
    private static ModifyTypeByEnvironment GetModifyTypeDelegate(string modifyTypeDelegateName)
    {
        return modifyTypeDelegateName switch
        {
            "ModifyType_Retinazer" or "ModifyType_Spazmatism" =>
            (typeInfo, npc) => npc.ai[0] < 2 ? typeInfo : new NPCTypeInfo(ElementArray.Get(Element.steel, Element.flying), ElementArray.Get(Element.steel)),
            _ => null,
        };
    }
    public override void Load()
    {
        Instance = this;
    }
    public override void Unload()
    {
        Instance = null;
    }

    private class NPCTypeInfo
    {
        private readonly ElementArray defensiveTypes;
        private readonly ElementArray contactTypes;
        private readonly ModifyTypeByEnvironment modifyTypeByEnvironment;
        public readonly AbilityContainer abilities;

        [Obsolete]
        public Element Primary => defensiveTypes.Length > 0 ? defensiveTypes[0] : Element.none;
        [Obsolete]
        public Element Secondary => defensiveTypes.Length > 1 ? defensiveTypes[1] : Element.none;
        [Obsolete]
        public Element Offensive => contactTypes.Length > 0 ? contactTypes[0] : Element.none;

        public static NPCTypeInfo Default => new NPCTypeInfo();

        public NPCTypeInfo()
        {
            defensiveTypes = ElementArray.Get();
            contactTypes = ElementArray.Get();
            modifyTypeByEnvironment = null;
            abilities = AbilityContainer.None;
        }

        public NPCTypeInfo(ElementArray defensiveTypes, ElementArray contactTypes)
        {
            this.defensiveTypes = defensiveTypes ?? throw new ArgumentNullException(nameof(defensiveTypes));
            this.contactTypes = contactTypes ?? throw new ArgumentNullException(nameof(contactTypes));
            this.abilities = AbilityContainer.None;
            modifyTypeByEnvironment = null;
        }

        public NPCTypeInfo(ElementArray defensiveTypes, ElementArray contactTypes, AbilityContainer abilities)
        {
            this.defensiveTypes = defensiveTypes ?? throw new ArgumentNullException(nameof(defensiveTypes));
            this.contactTypes = contactTypes ?? throw new ArgumentNullException(nameof(contactTypes));
            this.abilities = abilities;
            modifyTypeByEnvironment = null;
        }

        public NPCTypeInfo(ElementArray defensiveTypes, ElementArray contactTypes, ModifyTypeByEnvironment modifyTypeByEnvironment)
        {
            this.defensiveTypes = defensiveTypes ?? throw new ArgumentNullException(nameof(defensiveTypes));
            this.contactTypes = contactTypes ?? throw new ArgumentNullException(nameof(contactTypes));
            abilities = AbilityContainer.None;
            this.modifyTypeByEnvironment = modifyTypeByEnvironment;
        }

        public NPCTypeInfo(ElementArray defensiveTypes, ElementArray contactTypes, AbilityContainer abilities, ModifyTypeByEnvironment modifyTypeByEnvironment)
        {
            this.defensiveTypes = defensiveTypes ?? throw new ArgumentNullException(nameof(defensiveTypes));
            this.contactTypes = contactTypes ?? throw new ArgumentNullException(nameof(contactTypes));
            this.abilities = abilities;
            this.modifyTypeByEnvironment = modifyTypeByEnvironment;
        }

        /// <summary>
        /// Pass null to <paramref name="npc"/> to get the default type for this NPC. Passing the NPC is always better whenever possible.
        /// </summary>
        /// <param name="npc">Pass null to get the default type for this NPC. Passing the NPC is always better whenever possible.</param>
        /// <returns></returns>
        public ElementArray GetDefensiveElements(NPC npc)
        {
            if (npc is not null && modifyTypeByEnvironment is not null)
            {
                return modifyTypeByEnvironment(this, npc).defensiveTypes;
            }
            else
            {
                return defensiveTypes;
            }
        }

        /// <inheritdoc cref="GetDefensiveElements(NPC)"/>
        public ElementArray GetOffensiveElements(NPC npc)
        {
            if (npc is not null && modifyTypeByEnvironment is not null)
            {
                return modifyTypeByEnvironment(this, npc).contactTypes;
            }
            else
            {
                return contactTypes;
            }
        }
    }

    private delegate NPCTypeInfo ModifyTypeByEnvironment(NPCTypeInfo original, NPC npc);
}
