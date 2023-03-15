using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

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

    protected override void InitTypeInfoCollection()
    {
        typeInfos = new NPCTypeInfo[NPCLoader.NPCCount];
    }

    protected override void ParseLine(string line, string[] cells, int lineCount)
    {
        if (9 >= cells.Length) // change number based on the highest needed index
        {
            return;
        }

        int npcID = int.Parse(cells[0]);

        ElementArray defenseElements = ParseAtLeastOneElement(cells[1..5]);
        ElementArray offenseElements = ParseAtLeastOneElement(cells[5..7]);
        AbilityContainer abilityContainer = ParseAbilities(cells[7], cells[8], cells[9]);
        ModifyTypeByEnvironment modifyTypeByEnvironment = ModifyType(npcID);

        typeInfos[npcID] = new NPCTypeInfo(defenseElements, offenseElements, abilityContainer, modifyTypeByEnvironment);
    }

    private static ModifyTypeByEnvironment ModifyType(int npcID)
    {
        return npcID switch
        {
            NPCID.Retinazer or NPCID.Spazmatism =>
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
            if (npc is null || modifyTypeByEnvironment is null)
            {
                return defensiveTypes;
            }
            else
            {
                return modifyTypeByEnvironment(this, npc).defensiveTypes;
            }
        }

        /// <inheritdoc cref="GetDefensiveElements(NPC)"/>
        public ElementArray GetOffensiveElements(NPC npc)
        {
            if (npc is null || modifyTypeByEnvironment is null)
            {
                return contactTypes;
            }
            else
            {
                return modifyTypeByEnvironment(this, npc).contactTypes;
            }
        }
    }

    private delegate NPCTypeInfo ModifyTypeByEnvironment(NPCTypeInfo original, NPC npc);
}
