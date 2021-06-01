using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.DataTypes.Structs
{
    public struct EntityTyping
    {
        public List<Element> defensiveElements;
        public Element offensiveElement;

        public Element Primary
        {
            get
            {
                return defensiveElements[0];
            }
        }
        public Element Secondary
        {
            get
            {
                if (defensiveElements.Count >= 1)
                {
                    return defensiveElements[1];
                }
                else
                {
                    return Element.none;
                }
            }
        }

        public EntityTyping(List<Element> defensiveElements, Element offensiveElement)
        {
            this.defensiveElements = defensiveElements;
            this.offensiveElement = offensiveElement;
        }

        public EntityTyping(Element primary, Element secondary, Element offensive)
        {
            defensiveElements = new List<Element>()
            {
                primary, secondary
            };
            offensiveElement = offensive;
        }
    }
}
