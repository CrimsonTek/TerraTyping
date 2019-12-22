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
using Terraria.GameInput;

namespace TerraTyping
{
	class TerraTyping : Mod
    {
        static List<string> errors = new List<string>();

        public TerraTyping()
        {

        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            PlayerInput.SetZoom_Unscaled();
            PlayerInput.SetZoom_MouseInWorld();
            Microsoft.Xna.Framework.Rectangle rectangle1 = new Microsoft.Xna.Framework.Rectangle((int)((double)Main.mouseX + (double)Main.screenPosition.X), (int)((double)Main.mouseY + (double)Main.screenPosition.Y), 1, 1);
            if ((double)Main.player[Main.myPlayer].gravDir == -1.0)
                rectangle1.Y = (int)Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
            PlayerInput.SetZoom_UI();
            IngameOptions.MouseOver();
            IngameFancyUI.MouseOver();

            for (int index1 = 0; index1 < 200; ++index1)
            {
                if (Main.npc[index1].active)
                {
                    Rectangle rectangle2 = new Rectangle((int)Main.npc[index1].Bottom.X - Main.npc[index1].frame.Width / 2,
                                                            (int)Main.npc[index1].Bottom.Y - Main.npc[index1].frame.Height,
                                                            Main.npc[index1].frame.Width,
                                                            Main.npc[index1].frame.Height);
                    if (Main.npc[index1].type >= 87 && Main.npc[index1].type <= 92)
                        rectangle2 = new Rectangle((int)((double)Main.npc[index1].position.X + (double)Main.npc[index1].width * 0.5 - 32.0),
                                                    (int)((double)Main.npc[index1].position.Y + (double)Main.npc[index1].height * 0.5 - 32.0),
                                                    64,
                                                    64);
                    bool flag1 = rectangle1.Intersects(rectangle2);
                    bool flag2 = flag1 || Main.SmartInteractShowingGenuine && Main.SmartInteractNPC == index1;
                    if (flag2 && (
                        Main.npc[index1].type != 85 && 
                        Main.npc[index1].type != 341 && 
                        Main.npc[index1].aiStyle != 87 || 
                        Main.npc[index1].ai[0] != 0.0) && 
                        Main.npc[index1].type != 488)
                    {
                        if (flag1)
                        {
                            ElementHelper elementHelper = new ElementHelper();

                            float buffer = 4 * Main.UIScale;
                            float prev = 0;

                            Element Primary = elementHelper.Primary(Main.npc[index1]);
                            Element Secondary = elementHelper.Secondary(Main.npc[index1]);
                            Element Tertiary = elementHelper.Tertiary(Main.npc[index1]);
                            Element Quatrinary = elementHelper.Quatrinary(Main.npc[index1]);

                            var icon1 = GetTexture("Types/" + Formal.Name[Primary]);
                            var icon2 = GetTexture("Types/" + Formal.Name[Secondary]);
                            var icon3 = GetTexture("Types/" + Formal.Name[Tertiary]);
                            var icon4 = GetTexture("Types/" + Formal.Name[Quatrinary]);

                            int yOffset = 38;
                            int xOffset = 12;
                            Main.spriteBatch.Begin();
                            int x = (int)((Main.mouseX + xOffset) * Main.UIScale);
                            int y = (int)((Main.mouseY + yOffset) * Main.UIScale);
                            if (Primary != Element.none && Primary != Element.levitate)
                            {
                                Main.spriteBatch.Draw(icon1, new Vector2(x + prev, y), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
                                prev += icon1.Width * Main.UIScale + buffer;
                            }
                            if (Secondary != Element.none && Secondary != Element.levitate)
                            {
                                Main.spriteBatch.Draw(icon2, new Vector2(x + prev, y), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
                                prev += icon2.Width * Main.UIScale + buffer;
                            }
                            if (Tertiary != Element.none && Tertiary != Element.levitate)
                            {
                                Main.spriteBatch.Draw(icon3, new Vector2(Main.mouseX + prev, Main.mouseY), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
                            }
                            if (Quatrinary != Element.none && Quatrinary != Element.levitate)
                            {
                                Main.spriteBatch.Draw(icon4, new Vector2(x, y + icon1.Width * Main.UIScale + buffer), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
                            }
                            Main.spriteBatch.End();
                            break;
                        }
                        break;
                    }
                }
            }

            //PlayerInput.SetZoom_UI();
            base.ModifyInterfaceLayers(layers);
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

        public override void PostUpdateEverything()
        {
            if (Main.ActivePlayersCount > 0 && errors.Count > 0)
            {
                for (int index = 0; index < errors.Count; index++)
                {
                    if (Config.DevMode)
                    {
                        Main.NewText("[TerraTyping] " + errors[index], 255, 0, 0, true);
                    }
                    errors.RemoveAt(index);
                }
            }
        }
    }
}
