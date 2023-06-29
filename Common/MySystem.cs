using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TerraTyping.DataTypes;
using Terraria.ID;
using ReLogic.Content;
using TerraTyping.Helpers;
using TerraTyping.Common.UI;
using Terraria.Audio;
using TerraTyping.Core;
using TerraTyping.Common.Configs;

namespace TerraTyping.Common
{
    public class MySystem : ModSystem
    {
        private bool iconsLoaded;
        private Asset<Texture2D>[] icons;
        private WikiUIState wikiUIState;
        private UserInterface wikiUserInterface;
        private bool[] combatTextsToTrack;
        private int[] combatTextsTimeTracked;
        private ElementArray[] combatTextsTypes;

        public override void Load()
        {
            LoadIcons();
            wikiUserInterface = new UserInterface();
            wikiUIState = new WikiUIState();
            combatTextsToTrack = new bool[Main.maxCombatText];
            combatTextsTimeTracked = new int[Main.maxCombatText];
            combatTextsTypes = MyUtils.FilledArray(() => ElementArray.Default, Main.maxCombatText);
        }

        public override void Unload()
        {
            UnloadIcons();
            wikiUserInterface = null;
            wikiUIState = null;
            combatTextsToTrack = null;
            combatTextsTimeTracked = null;
            combatTextsTypes = null;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int healthBarLayerIndex = layers.FindIndex((layer) => layer.Name.Equals("Vanilla: Entity Health Bars"));
            if (healthBarLayerIndex != -1)
            {
                layers.Insert(healthBarLayerIndex, new LegacyGameInterfaceLayer("TerraTyping: Types", DrawTypes, InterfaceScaleType.Game));
            }

            int inventoryLayerIndex = layers.FindIndex((layer) => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryLayerIndex != -1)
            {
                layers.Insert(inventoryLayerIndex, new LegacyGameInterfaceLayer("TerraTyping: Wiki UI", Wiki, InterfaceScaleType.UI));
                layers.Insert(inventoryLayerIndex, new LegacyGameInterfaceLayer("TerraTyping: Wiki UI Button",
                    delegate
                    {
                        return true;
                    }, InterfaceScaleType.UI));
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (wikiUserInterface?.CurrentState is not null)
            {
                wikiUserInterface.Update(gameTime);
            }
        }

        public override void PostUpdateEverything()
        {
            const int CombatTextLifeTimeDefault = 60;

            for (int i = 0; i < Main.maxCombatText; i++)
            {
                if (!combatTextsToTrack[i])
                {
                    continue;
                }

                CombatText combatText = Main.combatText[i];
                if (combatText is null || !combatText.active)
                {
                    combatTextsToTrack[i] = false;
                    combatTextsTimeTracked[i] = 0;
                    continue;
                }

                ElementArray elementArray = combatTextsTypes[i];

                int elementIndex = (int)Math.Clamp(elementArray.Length * (1 - ((float)combatText.lifeTime / CombatTextLifeTimeDefault)), 0, elementArray.Length - 1);

                Element element = elementArray[elementIndex];
                combatText.color = TerraTypingColors.GetColor(element);
            }
        }

        public void ActivateWikiUI()
        {
            Main.LocalPlayer.SetTalkNPC(-1);
            Main.npcChatCornerItem = 0;
            Main.npcChatText = "";
            SoundEngine.PlaySound(SoundID.MenuOpen);
            Main.playerInventory = false;
            Main.editChest = false;
            wikiUserInterface.SetState(wikiUIState);
        }

        public void DeactivateWikiUI()
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            if (Main.gameMenu)
            {
                Main.menuMode = 0;
            }
            wikiUserInterface.SetState(null);
        }

        public void TrackCombatText(int combatTextIndex, ElementArray elements)
        {
            if (combatTextIndex >= 0
                && combatTextIndex < Main.maxCombatText
                && elements is not null
                && !elements.Empty)
            {
                combatTextsToTrack[combatTextIndex] = true;
                combatTextsTimeTracked[combatTextIndex] = 0;
                combatTextsTypes[combatTextIndex] = elements;
            }
        }

        private bool DrawTypes()
        {
            Player player = Main.LocalPlayer;
            if (player.mouseInterface)
            {
                return true;
            }

            Rectangle mouseRectangle = new Rectangle((int)(Main.mouseX + Main.screenPosition.X), (int)(Main.mouseY + Main.screenPosition.Y), 1, 1);
            if (player.gravDir == -1f)
            {
                mouseRectangle.Y = (int)Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
            }

            if (!iconsLoaded)
            {
                LoadIcons();
            }

            bool showTypesOfCritters = ClientConfig.Instance.ShowTypesOfCritters;
            bool showTypesOfTownNPCs = ClientConfig.Instance.ShowTypesOfTownNPCs;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (ShouldShowTypesOfThisNPC(npc, showTypesOfCritters, showTypesOfTownNPCs) && NPCIsVisible(npc, i, mouseRectangle))
                {
                    DrawTypesOfNPC(npc);
                    break;
                }
            }

