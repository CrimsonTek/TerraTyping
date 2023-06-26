using TerraTyping.Core;

namespace TerraTyping.DataTypes
{
    public struct ItemTypeInfo
    {
        public Element Offensive { get; set; }

        public ItemTypeInfo(Element element)
        {
            Offensive = element;
        }
    }
}
