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
using TerraTyping.Common.UI;

namespace TerraTyping.Common
{
    public class MySystem : ModSystem
    {
        private bool iconsLoaded;
        private Asset<Texture2D>[] icons;
        private UserInterface wikiUserInterface;

        public override void Load()
        {
            //wikiUserInterface = new UserInterface();
            //wikiUserInterface.SetState(new WikiUIState());
            LoadIcons();
        }

        public override void Unload()
        {
            //icons = null;
            //wikiUserInterface = null;
            UnloadIcons();
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int healthBarLayerIndex = layers.FindIndex((layer) => layer.Name.Equals("Vanilla: Entity Health Bars"));
            if (healthBarLayerIndex != -1)
            {
                layers.Insert(healthBarLayerIndex, new LegacyGameInterfaceLayer("TerraTyping: Types UI", DrawTypes, InterfaceScaleType.Game));
            }

            int inventoryLayerIndex = layers.FindIndex((layer) => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryLayerIndex != -1)
            {
                //layers.Insert(inventoryLayerIndex, new LegacyGameInterfaceLayer("TerraTyping: Wiki UI", Wiki, InterfaceScaleType.UI));
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (wikiUserInterface?.CurrentState is not null)
            {
                wikiUserInterface.Update(gameTime);
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
                    Rectangle npcFrame = new Rectangle((int)npc.Bottom.X - npc.frame.Width / 2,
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
                            float calculatedContactIconY = topLeft.Y + (icon.Height + buffer) * Main.UIScale;
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

        private bool Wiki()
        {
            if (wikiUserInterface?.CurrentState is not null)
            {
                wikiUserInterface.Draw(Main.spriteBatch, new GameTime());
            }

            return true;
        }

        private void LoadIcons()
        {
            try
            {
                if (iconsLoaded)
                {
                    return;
                }

                Element[] elements = ElementHelper.GetAllIncludeNone();

                icons = new Asset<Texture2D>[elements.Length];

                for (int i = 0; i < elements.Length; i++)
                {
                    //icons[i] = ModContent.Request<Texture2D>($"TerraTyping/Types/{LangHelper.ElementName(elements[i])}");
                }

                iconsLoaded = true;
            }
            catch (Exception e)
            {
                TerraTyping.Instance.Logger.Error("Caught exception while loading icons.", e);
            }
        }

        private void UnloadIcons()
        {
            icons = null;
            iconsLoaded = false;
        }
    }
}
