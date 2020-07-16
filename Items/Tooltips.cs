using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping
{
    public class Tooltips : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (DictionaryHelper.Item(item).ContainsKey(item.type))
            {
                var line = new TooltipLine(mod, "Type", LangHelper.langHelper.ElementName(DictionaryHelper.Item(item)[item.type]))
                {
                    overrideColor = new Color
                    (
                        Colors.Type[DictionaryHelper.Item(item)[item.type]].Item1,
                        Colors.Type[DictionaryHelper.Item(item)[item.type]].Item2,
                        Colors.Type[DictionaryHelper.Item(item)[item.type]].Item3
                    )
                };
                tooltips.Add(line);
            }
            else if (DictionaryHelper.Ammo(item).ContainsKey(item.type))
            {
                var line = new TooltipLine(mod, "Type", LangHelper.langHelper.ElementName(DictionaryHelper.Ammo(item)[item.type]))
                {
                    overrideColor = new Color
                      (
                          Colors.Type[DictionaryHelper.Ammo(item)[item.type]].Item1,
                          Colors.Type[DictionaryHelper.Ammo(item)[item.type]].Item2,
                          Colors.Type[DictionaryHelper.Ammo(item)[item.type]].Item3
                      )
                };
                tooltips.Add(line);
            }
            else if (DictionaryHelper.Armor(item).ContainsKey(item.type))
            {
                var firstline = new TooltipLine(mod, "Type", LangHelper.langHelper.ElementName(DictionaryHelper.Armor(item)[item.type].Item1))
                {
                    overrideColor = new Color
                    (
                        Colors.Type[DictionaryHelper.Armor(item)[item.type].Item1].Item1,
                        Colors.Type[DictionaryHelper.Armor(item)[item.type].Item1].Item2,
                        Colors.Type[DictionaryHelper.Armor(item)[item.type].Item1].Item3
                    )
                };
                tooltips.Add(firstline);

                if (DictionaryHelper.Armor(item)[item.type].Item2 != Element.none)
                {
                    var secondline = new TooltipLine(mod, "Type", LangHelper.langHelper.ElementName(DictionaryHelper.Armor(item)[item.type].Item2))
                    {
                        overrideColor = new Color
                        (
                            Colors.Type[DictionaryHelper.Armor(item)[item.type].Item2].Item1,
                            Colors.Type[DictionaryHelper.Armor(item)[item.type].Item2].Item2,
                            Colors.Type[DictionaryHelper.Armor(item)[item.type].Item2].Item3
                        )
                    };
                    tooltips.Add(secondline);
                }
            }
        }
    }
}
