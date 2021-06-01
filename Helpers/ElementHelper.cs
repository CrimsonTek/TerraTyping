using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.Helpers
{
    public static class ElementHelper
    {
        public static Element FromIndex(int i)
        {
            return (Element)Enum.ToObject(typeof(Element), i);
        }
    }
}
