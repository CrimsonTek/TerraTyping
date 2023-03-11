using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.Attributes;

namespace TerraTyping
{
    public class Russian
    {
        public static string ElementName(Element element)
        {
            return element switch
            {
                Element.normal => "Нормальный",
                Element.fire => "Огненный",
                Element.water => "Водянной",
                Element.electric => "Электрический",
                Element.grass => "Травянной",
                Element.ice => "Ледянной",
                Element.fighting => "Боевой",
                Element.poison => "Отравляющий",
                Element.ground => "Землянной",
                Element.flying => "Летающий",
                Element.psychic => "Физический",
                Element.bug => "Жук",
                Element.rock => "Камменый",
                Element.ghost => "Призрачный",
                Element.dragon => "Драконний",
                Element.dark => "Тёмный",
                Element.steel => "Стальной",
                Element.fairy => "Фантастический",
                Element.blood => "Кровянной",
                Element.bone => "Костянной",
                Element.none => "Никакой",
                _ => throw new ArgumentException("Unexpected element: {element}.")
            };
        }
    }
}