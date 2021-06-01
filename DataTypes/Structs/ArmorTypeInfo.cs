using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.DataTypes
{
    public struct ArmorTypeInfo
    {
        public Element Primary { get; set; }
        public Element Secondary { get; set; }
        public AbilityID AbilityID { get; set; }

        public ArmorTypeInfo(Element primary, Element secondary)
        {
            Primary = primary;
            Secondary = secondary;
            AbilityID = AbilityID.None;
        }
        
        public ArmorTypeInfo(Element primary, Element secondary, AbilityID abilityID)
        {
            Primary = primary;
            Secondary = secondary;
            AbilityID = abilityID;
        }
    }
}
