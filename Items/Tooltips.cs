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
        static readonly DictionaryHelper dictionaryHelper = new DictionaryHelper();
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (dictionaryHelper.Item(item).ContainsKey(item.type))
            {
                var line = new TooltipLine(mod, "Type", LangHelper.langHelper.ElementName(dictionaryHelper.Item(item)[item.type]))
                {
                    overrideColor = new Color
                    (
                        Colors.Type[dictionaryHelper.Item(item)[item.type]].Item1,
                        Colors.Type[dictionaryHelper.Item(item)[item.type]].Item2,
                        Colors.Type[dictionaryHelper.Item(item)[item.type]].Item3
                    )
                };
                tooltips.Add(line);
            }
            else if (dictionaryHelper.Ammo(item).ContainsKey(item.type))
            {
                var line = new TooltipLine(mod, "Type", LangHelper.langHelper.ElementName(dictionaryHelper.Ammo(item)[item.type]))
                {
                    overrideColor = new Color
                      (
                          Colors.Type[dictionaryHelper.Ammo(item)[item.type]].Item1,
                          Colors.Type[dictionaryHelper.Ammo(item)[item.type]].Item2,
                          Colors.Type[dictionaryHelper.Ammo(item)[item.type]].Item3
                      )
                };
                tooltips.Add(line);
            }
            else if (dictionaryHelper.Armor(item).ContainsKey(item.type))
            {
                var firstline = new TooltipLine(mod, "Type", LangHelper.langHelper.ElementName(dictionaryHelper.Armor(item)[item.type].Item1))
                {
                    overrideColor = new Color
                    (
                        Colors.Type[dictionaryHelper.Armor(item)[item.type].Item1].Item1,
                        Colors.Type[dictionaryHelper.Armor(item)[item.type].Item1].Item2,
                        Colors.Type[dictionaryHelper.Armor(item)[item.type].Item1].Item3
                    )
                };
                tooltips.Add(firstline);

                if (dictionaryHelper.Armor(item)[item.type].Item2 != Element.none)
                {
                    var secondline = new TooltipLine(mod, "Type", LangHelper.langHelper.ElementName(dictionaryHelper.Armor(item)[item.type].Item2))
                    {
                        overrideColor = new Color
                        (
                            Colors.Type[dictionaryHelper.Armor(item)[item.type].Item2].Item1,
                            Colors.Type[dictionaryHelper.Armor(item)[item.type].Item2].Item2,
                            Colors.Type[dictionaryHelper.Armor(item)[item.type].Item2].Item3
                        )
                    };
                    tooltips.Add(secondline);
                }
            }
        }
    }
}
