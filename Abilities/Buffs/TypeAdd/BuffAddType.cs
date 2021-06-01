using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TerraTyping.Abilities.Buffs.TypeAdd
{
    public class BuffAddType : ModBuff, IUseModifiedType
    {
        public Element MyElement { get; set; }

        public override bool Autoload(ref string name, ref string texture)
        {
            return false;
        }

        public override void SetDefaults()
        {
            DisplayName.SetDefault($"Add {LangHelper.ElementName(MyElement)}");
            Description.SetDefault($"You are now {LangHelper.ElementName(MyElement)} in addition to your armor's type.");
            Main.debuff[Type] = true;
        }
    }
}
