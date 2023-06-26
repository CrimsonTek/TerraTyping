using Terraria;
using Terraria.ModLoader;

namespace TerraTyping.Core.Abilities.Buffs.TypeReplace
{
    public class BuffReplaceType : ModBuff, IUseModifiedType
    {
        public Element MyElement { get; set; }
        public ElementArray Elements { get; set; }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault($"{LangHelper.MultipleElements(Elements)} Type");
            // Description.SetDefault($"You are now {LangHelper.MultipleElements(Elements)}, replacing what your typing would have been.");
            Main.debuff[Type] = true;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
    }
}
