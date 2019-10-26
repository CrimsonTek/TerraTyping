using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Items
{
    class Mouse : GlobalNPC
    {
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (Enemies.Type.ContainsKey(npc.type))
            {
                int buffer = 4;
                int prev = 12;

                var icon1 = mod.GetTexture("Types/" + Formal.Name[Enemies.Type[npc.type].Item1]);
                var icon2 = mod.GetTexture("Types/" + Formal.Name[Enemies.Type[npc.type].Item2]);
                var icon3 = mod.GetTexture("Types/" + Formal.Name[Enemies.Type[npc.type].Item3]);
                var icon4 = mod.GetTexture("Types/" + Formal.Name[Enemies.Type[npc.type].Item4]);
                if (!Main.hideUI && Math.Abs(npc.position.X + npc.width / 2 - Main.screenPosition.X - Main.mouseX) <= npc.width && Math.Abs(npc.position.Y + npc.height / 2 - Main.screenPosition.Y - Main.mouseY) <= npc.height)
                {
                    if (Enemies.Type[npc.type].Item1 != Element.Type.none && Enemies.Type[npc.type].Item1 != Element.Type.levitate)
                    {
                        Main.spriteBatch.Draw(icon1, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
                        prev += icon1.Width + buffer;
                    }
                    if (Enemies.Type[npc.type].Item2 != Element.Type.none && Enemies.Type[npc.type].Item2 != Element.Type.levitate)
                    {
                        Main.spriteBatch.Draw(icon2, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
                        prev += icon2.Width + buffer;
                    }
                    if (Enemies.Type[npc.type].Item3 != Element.Type.none && Enemies.Type[npc.type].Item3 != Element.Type.levitate)
                    {
                        Main.spriteBatch.Draw(icon3, new Vector2(Main.mouseX + prev, Main.mouseY + 38), Color.White);
                    }
                    if (Enemies.Type[npc.type].Item4 != Element.Type.none && Enemies.Type[npc.type].Item4 != Element.Type.levitate)
                    {
                        Main.spriteBatch.Draw(icon4, new Vector2(Main.mouseX + 12, Main.mouseY + 38 + icon1.Width + buffer), Color.White);
                    }
                }
            }
        }
    }
}