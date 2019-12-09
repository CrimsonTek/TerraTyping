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
using TerraTyping.Dictionaries;

namespace TerraTyping
{
	class TerraTyping : Mod
	{
        public TerraTyping()
        {

        }

        //public override void PostSetupContent()
        //{
        //    Mod weaponOut = ModLoader.GetMod("WeaponOut");
        //    Initialize(weaponOut);
        //}

        //public void Initialize(Mod mod)
        //{
        //    if (mod == null)
        //        return;

        //    if (mod.Name == "WeaponOut")
        //    {
        //        foreach(KeyValuePair<string, Element> entry in WeaponOut._Item)
        //        {
        //            int key = mod.ItemType(entry.Key);
        //            Element value = entry.Value;
        //            if (WeaponOut.Item.ContainsKey(key))
        //                return;
        //            WeaponOut.Item.Add(key, value);
        //        }
        //    }
        //}
    }
}
