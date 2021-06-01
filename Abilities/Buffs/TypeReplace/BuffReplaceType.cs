using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TerraTyping.Abilities.Buffs.TypeReplace
{
    public class BuffReplaceType : ModBuff, IUseModifiedType
    {
        public Element MyElement { get; set; }

        public override bool Autoload(ref string name, ref string texture)
        {
            return false;
        }

        public override void SetDefaults()
        {
            DisplayName.SetDefault($"{LangHelper.ElementName(MyElement)} Type");
            Description.SetDefault($"You are now {LangHelper.ElementName(MyElement)}, replacing what your typing would have been.");
            Main.debuff[Type] = true;
        }
    }
}
