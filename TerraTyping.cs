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
            Mod weaponOut = ModLoader.GetMod("WeaponOut");
            if (weaponOut != null)
            {
                foreach (KeyValuePair<string, Element> index in Items.newWeaponOut)
                {
                    Items.WeaponOut.Add(weaponOut.ItemType(index.Key), index.Value);
                }
            }
        }
        public override void Unload()
        {
            Items.WeaponOut.Clear();
        }

        public override void PostSetupContent()
        {
        }
    }
}
