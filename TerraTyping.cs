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
using TerraTyping.Abilities.Buffs;
using TerraTyping.Helpers;
using TerraTyping.TypeLoaders;
using log4net;

namespace TerraTyping
{
    class TerraTyping : Mod
    {
        private static readonly List<string> errors = new List<string>();
        private static TerraTyping instance;

        public static TerraTyping Instance
        {
            get => (instance ??= ModContent.GetInstance<TerraTyping>());
            private set => instance = value;
        }

        public override void Load()
        {
            Instance = this;

            ElementHelper.Load();
            ElementArray.Load();

            errors.Clear();
        }

        public override void PostSetupContent()
        {
            TypeLoader.IsLoadingTypes = true;
            TypeLoader.Logger = Logger;
            Table.Load();

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

        public override void Unload()
        {
            BuffUtils.addTypeBuffs = null;
            BuffUtils.replaceTypeBuffs = null;

            ProjectileWrapper.Unload();
            ElementHelper.Unload();
            ElementArray.Unload();
            Table.Unload();

            SpecialTooltip.StaticUnload();
            Instance = null;
        }
    }
}
