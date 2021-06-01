using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
