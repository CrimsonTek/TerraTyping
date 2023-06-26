using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Core;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;
using TerraTyping.TypeLoaders;

namespace TerraTyping.Common.Commands
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
                Color color = TerraTypingColors.GetColor(result);
                string elementNameUpperFirst = LangHelper.ElementName(result, true);
                string elementNameLowerFirst = LangHelper.ElementName(result, false);

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

                foreach (Element element in ElementHelper.GetAll(false))
                {
                    float eff = Table.EffectivenessUnscaled(result, element);
                    switch (eff)
                    {
                        case 0f:
                            nothingAgainst.Add(LangHelper.ElementName(element, true));
                            break;
                        case 0.5f:
                            weakAgainst.Add(LangHelper.ElementName(element, true));
                            break;
                        case 1f:
                            neutralAgainst.Add(LangHelper.ElementName(element, true));
                            break;
                        case 2:
                            strongAgainst.Add(LangHelper.ElementName(element, true));
                            break;
                        default:
                            throw new Exception($"Unexpected effectiveness case: {eff}.");
                    }
                }

                foreach (Element element in ElementHelper.GetAll(false))
                {
                    float eff = Table.EffectivenessUnscaled(element, result);
                    switch (eff)
                    {
                        case 0f:
                            immuneTo.Add(LangHelper.ElementName(element, true));
                            break;
                        case 0.5f:
                            resistantTo.Add(LangHelper.ElementName(element, true));
                            break;
                        case 1f:
                            neutralTo.Add(LangHelper.ElementName(element, true));
                            break;
                        case 2:
                            susceptibleTo.Add(LangHelper.ElementName(element, true));
                            break;
                        default:
                            throw new Exception($"Unexpected effectiveness case: {eff}.");
                    }
                }

                caller.Reply($"{elementNameUpperFirst} Defensive Stats:", color);
                caller.Reply($" > Effective against {elementNameLowerFirst}: {string.Join(", ", susceptibleTo)}", effectiveColor);
                caller.Reply($" > Neutral against {elementNameLowerFirst}: {string.Join(", ", neutralTo)}", neutralColor);
                caller.Reply($" > Ineffective against {elementNameLowerFirst}: {string.Join(", ", resistantTo)}", ineffectiveColor);
                if (immuneTo.Count > 0) caller.Reply($"   > No effect against {elementNameLowerFirst}: {string.Join(", ", immuneTo)}", immuneColor);

                caller.Reply($"{elementNameUpperFirst} Offensive Stats:", color);
                caller.Reply($" > {elementNameUpperFirst} effective against: {string.Join(", ", strongAgainst)}", effectiveColor);
                caller.Reply($" > {elementNameUpperFirst} neutral against: {string.Join(", ", neutralAgainst)}", neutralColor);
                caller.Reply($" > {elementNameUpperFirst} ineffective against: {string.Join(", ", weakAgainst)}", ineffectiveColor);
                if (nothingAgainst.Count > 0) caller.Reply($" > {elementNameUpperFirst} no effect against: {string.Join(", ", nothingAgainst)}", immuneColor);

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

                ElementArray defensiveTypes = NPCTypeLoader.GetDefensiveElements(npc);
                ElementArray contactTypes = NPCTypeLoader.GetOffensiveElements(npc);
                var resistancesToTypes = new Dictionary<float, List<Element>>();

                foreach (Element element in ElementUtils.GetAll(false))
                {
                    // element here is attacker
                    float damage = defensiveTypes.DamageFrom(element, true);
                    resistancesToTypes.AddIfMissingAndGet(damage, new List<Element>()).Add(element);
                }

                caller.Reply($"{npc.TypeName} typing:");
                foreach (Element element in defensiveTypes)
                {
                    caller.Reply($" > {LangHelper.ElementName(element, true)}", TerraTypingColors.GetColor(element));
                }

                caller.Reply($"{npc.TypeName} melee attack:");
                foreach (Element element in contactTypes)
                {
                    caller.Reply($" > {LangHelper.ElementName(element, true)}", TerraTypingColors.GetColor(element));
                }

                foreach (Ability basicAbility in abilities.BasicAbilities)
                {
                    caller.Reply($"  Basic Ability: {LangHelper.AbilityName(basicAbility, true)}");
                }

                foreach (Ability hiddenAbility in abilities.HiddenAbilities)
                {
                    caller.Reply($"  Hidden Ability: {LangHelper.AbilityName(hiddenAbility, true)}");
                }

                IOrderedEnumerable<KeyValuePair<float, List<Element>>> orderedEnumerable = resistancesToTypes.OrderBy((kvp) => kvp.Value);
                foreach (KeyValuePair<float, List<Element>> kvp in orderedEnumerable)
                {
                    caller.Reply($" [{kvp.Key}x]: {kvp.Value}");
                }

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
