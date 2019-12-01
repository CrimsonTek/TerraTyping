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

namespace TerraTyping
{
	class TerraTyping : Mod
	{
        public TerraTyping()
        {
        }
        public override void Load()
        {
        }
        public override void Unload()
        {
            Mod weaponOut = ModLoader.GetMod("WeaponOut");
            if (Items.Items.WeaponOut.Count > 0)
            {
                foreach (var index in Items.Items.WeaponOut)
                {
                    if (Items.Items.Type.ContainsKey(index.Key))
                        Items.Items.Type.Remove(index.Key);
                }
            }
        }

        public override void PostSetupContent()
        {
            Items.Items.ModCompatibility();
        }
    }
}
