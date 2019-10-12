using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items
{
    public class Tooltips : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (Items.Type.ContainsKey(item.type))
            {
                var line = new TooltipLine(mod, "Type", Formal.Name[Items.Type[item.type]])
                {
                    overrideColor = new Color
                    (
                        Colors.Type[Items.Type[item.type]].Item1, 
                        Colors.Type[Items.Type[item.type]].Item2, 
                        Colors.Type[Items.Type[item.type]].Item3
                    )
                };
                tooltips.Add(line);
            }
            else if (Ammos.Type.ContainsKey(item.type))
            {
                var line = new TooltipLine(mod, "Type", Formal.Name[Ammos.Type[item.type]])
                {
                    overrideColor = new Color
                      (
                          Colors.Type[Ammos.Type[item.type]].Item1,
                          Colors.Type[Ammos.Type[item.type]].Item2,
                          Colors.Type[Ammos.Type[item.type]].Item3
                      )
                };
                tooltips.Add(line);
            }
            else if (Armors.Type.ContainsKey(item.type))
            {
                var firstline = new TooltipLine(mod, "Type", Formal.Name[Armors.Type[item.type].Item1])
                {
                    overrideColor = new Color
                    (
                        Colors.Type[Armors.Type[item.type].Item1].Item1,
                        Colors.Type[Armors.Type[item.type].Item1].Item2,
                        Colors.Type[Armors.Type[item.type].Item1].Item3
                    )
                };
                tooltips.Add(firstline);

                if (Armors.Type[item.type].Item2 != Element.Type.none)
                {
                    var secondline = new TooltipLine(mod, "Type", Formal.Name[Armors.Type[item.type].Item2])
                    {
                        overrideColor = new Color
                        (
                            Colors.Type[Armors.Type[item.type].Item2].Item1,
                            Colors.Type[Armors.Type[item.type].Item2].Item2,
                            Colors.Type[Armors.Type[item.type].Item2].Item3
                        )
                    };
                    tooltips.Add(secondline);
                }
            }
        }
    }
}
