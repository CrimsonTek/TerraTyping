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
                var line = new TooltipLine(mod, "Type", LangHelper.ElementName(DictionaryHelper.Item(item)[item.type].Offensive))
                {
                    overrideColor = new Color
                    (
                        Colors.Type[DictionaryHelper.Item(item)[item.type].Offensive].Item1,
                        Colors.Type[DictionaryHelper.Item(item)[item.type].Offensive].Item2,
                        Colors.Type[DictionaryHelper.Item(item)[item.type].Offensive].Item3
                    )
                };
                tooltips.Add(line);
            }
            else if (DictionaryHelper.Ammo(item).ContainsKey(item.type))
            {
                var line = new TooltipLine(mod, "Type", LangHelper.ElementName(DictionaryHelper.Ammo(item)[item.type].Offensive))
                {
                    overrideColor = new Color
                      (
                          Colors.Type[DictionaryHelper.Ammo(item)[item.type].Offensive].Item1,
                          Colors.Type[DictionaryHelper.Ammo(item)[item.type].Offensive].Item2,
                          Colors.Type[DictionaryHelper.Ammo(item)[item.type].Offensive].Item3
                      )
                };
                tooltips.Add(line);
            }
            else if (DictionaryHelper.Armor(item).ContainsKey(item.type))
            {
                var firstline = new TooltipLine(mod, "Type", LangHelper.ElementName(DictionaryHelper.Armor(item)[item.type].Primary))
                {
                    overrideColor = new Color
                    (
                        Colors.Type[DictionaryHelper.Armor(item)[item.type].Primary].Item1,
                        Colors.Type[DictionaryHelper.Armor(item)[item.type].Primary].Item2,
                        Colors.Type[DictionaryHelper.Armor(item)[item.type].Primary].Item3
                    )
                };
                tooltips.Add(firstline);

                if (DictionaryHelper.Armor(item)[item.type].Secondary != Element.none)
                {
                    var secondline = new TooltipLine(mod, "Type", LangHelper.ElementName(DictionaryHelper.Armor(item)[item.type].Secondary))
                    {
                        overrideColor = new Color
                        (
                            Colors.Type[DictionaryHelper.Armor(item)[item.type].Secondary].Item1,
                            Colors.Type[DictionaryHelper.Armor(item)[item.type].Secondary].Item2,
                            Colors.Type[DictionaryHelper.Armor(item)[item.type].Secondary].Item3
                        )
                    };
                    tooltips.Add(secondline);
                }
            }
        }
    }
}
