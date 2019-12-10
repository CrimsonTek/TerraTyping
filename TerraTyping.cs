using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using TerraTyping;
using Newtonsoft.Json;
using Terraria.ModLoader.IO;

namespace TerraTyping
{
	class TerraTyping : Mod
    {
        static List<string> errors = new List<string>();

        public TerraTyping()
        {

        }

        public override void MidUpdateNPCGore()
        {
            //if (Main.ActivePlayersCount == 0)
            //    return;

            //foreach (NPC npc in Main.npc)
            //{
            //    //if (!(Main.HoveringOverAnNPC && Main.hideUI))
            //    //    return;

            //    ElementHelper elementHelper = new ElementHelper();

            //    int buffer = 4;
            //    int prev = 12;

            //    Element Primary = elementHelper.Primary(npc);
            //    Element Secondary = elementHelper.Secondary(npc);
            //    Element Tertiary = elementHelper.Tertiary(npc);
            //    Element Quatrinary = elementHelper.Quatrinary(npc);

            //    var icon1 = GetTexture("Types/" + Formal.Name[Primary]);
            //    var icon2 = GetTexture("Types/" + Formal.Name[Secondary]);
            //    var icon3 = GetTexture("Types/" + Formal.Name[Tertiary]);
            //    var icon4 = GetTexture("Types/" + Formal.Name[Quatrinary]);
            //    //if (npc.Hitbox.Contains(Main.MouseWorld.ToPoint()))
            //    {
            //        Main.spriteBatch.Begin();
            //        Main.spriteBatch.Draw(icon1, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
            //        //Main.spriteBatch.Draw(icon1, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
            //        //Main.spriteBatch.DrawString(Main.fontMouseText, npc.FullName, Main.MouseScreen, new Color(new Vector3(1, 1, 1)));
            //        Main.spriteBatch.End();
            //    }
            //    //if (Math.Abs(npc.position.X + npc.width / 2 - Main.screenPosition.X - Main.mouseX) <= npc.width && Math.Abs(npc.position.Y + npc.height / 2 - Main.screenPosition.Y - Main.mouseY) <= npc.height)
            //    //{
            //    //    Main.spriteBatch.Begin();
            //    //    if (Enemies.Type[npc.type].Item1 != Element.none && Enemies.Type[npc.type].Item1 != Element.levitate)
            //    //    {
            //    //        Main.spriteBatch.Draw(icon1, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
            //    //        prev += icon1.Width + buffer;
            //    //    }
            //    //    if (Enemies.Type[npc.type].Item2 != Element.none && Enemies.Type[npc.type].Item2 != Element.levitate)
            //    //    {
            //    //        Main.spriteBatch.Draw(icon2, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
            //    //        prev += icon2.Width + buffer;
            //    //    }
            //    //    if (Enemies.Type[npc.type].Item3 != Element.none && Enemies.Type[npc.type].Item3 != Element.levitate)
            //    //    {
            //    //        Main.spriteBatch.Draw(icon3, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
            //    //    }
            //    //    if (Enemies.Type[npc.type].Item4 != Element.none && Enemies.Type[npc.type].Item4 != Element.levitate)
            //    //    {
            //    //        Main.spriteBatch.Draw(icon4, new Vector2(Main.mouseX + 12, Main.mouseY + 38 + icon1.Width + buffer), Color.White);
            //    //    }
            //    //    Main.spriteBatch.End();
            //    //}
            //}
        }

        public override void Load()
        {
            errors.Clear();
        }

        public override void PostSetupContent()
        {
            /* armor */ {
                foreach (KeyValuePair<int, Tuple<Element, Element>> entry in Armors.Helmet)
                {
                    int key = entry.Key;
                    Tuple<Element, Element> value = entry.Value;
                    if (Armors.Type.ContainsKey(key))
                    {
                        //errors.Add(string.Format("The key {0} [Internal Index: {1}] has already been added. Likely a spelling error.", entry.Key, key));
                        return;
                    }
                    Armors.Type.Add(key, value);
                }
                foreach (KeyValuePair<int, Tuple<Element, Element>> entry in Armors.Chest)
                {
                    int key = entry.Key;
                    Tuple<Element, Element> value = entry.Value;
                    if (Armors.Type.ContainsKey(key))
                    {
                        //errors.Add(string.Format("The key {0} [Internal Index: {1}] has already been added. Likely a spelling error.", entry.Key, key));
                        return;
                    }
                    Armors.Type.Add(key, value);
                }
                foreach (KeyValuePair<int, Tuple<Element, Element>> entry in Armors.Leggings)
                {
                    int key = entry.Key;
                    Tuple<Element, Element> value = entry.Value;
                    if (Armors.Type.ContainsKey(key))
                    {
                        //errors.Add(string.Format("The key {0} [Internal Index: {1}] has already been added. Likely a spelling error.", entry.Key, key));
                        return;
                    }
                    Armors.Type.Add(key, value);
                }
            }

            Mod weaponOut = ModLoader.GetMod("WeaponOut");
            if (weaponOut != null)
            {
                Initialize(weaponOut, ref WeaponOutAmmos._Type, ref WeaponOutAmmos.Type);
                Initialize(weaponOut, ref WeaponOutArmors._Helmet, ref WeaponOutArmors.Helmet, ref WeaponOutArmors.Type);
                Initialize(weaponOut, ref WeaponOutArmors._Chest, ref WeaponOutArmors.Chest, ref WeaponOutArmors.Type);
                Initialize(weaponOut, ref WeaponOutArmors._Leggings, ref WeaponOutArmors.Leggings, ref WeaponOutArmors.Type);
                Initialize(weaponOut, ref WeaponOutEnemies._Type, ref WeaponOutEnemies.Type);
                Initialize(weaponOut, ref WeaponOutItems._Type, ref WeaponOutItems.Type);
                Initialize(weaponOut, ref WeaponOutProjectiles._Type, ref WeaponOutProjectiles.Type, true);
            }
        }

