using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.GameInput;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.Abilities.Buffs.TypeAdd;
using TerraTyping.Abilities.Buffs.TypeReplace;
using System.Reflection;
using TerraTyping.Attributes;
using TerraTyping.Abilities.Buffs;
using TerraTyping.Helpers;
using TerraTyping.Dictionaries;
using TerraTyping.TypeLoaders;
using log4net;

namespace TerraTyping
{
    class TerraTyping : Mod
    {
        static readonly List<string> errors = new List<string>();

        public static TerraTyping Instance { get; private set; }

        //public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)/* tModPorter Note: Removed. Use ModSystem.ModifyInterfaceLayers */
        //{
        //    PlayerInput.SetZoom_Unscaled();
        //    PlayerInput.SetZoom_MouseInWorld();
        //    Rectangle rectangle1 = new Rectangle((int)((double)Main.mouseX + (double)Main.screenPosition.X), (int)((double)Main.mouseY + (double)Main.screenPosition.Y), 1, 1);
        //    if ((double)Main.player[Main.myPlayer].gravDir == -1.0)
        //        rectangle1.Y = (int)Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
        //    PlayerInput.SetZoom_UI();
        //    IngameOptions.MouseOver();
        //    IngameFancyUI.MouseOver();

        //    for (int index1 = 0; index1 < 200; ++index1)
        //    {
        //        if (Main.npc[index1].active)
        //        {
        //            Rectangle rectangle2 = new Rectangle((int)Main.npc[index1].Bottom.X - Main.npc[index1].frame.Width / 2,
        //                                                    (int)Main.npc[index1].Bottom.Y - Main.npc[index1].frame.Height,
        //                                                    Main.npc[index1].frame.Width,
        //                                                    Main.npc[index1].frame.Height);
        //            if (Main.npc[index1].type >= 87 && Main.npc[index1].type <= 92)
        //                rectangle2 = new Rectangle((int)((double)Main.npc[index1].position.X + (double)Main.npc[index1].width * 0.5 - 32.0),
        //                                            (int)((double)Main.npc[index1].position.Y + (double)Main.npc[index1].height * 0.5 - 32.0),
        //                                            64,
        //                                            64);
        //            bool flag1 = rectangle1.Intersects(rectangle2);
        //            bool flag2 = flag1 || Main.SmartInteractShowingGenuine && Main.SmartInteractNPC == index1;
        //            if (flag2 && (
        //                Main.npc[index1].type != 85 && 
        //                Main.npc[index1].type != 341 && 
        //                Main.npc[index1].aiStyle != 87 || 
        //                Main.npc[index1].ai[0] != 0.0) && 
        //                Main.npc[index1].type != 488)
        //            {
        //                if (flag1)
        //                {
        //                    float buffer = 4 * Main.UIScale;
        //                    float prev = 0;

        //                    Element Primary = new NPCWrapper(Main.npc[index1]).Primary;
        //                    Element Secondary = new NPCWrapper(Main.npc[index1]).Secondary;
        //                    Element Quatrinary = new NPCWrapper(Main.npc[index1]).Offensive;

        //                    var icon1 = GetTexture("Types/" + Formal.Name[Primary]);
        //                    var icon2 = GetTexture("Types/" + Formal.Name[Secondary]);
        //                    var icon4 = GetTexture("Types/" + Formal.Name[Quatrinary]);

        //                    int yOffset = 38;
        //                    int xOffset = 12;
        //                    Main.spriteBatch.Begin();
        //                    int x = (int)((Main.mouseX + xOffset) * Main.UIScale);
        //                    int y = (int)((Main.mouseY + yOffset) * Main.UIScale);
        //                    if (Primary != Element.none && Primary != Element.levitate)
        //                    {
        //                        Main.spriteBatch.Draw(icon1, new Vector2(x + prev, y), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
        //                        prev += icon1.Width * Main.UIScale + buffer;
        //                    }
        //                    if (Secondary != Element.none && Secondary != Element.levitate)
        //                    {
        //                        Main.spriteBatch.Draw(icon2, new Vector2(x + prev, y), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
        //                    }
        //                    if (Quatrinary != Element.none && Quatrinary != Element.levitate)
        //                    {
        //                        Main.spriteBatch.Draw(icon4, new Vector2(x, y + icon1.Width * Main.UIScale + buffer), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
        //                    }
        //                    Main.spriteBatch.End();
        //                    break;
        //                }
        //                break;
        //            }
        //        }
        //    }

        //    //PlayerInput.SetZoom_UI();
        //    base.ModifyInterfaceLayers(layers);
        //}

