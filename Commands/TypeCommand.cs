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
                Tuple<int, int, int> tuple = Colors.Type[result];
                Color color = new Color(tuple.Item1, tuple.Item2, tuple.Item3);
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

                int defenseCount = Table.Effectiveness.GetLength(0);
                for (int i = 0; i < defenseCount; i++)
                {
                    float e = Table.Effectiveness[(int)result, i];
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

                int attackCount = Table.Effectiveness.GetLength(1);
                for (int i = 0; i < attackCount; i++)
                {
                    float e = Table.Effectiveness[i, (int)result];
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
            for (int i = -65; i < NPCLoader.NPCCount; i++)
            {
                npc.SetDefaults(i);
                if (npc.TypeName.ToLower() == str.ToLower())
                {
                    chosenIndexes.Clear();
                    chosenIndexes.Add(i);
                    break;
                }
                else if (npc.TypeName.ToLower().Contains(str.ToLower()))
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
                NPCTypeInfo typeInfo = Enemies.Type[chosenIndexes[0]];
                Element primary = typeInfo.Primary;
                Element secondary = typeInfo.Secondary;
                Element offensive = typeInfo.Offensive;
                AbilityID primaryAbility = typeInfo.Container.PrimaryAbility;
                AbilityID secondaryAbility = typeInfo.Container.SecondaryAbility;
                AbilityID hiddenAbility = typeInfo.Container.HiddenAbility;

                // element is defender
                List<string> superSusceptibleTo = new List<string>();
                List<string> susceptibleTo = new List<string>();
                List<string> neutralTo = new List<string>();
                List<string> resistantTo = new List<string>();
                List<string> superResistantTo = new List<string>();
                List<string> immuneTo = new List<string>();

                int attackCount = Table.Effectiveness.GetLength(1);
                for (int i = 0; i < attackCount; i++)
                {
                    float e = Table.Effectiveness[i, (int)primary] * Table.Effectiveness[i, (int)secondary];

                    if (e == 0f)
                        immuneTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == Table.Divi * Table.Divi)
                        superResistantTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == Table.Divi)
                        resistantTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == 1f || e == Table.Mult * Table.Divi)
                        neutralTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == Table.Mult)
                        susceptibleTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else if (e == Table.Mult * Table.Mult)
                        superSusceptibleTo.Add(LangHelper.ElementName(ElementHelper.FromIndex(i)));
                    else
                        throw new Exception("Unexpected effectiveness case.");
                }

                caller.Reply($"{npc.TypeName} typing:");

                if (true)
                    caller.Reply($" > {LangHelper.ElementName(primary)}", Colors.type[primary]);
                if (secondary != Element.none)
                    caller.Reply($" > {LangHelper.ElementName(secondary)}", Colors.type[secondary]);

                caller.Reply($"{npc.TypeName} melee attack:");
                if (true)
                    caller.Reply($" > {LangHelper.ElementName(offensive)}", Colors.type[offensive]);

                if (primaryAbility != AbilityID.None) caller.Reply($"  Primary Ability: {LangHelper.AbilityName(primaryAbility)}");
                if (secondaryAbility != AbilityID.None) caller.Reply($"  Secondary Ability: {LangHelper.AbilityName(secondaryAbility)}");
                if (hiddenAbility != AbilityID.None) caller.Reply($"  Hidden Ability: {LangHelper.AbilityName(hiddenAbility)}");

                if (superSusceptibleTo.Count > 0)
                    caller.Reply($" {npc.TypeName} is very susceptible to: {string.Join(", ", superSusceptibleTo)}", superEffectiveColor);
                if (susceptibleTo.Count > 0)
                    caller.Reply($" {npc.TypeName} is susceptible to: {string.Join(", ", susceptibleTo)}", effectiveColor);
                if (neutralTo.Count > 0)
                    caller.Reply($" {npc.TypeName} is neutral to: {string.Join(", ", neutralTo)}", neutralColor);
                if (resistantTo.Count > 0)
                    caller.Reply($" {npc.TypeName} is resistant to: {string.Join(", ", resistantTo)}", ineffectiveColor);
                if (superResistantTo.Count > 0)
                    caller.Reply($" {npc.TypeName} is very resistant to: {string.Join(", ", superResistantTo)}", superIneffectiveColor);
                if (immuneTo.Count > 0)
                    caller.Reply($" {npc.TypeName} is immune to: {string.Join(", ", immuneTo)}", immuneColor);

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
