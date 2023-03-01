using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;
using TerraTyping.TypeLoaders;

namespace TerraTyping.Commands
{
    public class TypeCommand : ModCommand
    {
        static Color superEffectiveColor = Color.Green;
        static Color effectiveColor = Color.LightGreen;
        static Color neutralColor = Color.LightGray;
        static Color ineffectiveColor = Color.Salmon;
        static Color superIneffectiveColor = Color.Red;
        static Color immuneColor = Color.DimGray;

        public override string Command => "type";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                caller.Reply("Here are some available commands:");
                PrintCommands();
                return;
            }

            if (PrintElementEffectiveness(caller, args)) return;
            else if (PrintNPCTyping(caller, args)) return;
            else
            {
                caller.Reply($"Unable to parse {input}. Please try one of the following:", Color.Red);
                PrintCommands();
                return;
            }

            void PrintCommands()
            {
                caller.Reply("\"/type [type]\"");
                caller.Reply("\"/type npc [npc name]\"");
            }
        }

        private static bool PrintElementEffectiveness(CommandCaller caller, string[] args)
        {
            if (args.Length == 0)
                return false;

            if (Enum.TryParse(args[0], true, out Element result))
            {
                Color color = ElementColors.GetColor(result);
                string elementName = LangHelper.ElementName(result);

                // element is defender
                List<string> susceptibleTo = new List<string>();
                List<string> neutralTo = new List<string>();
                List<string> resistantTo = new List<string>();
                List<string> immuneTo = new List<string>();

                // element is attacker
                List<string> strongAgainst = new List<string>();
                List<string> neutralAgainst = new List<string>();
                List<string> weakAgainst = new List<string>();
                List<string> nothingAgainst = new List<string>();

                int defenseCount = Table.EffectivenessTable.GetLength(0);
                for (int i = 0; i < defenseCount; i++)
                {
                    float e = Table.EffectivenessTable[(int)result, i];
                    if (e == 0f)
                        nothingAgainst.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == Table.Divi)
                        weakAgainst.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == 1f)
                        neutralAgainst.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == Table.Mult)
                        strongAgainst.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else
                        throw new Exception("Unexpected effectiveness case.");
                }

                int attackCount = Table.EffectivenessTable.GetLength(1);
                for (int i = 0; i < attackCount; i++)
                {
                    float e = Table.EffectivenessTable[i, (int)result];
                    if (e == 0f)
                        immuneTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == Table.Divi)
                        resistantTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == 1f)
                        neutralTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == Table.Mult)
                        susceptibleTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else
                        throw new Exception("Unexpected effectiveness case.");
                }

                caller.Reply($"{elementName} Defensive Stats:", color);
                caller.Reply($" > Effective against {elementName}: {string.Join(", ", susceptibleTo)}", effectiveColor);
                caller.Reply($" > Neutral against {elementName}: {string.Join(", ", neutralTo)}", neutralColor);
                caller.Reply($" > Ineffective against {elementName}: {string.Join(", ", resistantTo)}", ineffectiveColor);
                if (immuneTo.Count > 0) caller.Reply($"   > No effect against {elementName}: {string.Join(", ", immuneTo)}", immuneColor);

                caller.Reply($"{elementName} Offensive Stats:", color);
                caller.Reply($" > {elementName} effective against: {string.Join(", ", strongAgainst)}", effectiveColor);
                caller.Reply($" > {elementName} neutral against: {string.Join(", ", neutralAgainst)}", neutralColor);
                caller.Reply($" > {elementName} ineffective against: {string.Join(", ", weakAgainst)}", ineffectiveColor);
                if (nothingAgainst.Count > 0) caller.Reply($" > {elementName} no effect against: {string.Join(", ", nothingAgainst)}", immuneColor);

                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool PrintNPCTyping(CommandCaller caller, string[] args)
        {
            if (args.Length == 0)
                return false;

            if (args[0] != "npc")
                return false;

            string str = string.Join(" ", args, 1, args.Length - 1);
            NPC npc = new NPC();
            List<int> chosenIndexes = new List<int>();
            for (int i = NPCID.NegativeIDCount + 1; i < NPCLoader.NPCCount; i++)
            {
                npc.SetDefaults(i);
                if (npc.TypeName.Equals(str, StringComparison.OrdinalIgnoreCase))
                {
                    chosenIndexes.Clear();
                    chosenIndexes.Add(i);
                    break;
                }
                else if (npc.TypeName.Contains(str, StringComparison.OrdinalIgnoreCase))
                {
                    chosenIndexes.Add(i);
                }
            }

            if (chosenIndexes.Count == 0)
            {
                caller.Reply($"Unable to find any NPCs named {str}. Try checking your spelling or narrowing your search.", Color.Red);
                return false;
            }
            else if (chosenIndexes.Count == 1)
            {
                npc.SetDefaults(chosenIndexes[0]);
                AbilityContainer abilities = NPCTypeLoader.GetAbilities(chosenIndexes[0]);

                AbilityID _primaryAbility = abilities.PrimaryAbility;
                AbilityID _secondaryAbility = abilities.SecondaryAbility;
                AbilityID _hiddenAbility = abilities.HiddenAbility;

                // element is defender
                List<string> superSusceptibleTo = new List<string>();
                List<string> susceptibleTo = new List<string>();
                List<string> neutralTo = new List<string>();
                List<string> resistantTo = new List<string>();
                List<string> superResistantTo = new List<string>();
                List<string> immuneTo = new List<string>();

                ElementArray defensiveTypes = NPCTypeLoader.GetDefensiveElements(npc);
                ElementArray contactTypes = NPCTypeLoader.GetOffensiveElements(npc);
                var resistancesToTypes = new Dictionary<float, List<Element>>();

                int attackCount = Table.EffectivenessTable.GetLength(1);
                for (int i = 0; i < attackCount; i++)
                {
                    Element attackElement = ElementHelper.FromIndex(i);
                    float damage = defensiveTypes.DamageFrom(attackElement);
                    resistancesToTypes.AddIfMissingAndGet(damage, new List<Element>()).Add(attackElement);
                }

                caller.Reply($"{npc.TypeName} typing:");
                for (int i = 0; i < defensiveTypes.Length; i++)
                {
                    Element element = defensiveTypes[i];
                    caller.Reply($" > {LangHelper.ElementName(element)}", ElementColors.GetColor(element));
                }

                caller.Reply($"{npc.TypeName} melee attack:");
                for (int i = 0; i < contactTypes.Length; i++)
                {
                    Element element = contactTypes[i];
                    caller.Reply($" > {LangHelper.ElementName(element)}", ElementColors.GetColor(element));
                }

                if (_primaryAbility != AbilityID.None) caller.Reply($"  Primary Ability: {LangHelper.AbilityName(_primaryAbility)}");
                if (_secondaryAbility != AbilityID.None) caller.Reply($"  Secondary Ability: {LangHelper.AbilityName(_secondaryAbility)}");
                if (_hiddenAbility != AbilityID.None) caller.Reply($"  Hidden Ability: {LangHelper.AbilityName(_hiddenAbility)}");

                IOrderedEnumerable<KeyValuePair<float, List<Element>>> orderedEnumerable = resistancesToTypes.OrderBy((kvp) => kvp.Value);
                foreach (KeyValuePair<float, List<Element>> kvp in orderedEnumerable)
                {
                    caller.Reply($" [{kvp.Key}x]: {kvp.Value}");
                }
                
                //if (superSusceptibleTo.Count > 0)
                //    caller.Reply($" {npc.TypeName} is very susceptible to: {string.Join(", ", superSusceptibleTo)}", superEffectiveColor);
                //if (susceptibleTo.Count > 0)
                //    caller.Reply($" {npc.TypeName} is susceptible to: {string.Join(", ", susceptibleTo)}", effectiveColor);
                //if (neutralTo.Count > 0)
                //    caller.Reply($" {npc.TypeName} is neutral to: {string.Join(", ", neutralTo)}", neutralColor);
                //if (resistantTo.Count > 0)
                //    caller.Reply($" {npc.TypeName} is resistant to: {string.Join(", ", resistantTo)}", ineffectiveColor);
                //if (superResistantTo.Count > 0)
                //    caller.Reply($" {npc.TypeName} is very resistant to: {string.Join(", ", superResistantTo)}", superIneffectiveColor);
                //if (immuneTo.Count > 0)
                //    caller.Reply($" {npc.TypeName} is immune to: {string.Join(", ", immuneTo)}", immuneColor);

                return true;
            }
            else
            {
                caller.Reply($"Found {chosenIndexes.Count} NPC results for search parameter {str}.");
                return true;
            }
        }
    }
}
