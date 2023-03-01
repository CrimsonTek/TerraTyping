using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;
using TerraTyping.DataTypes;
using Terraria.ID;
using ReLogic.Content;
using TerraTyping.Helpers;

namespace TerraTyping
{
    public class MySystem : ModSystem
    {
        Asset<Texture2D>[] icons;

        bool iconsLoaded;

        void LoadIcons()
        {
            if (iconsLoaded)
            {
                return;
            }

            Element[] elements = ElementHelper.GetAllIncludeNone();
            
            icons = new Asset<Texture2D>[elements.Length];

            for (int i = 0; i < (elements.Length); i++)
            {
                icons[i] = ModContent.Request<Texture2D>($"TerraTyping/Types/{Formal.Name[elements[i]]}");
            }

            iconsLoaded = true;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex((layer) => layer.Name.Equals("Vanilla: Entity Health Bars"));
            if (index != -1)
            {
                layers.Insert(index, new LegacyGameInterfaceLayer("TerraTyping: Types UI", DrawTypes, InterfaceScaleType.Game));
            }
        }

        private bool DrawTypes()
        {
            //PlayerInput.SetZoom_Unscaled();
            //PlayerInput.SetZoom_MouseInWorld();

            Rectangle mouseRect = new Rectangle((int)(Main.mouseX + (double)Main.screenPosition.X), (int)(Main.mouseY + (double)Main.screenPosition.Y), 1, 1);
            if (Main.player[Main.myPlayer].gravDir == -1.0f)
            {
                mouseRect.Y = (int)Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
            }

            //PlayerInput.SetZoom_UI();
            //IngameOptions.MouseOver();
            //IngameFancyUI.MouseOver();

            if (!iconsLoaded)
            {
                LoadIcons();
            }

            for (int index = 0; index < Main.maxNPCs; ++index)
            {
                NPC npc = Main.npc[index];
                if (npc.active)
                {
                    Rectangle npcFrame = new Rectangle((int)npc.Bottom.X - (npc.frame.Width / 2),
                        (int)npc.Bottom.Y - npc.frame.Height, npc.frame.Width, npc.frame.Height);
                    if (npc.type >= NPCID.WyvernHead && npc.type <= NPCID.WyvernTail)
                    {
                        npcFrame = new Rectangle((int)(npc.position.X + npc.width * 0.5 - 32.0), (int)(npc.position.Y + npc.height * 0.5 - 32.0), 64, 64);
                    }
                    bool hovering = mouseRect.Intersects(npcFrame) || Main.SmartInteractShowingGenuine && Main.SmartInteractNPC == index;
                    if (hovering && npc.type != NPCID.TargetDummy
                        && !((npc.type == NPCID.Mimic || npc.type == NPCID.PresentMimic || npc.aiStyle == NPCAIStyleID.BiomeMimic) && npc.ai[0] == 0.0))
                    {
                        const float buffer = 6;

                        NPCWrapper npcWrapper = NPCWrapper.GetWrapper(npc);

                        Vector2 topLeft = Main.MouseScreen + new Vector2(23, 48);
                        Vector2 defensiveIconsVector = topLeft;
                        Vector2 contactIconVector = topLeft;

                        for (int i = 0; i < npcWrapper.DefensiveElements.Length; i++)
                        {
                            Texture2D icon = icons[(int)npcWrapper.DefensiveElements[i]].Value;
                            Main.spriteBatch.Draw(icon, defensiveIconsVector, null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
                            defensiveIconsVector.X += (icon.Width + buffer) * Main.UIScale;
                            float calculatedContactIconY = topLeft.Y + ((icon.Height + buffer) * Main.UIScale);
                            if (calculatedContactIconY > contactIconVector.Y)
                            {
                                contactIconVector.Y = calculatedContactIconY;
                            }
                        }

                        if (npc.damage > 0)
                        {
                            for (int i = 0; i < npcWrapper.OffensiveElements.Length; i++)
                            {
                                Texture2D icon = icons[(int)npcWrapper.OffensiveElements[i]].Value;
                                Main.spriteBatch.Draw(icon, contactIconVector, null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
                                contactIconVector.X += (icon.Width + buffer) * Main.UIScale;
                            }
                        }
                        break;
                    }
                }
            }

            return true;
            //PlayerInput.SetZoom_UI();
        }
        //[Obsolete]
        //private void DrawTypesOld()
        //{
        //    PlayerInput.SetZoom_Unscaled();
        //    PlayerInput.SetZoom_MouseInWorld();
        //    Rectangle rectangle1 = new Rectangle((int)((double)Main.mouseX + (double)Main.screenPosition.X), (int)((double)Main.mouseY + (double)Main.screenPosition.Y), 1, 1);
        //    if ((double)Main.player[Main.myPlayer].gravDir == -1.0)
        //        rectangle1.Y = (int)Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
        //    PlayerInput.SetZoom_UI();
        //    IngameOptions.MouseOver();
        //    IngameFancyUI.MouseOver();

        //    if (!iconsLoaded)
        //    {
        //        LoadIcons();
        //    }

        //    for (int index1 = 0; index1 < 200; ++index1)
        //    {
        //        if (Main.npc[index1].active)
        //        {
        //            Rectangle rectangle2 = new Rectangle((int)Main.npc[index1].Bottom.X - Main.npc[index1].frame.Width / 2,
        //                                                    (int)Main.npc[index1].Bottom.Y - Main.npc[index1].frame.Height,
        //                                                    Main.npc[index1].frame.Width,
        //                                                    Main.npc[index1].frame.Height);
        //            if (Main.npc[index1].type >= NPCID.WyvernHead && Main.npc[index1].type <= NPCID.WyvernTail)
        //                rectangle2 = new Rectangle((int)((double)Main.npc[index1].position.X + (double)Main.npc[index1].width * 0.5 - 32.0),
        //                                            (int)((double)Main.npc[index1].position.Y + (double)Main.npc[index1].height * 0.5 - 32.0),
        //                                            64,
        //                                            64);
        //            bool flag1 = rectangle1.Intersects(rectangle2);
        //            bool flag2 = flag1 || Main.SmartInteractShowingGenuine && Main.SmartInteractNPC == index1;
        //            if (flag2 && (
        //                Main.npc[index1].type != NPCID.Mimic &&
        //                Main.npc[index1].type != NPCID.PresentMimic &&
        //                Main.npc[index1].aiStyle != 87 ||
        //                Main.npc[index1].ai[0] != 0.0) &&
        //                Main.npc[index1].type != NPCID.TargetDummy)
        //            {
        //                if (flag1)
        //                {
        //                    float buffer = 4 * Main.UIScale;

        //                    NPCWrapper npcWrapper = NPCWrapper.GetWrapper(Main.npc[index1]);
        //                    Element primary = npcWrapper.Primary;
        //                    Element offensive = npcWrapper.Offensive;

        //                    Texture2D icon1 = icons[(int)primary].Value;
        //                    Texture2D icon4 = icons[(int)offensive].Value;

        //                    int yOffset = 38;
        //                    int xOffset = 12;
        //                    Main.spriteBatch.Begin();
        //                    int startX = (int)((Main.mouseX + xOffset) * Main.UIScale);
        //                    int startY = (int)((Main.mouseY + yOffset) * Main.UIScale);

        //                    float x = startX;
        //                    float contactIconsY = startY;
        //                    for (int i = 0; i < npcWrapper.DefensiveElements.Length; i++)
        //                    {
        //                        Texture2D icon = icons[(int)npcWrapper.DefensiveElements[i]].Value;
        //                        Main.spriteBatch.Draw(icon, new Vector2(x, startY), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
                                
        //                        x += (icon.Width + buffer) * Main.UIScale;
        //                        float bottomPlusBuffer = startY + (icon.Height + buffer) * Main.UIScale;
        //                        if (bottomPlusBuffer > contactIconsY)
        //                        {
        //                            contactIconsY = bottomPlusBuffer;
        //                        }
        //                    }

        //                    x = startX;
        //                    for (int i = 0; i < npcWrapper.ContactElements.Length; i++)
        //                    {
        //                        Texture2D icon = icons[(int)npcWrapper.ContactElements[i]].Value;
        //                        Main.spriteBatch.Draw(icon, new Vector2(x, contactIconsY), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);

        //                        x += (icon.Width + buffer) * Main.UIScale;
        //                    }

        //                    if (offensive != Element.none)
        //                    {
        //                        Main.spriteBatch.Draw(icon4, new Vector2(startX, startY + icon1.Width * Main.UIScale + buffer), null, Color.White, 0, new Vector2(0, 0), Main.UIScale, SpriteEffects.None, 0);
        //                    }
        //                    Main.spriteBatch.End();
        //                    break;
        //                }
        //                break;
        //            }
        //        }
        //    }

        //    //PlayerInput.SetZoom_UI();
        //}
    }
}