        public override void Unload()
        {
            /* WeaponOut */ {
                WeaponOutAmmos.Type.Clear();
                WeaponOutArmors.Type.Clear();
                WeaponOutArmors.Helmet.Clear();
                WeaponOutArmors.Chest.Clear();
                WeaponOutArmors.Leggings.Clear();
                WeaponOutEnemies.Type.Clear();
                WeaponOutItems.Type.Clear();
                WeaponOutProjectiles.Type.Clear(); 
            }
        }

        private void Initialize(Mod mod, ref Dictionary<string, Element> _dict, ref Dictionary<int, Element> dict, bool projectile = false)
        {
            if (mod == null)
                return;

            foreach (KeyValuePair<string, Element> entry in _dict)
            {
                int key = 0;
                if (projectile)
                    key = mod.ProjectileType(entry.Key);
                else
                    key = mod.ItemType(entry.Key);

                if (key == 0)
                {
                    errors.Add(string.Format("[Mod: {0}] {1} [Internal Index: {2}] may not exist. Consider looking into this. Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                Element value = entry.Value;
                if (value == Element.none)
                {
                    errors.Add(string.Format("[Mod: {0}] {1} [Internal Index: {2}] doesn't have a type. Why? Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                if (dict.ContainsKey(key))
                {
                    errors.Add(string.Format("[Mod: {0}] The key {1} [Internal Index: {2}] has already been added. Likely a spelling error. Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                dict.Add(key, value);
            }
        }

        private void Initialize(Mod mod, ref Dictionary<string, Tuple<Element, Element>> _dict, ref Dictionary<int, Tuple<Element, Element>> dict, ref Dictionary<int, Tuple<Element, Element>> dict2)
        {
            if (mod == null)
                return;

            foreach (KeyValuePair<string, Tuple<Element, Element>> entry in _dict)
            {
                int key = mod.ItemType(entry.Key);
                if (key == 0)
                {
                    errors.Add(string.Format("[Mod: {0}] {1} [Internal Index: {2}] may not exist. Consider looking into this. Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                Tuple<Element, Element> value = entry.Value;
                if (value.Item1 == Element.none)
                {
                    errors.Add(string.Format("[Mod: {0}] {1} [Internal Index: {2}] doesn't have a type. Why? Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, value);
                }
                else
                {
                    errors.Add(string.Format("[Mod: {0}] The key {1} [Internal Index: {2}] has already been added. Likely a spelling error. Dict: {3}", mod, entry.Key, key, dict.ToString()));
                }
                if (!dict2.ContainsKey(key))
                {
                    dict2.Add(key, value);
                }
                else
                {
                    errors.Add(string.Format("[Mod: {0}] The key {1} [Internal Index: {2}] has already been added. Likely a spelling error. Dict: {3}", mod, entry.Key, key, dict2.ToString()));
                }
            }
        }

        private void Initialize(Mod mod, ref Dictionary<string, Tuple<Element, Element, Element, Element>> _dict, ref Dictionary<int, Tuple<Element, Element, Element, Element>> dict)
        {
            if (mod == null)
                return;

            foreach (KeyValuePair<string, Tuple<Element, Element, Element, Element>> entry in _dict)
            {
                int key = mod.NPCType(entry.Key);
                if (key == 0)
                {
                    errors.Add(string.Format("[Mod: {0}] {1} [Internal Index: {2}] may not exist. Consider looking into this. Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                Tuple<Element, Element, Element, Element> value = entry.Value;
                if (value.Item1 == Element.none)
                {
                    errors.Add(string.Format("[Mod: {0}] {1} [Internal Index: {2}] doesn't have a type. Why? Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                if (dict.ContainsKey(key))
                {
                    errors.Add(string.Format("[Mod: {0}] The key {1} [Internal Index: {2}] has already been added. Likely a spelling error. Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                dict.Add(key, value);
            }
        }

        //public override void PostUpdateEverything()
        //{
        //    if (Main.ActivePlayersCount > 0 && errors.Count > 0)
        //    {
        //        for (int index = 0; index < errors.Count; index++)
        //        {
        //            Main.NewText("[TerraTyping] " + errors[index], 255, 0, 0, true);
        //            errors.RemoveAt(index);
        //        }
        //    }
        //}
    }
}