            return true;
        }

        private static bool NPCIsVisible(NPC npc, int npcIndex, Rectangle mouseRectangle)
        {
            // code adapted from Main.OverOverNPCs(Rectangle mouseRectangle) 1.4.4

            if (!npc.ShowNameOnHover || !(npc.active & (npc.shimmerTransparency == 0f || npc.CanApplyHunterPotionEffects())))
            {
                return false;
            }

            Rectangle npcRect = new Rectangle((int)npc.Bottom.X - npc.frame.Width / 2, (int)npc.Bottom.Y - npc.frame.Height, npc.frame.Width, npc.frame.Height);
            if (npc.type >= NPCID.WyvernHead && npc.type <= NPCID.WyvernTail)
            {
                npcRect = new Rectangle((int)(npc.position.X + npc.width * 0.5 - 32.0), (int)(npc.position.Y + npc.height * 0.5 - 32.0), 64, 64);
            }
            NPCLoader.ModifyHoverBoundingBox(npc, ref npcRect);

            bool flag = mouseRectangle.Intersects(npcRect);
            bool flag2 = flag || (Main.SmartInteractShowingGenuine || Main.SmartInteractNPC == npcIndex);
            if (flag2 && ((npc.type != NPCID.Mimic && npc.type != NPCID.PresentMimic && npc.type != NPCID.IceMimic && npc.aiStyle != 87) || npc.ai[0] != 0f) && npc.type != NPCID.TargetDummy && npc.type != NPCID.BoundTownSlimeOld)
            {
                return true;
            }

            return false;
        }

        private static bool ShouldShowTypesOfThisNPC(NPC npc, bool showTypesOfCritters, bool showTypesOfTownNPCs)
        {
            if (!showTypesOfCritters)
            {
                if (npc.lifeMax <= 5)
                {
                    return false;
                }
            }

            if (!showTypesOfTownNPCs)
            {
                if (npc.townNPC || npc.type == NPCID.BoundGoblin || npc.type == NPCID.BoundWizard || npc.type == NPCID.BoundMechanic || npc.type == NPCID.WebbedStylist || npc.type == NPCID.SleepingAngler || npc.type == NPCID.BartenderUnconscious || npc.type == NPCID.SkeletonMerchant || npc.type == NPCID.GolferRescue || npc.type == NPCID.LostGirl)
                {
                    return false;
                }
            }

            return true;
        }

        private void DrawTypesOfNPC(NPC npc)
        {
            const float buffer = 6;

            NPCWrapper npcWrapper = NPCWrapper.GetWrapper(npc);

            Vector2 topLeft = Main.MouseScreen + new Vector2(23, 48);
            Vector2 defensiveIconsVector = topLeft;
            Vector2 contactIconVector = topLeft;

            for (int i = 0; i < npcWrapper.DefensiveElements.Length; i++)
            {
                if (npcWrapper.DefensiveElements is null)
                {
                    TerraTyping.Instance.Logger.Warn($"npcWrapper.DefensiveElements is null");
                    continue;
                }

                if (icons is null)
                {
                    TerraTyping.Instance.Logger.Warn($"Icons is null");
                }

                Texture2D icon = icons[(int)npcWrapper.DefensiveElements[i]].Value;

                if (icon is null)
                {
                    TerraTyping.Instance.Logger.Warn($"Icon is null");
                    continue;
                }

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
        }

        private bool DrawTypesOld()
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
                            if (npcWrapper.DefensiveElements is null)
                            {
                                TerraTyping.Instance.Logger.Warn($"npcWrapper.DefensiveElements is null");
                                continue;
                            }

                            if (icons is null)
                            {
                                TerraTyping.Instance.Logger.Warn($"Icons is null");
                            }

                            Texture2D icon = icons[(int)npcWrapper.DefensiveElements[i]].Value;

                            if (icon is null)
                            {
                                TerraTyping.Instance.Logger.Warn($"Icon is null");
                                continue;
                            }

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
                    icons[i] = ModContent.Request<Texture2D>($"TerraTyping/TypeIcons/Circle17/{LangHelper.InternalElementName(elements[i], true)}");
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
