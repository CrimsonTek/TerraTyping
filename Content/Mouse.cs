//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace TerraTyping
//{
//    class Mouse : GlobalNPC
//    {
//        //public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
//        //{
//        //    if (Main.HoveringOverAnNPC)
//        //    {
//        //        ElementHelper elementHelper = new ElementHelper();

//        //        int buffer = 4;
//        //        int prev = 12;

//        //        Element Primary = elementHelper.Primary(npc);
//        //        Element Secondary = elementHelper.Secondary(npc);
//        //        Element Tertiary = elementHelper.Tertiary(npc);
//        //        Element Quatrinary = elementHelper.Quatrinary(npc);

//        //        var icon1 = mod.GetTexture("Types/" + Formal.Name[Primary]);
//        //        var icon2 = mod.GetTexture("Types/" + Formal.Name[Secondary]);
//        //        var icon3 = mod.GetTexture("Types/" + Formal.Name[Tertiary]);
//        //        var icon4 = mod.GetTexture("Types/" + Formal.Name[Quatrinary]);
//        //        if (!Main.hideUI && Math.Abs(npc.position.X + npc.width / 2 - Main.screenPosition.X - Main.mouseX) <= npc.width && Math.Abs(npc.position.Y + npc.height / 2 - Main.screenPosition.Y - Main.mouseY) <= npc.height)
//        //        {
//        //            if (Enemies.Type[npc.type].Item1 != Element.none && Enemies.Type[npc.type].Item1 != Element.levitate)
//        //            {
//        //                Main.spriteBatch.Draw(icon1, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//        //                prev += icon1.Width + buffer;
//        //            }
//        //            if (Enemies.Type[npc.type].Item2 != Element.none && Enemies.Type[npc.type].Item2 != Element.levitate)
//        //            {
//        //                Main.spriteBatch.Draw(icon2, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//        //                prev += icon2.Width + buffer;
//        //            }
//        //            if (Enemies.Type[npc.type].Item3 != Element.none && Enemies.Type[npc.type].Item3 != Element.levitate)
//        //            {
//        //                Main.spriteBatch.Draw(icon3, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//        //            }
//        //            if (Enemies.Type[npc.type].Item4 != Element.none && Enemies.Type[npc.type].Item4 != Element.levitate)
//        //            {
//        //                Main.spriteBatch.Draw(icon4, new Vector2(Main.mouseX + 12, Main.mouseY + 38 + icon1.Width + buffer), Color.White);
//        //            }
//        //        }
//        //    }
//        //    return base.PreDraw(npc, spriteBatch, drawColor);
//        //}

//        //public override 

//        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
//        {
//            if (Main.hideUI || Main.ActivePlayersCount == 0)
//                return;

//            ElementHelper elementHelper = new ElementHelper();

//            int buffer = 4;
//            int prev = 12;

//            Element Primary = elementHelper.Primary(npc);
//            Element Secondary = elementHelper.Secondary(npc);
//            Element Tertiary = elementHelper.Tertiary(npc);
//            Element Quatrinary = elementHelper.Quatrinary(npc);

//            var icon1 = mod.GetTexture("Types/" + Formal.Name[Primary]);
//            var icon2 = mod.GetTexture("Types/" + Formal.Name[Secondary]);
//            var icon3 = mod.GetTexture("Types/" + Formal.Name[Tertiary]);
//            var icon4 = mod.GetTexture("Types/" + Formal.Name[Quatrinary]);

//            int width = npc.getRect().Width;
//            int height = npc.getRect().Height;
//            int num1 = 12 + (int)(width * 0);
//            int num2 = 12 + (int)(height * 0);
//            Rectangle myRect = new Rectangle(npc.getRect().X - num1,
//                                             npc.getRect().Y - num2,
//                                             npc.getRect().Width + (2 * num1),
//                                             npc.getRect().Height + num2);
//            if (myRect.Contains(new Point((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y)))
//            {
//                if (Primary != Element.none && Primary != Element.levitate)
//                {
//                    Main.spriteBatch.Draw(icon1, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//                    prev += icon1.Width + buffer;
//                }
//                if (Secondary != Element.none && Secondary != Element.levitate)
//                {
//                    Main.spriteBatch.Draw(icon2, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//                    prev += icon2.Width + buffer;
//                }
//                if (Tertiary != Element.none && Tertiary != Element.levitate)
//                {
//                    Main.spriteBatch.Draw(icon3, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//                }
//                if (Quatrinary != Element.none && Quatrinary != Element.levitate)
//                {
//                    Main.spriteBatch.Draw(icon4, new Vector2(Main.mouseX + 12, Main.mouseY + 38 + icon1.Width + buffer), Color.White);
//                }
//            }
//            //if (!Main.hideUI && Math.Abs(npc.position.X + npc.width / 2 - Main.screenPosition.X - Main.mouseX) <= npc.width && Math.Abs(npc.position.Y + npc.height / 2 - Main.screenPosition.Y - Main.mouseY) <= npc.height)
//            //{
//            //    if (Enemies.Type[npc.type].Item1 != Element.none && Enemies.Type[npc.type].Item1 != Element.levitate)
//            //    {
//            //        Main.spriteBatch.Draw(icon1, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//            //        prev += icon1.Width + buffer;
//            //    }
//            //    if (Enemies.Type[npc.type].Item2 != Element.none && Enemies.Type[npc.type].Item2 != Element.levitate)
//            //    {
//            //        Main.spriteBatch.Draw(icon2, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//            //        prev += icon2.Width + buffer;
//            //    }
//            //    if (Enemies.Type[npc.type].Item3 != Element.none && Enemies.Type[npc.type].Item3 != Element.levitate)
//            //    {
//            //        Main.spriteBatch.Draw(icon3, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
//            //    }
//            //    if (Enemies.Type[npc.type].Item4 != Element.none && Enemies.Type[npc.type].Item4 != Element.levitate)
//            //    {
//            //        Main.spriteBatch.Draw(icon4, new Vector2(Main.mouseX + 12, Main.mouseY + 38 + icon1.Width + buffer), Color.White);
//            //    }
//            //}
//        }
//    }
//}