        public override void Load()
        {
            try
            {
                Instance = this;

                ElementHelper.Load();
                ElementArray.Load();

                Type[] arrType = Code.GetTypes();
                foreach (Type type in arrType)
                {
                    LoadAttribute loadAttribute = type.GetCustomAttribute<LoadAttribute>();
                    if (loadAttribute != null)
                    {
                        MethodInfo methodInfo = type.GetMethod("Load", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        methodInfo.Invoke(null, null);
                    }
                }

                errors.Clear();
            }
            catch (Exception e)
            {
                Logger.Error("Caught exception while loading TerraTyping.", e);
            }
        }

        public override void PostSetupContent()
        {
            try
            {
                TypeLoader.Logger = Logger;
                TypeLoader.IsLoadingTypes = true;

                SpecialTooltip.StaticLoad();

                NPCTypeLoader.Instance.SetupTypes();
                WeaponTypeLoader.Instance.SetupTypes();
                AmmoTypeLoader.Instance.SetupTypes();
                ArmorTypeLoader.Instance.SetupTypes();
                ProjectileTypeLoader.Instance.SetupTypes();
                SpecialItemTypeLoader.Instance.SetupTypes();

                ProjectileWrapper.PostSetupContent();

                SpecialTooltip.Finish();

                ElementArray.Clean();

                TypeLoader.IsLoadingTypes = false;
            }
            catch (Exception e)
            {
                Logger.Error("Caught exception while loading TerraTyping.", e);
            }
        }

        public override void Unload()
        {
            try
            {
                Type[] arrType = Code.GetTypes();
                foreach (Type type in arrType)
                {
                    UnloadAttribute unloadAttribute = type.GetCustomAttribute<UnloadAttribute>();
                    if (unloadAttribute != null)
                    {
                        MethodInfo methodInfo = type.GetMethod("Unload", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        methodInfo.Invoke(null, null);
                    }
                }

                BuffUtils.addTypeBuffs = null;
                BuffUtils.replaceTypeBuffs = null;

                ProjectileWrapper.Unload();
                ElementArray.Unload();
                ElementHelper.Unload();
            }
            catch (Exception exception)
            {
                Logger.Error("An error occured during the unloading process.", exception);
            }

            SpecialTooltip.StaticUnload();
            Instance = null;
        }

        private void Initialize<T>(
            Mod mod,
            ref Dictionary<string, T> _dict,
            ref Dictionary<int, T> dict,
            Func<string, Mod, int> getKey)
        {
            if (mod == null)
                return;

            foreach (KeyValuePair<string, T> entry in _dict)
            {
                int key = getKey(entry.Key, mod);

                #region Error Reporting
                if (key == 0)
                {
                    errors.Add(string.Format("[Mod: {0}] {1} [Internal Index: {2}] may not exist. Consider looking into this. Dict: {3}", mod, entry.Key, key, dict.ToString()));
                    return;
                }
                T value = entry.Value;
                //if (value. == Element.none)
                //{
                //    errors.Add(string.Format("[Mod: {0}] {1} [Internal Index: {2}] doesn't have a type. Why? Dict: {3}", mod, entry.Key, key, dict.ToString()));
                //    return;
                //}
                #endregion

                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, value);
                }
                else
                {
                    errors.Add(string.Format("[Mod: {0}] The key {1} [Internal Index: {2}] has already been added. Likely a spelling error. Dict: {3}", mod, entry.Key, key, dict.ToString()));
                }
            }
        }

        [Obsolete] private void Initialize(Mod mod, ref Dictionary<string, Element> _dict, ref Dictionary<int, Element> dict, bool projectile = false)
        {
            if (mod == null)
                return;

            foreach (KeyValuePair<string, Element> entry in _dict)
            {
                int key = 0;
                if (projectile)
                    key = mod.Find<ModProjectile>(entry.Key).Type;
                else
                    key = mod.Find<ModItem>(entry.Key).Type;

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
        [Obsolete] private void Initialize(Mod mod, ref Dictionary<string, Tuple<Element, Element>> _dict, ref Dictionary<int, Tuple<Element, Element>> dict, ref Dictionary<int, Tuple<Element, Element>> dict2)
        {
            if (mod == null)
                return;

            foreach (KeyValuePair<string, Tuple<Element, Element>> entry in _dict)
            {
                int key = mod.Find<ModItem>(entry.Key).Type;
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
        [Obsolete] private void Initialize(Mod mod, ref Dictionary<string, Tuple<Element, Element, Element, Element>> _dict, ref Dictionary<int, Tuple<Element, Element, Element, Element>> dict)
        {
            if (mod == null)
                return;

            foreach (KeyValuePair<string, Tuple<Element, Element, Element, Element>> entry in _dict)
            {
                int key = mod.Find<ModNPC>(entry.Key).Type;
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
    }
}
