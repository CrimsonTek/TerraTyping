using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.Dictionaries;
using TerraTyping.TypeLoaders;

namespace TerraTyping;

public class TTGlobalItem : GlobalItem
{
    public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
    {
        WeaponWrapper offensiveType = new WeaponWrapper(item, player);
        if (offensiveType.GetsStab)
        {
            float mult = Calc.Stab(offensiveType, PlayerWrapper.GetWrapper(player));
            damage = damage.CombineWith(new StatModifier(1f, mult));
        }
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        const double CycleSpeed = 0.75;

        ElementArray itemElements = WeaponTypeLoader.GetElements(item);
        if (!itemElements.Empty)
        {
            SpecialTooltip[] specialTooltips = WeaponTypeLoader.GetSpecialTooltips(item);
            if (specialTooltips is not null)
            {
                if (specialTooltips.Length != 0)
                {
                    for (int i = 0; i < specialTooltips.Length; i++)
                    {
                        SpecialTooltip st = specialTooltips[i];
                        Color[] colors = st.Colors;
                        tooltips.Add(new TooltipLine(Mod, $"SpecialItemTooltip{i + 1}", st.TooltipString)
                        {
                            OverrideColor = CyclingColorsIfNeeded(CycleSpeed, colors)
                        });
                    }
                }
                else
                {
                    AddTooltipsForElementArray(tooltips, itemElements);
                }
            }
            else
            {
                AddTooltipsForElementArray(tooltips, itemElements);
            }

            WeaponWrapper offensiveType = new WeaponWrapper(item, Main.LocalPlayer);
            float mult = Calc.Stab(offensiveType, PlayerWrapper.GetWrapper(Main.LocalPlayer));
            if (mult != 1)
            {
                tooltips.Add(new TooltipLine(Mod, "STABTooltip", $"STAB: {mult:P0}")
                {

                });
            }

            return;
        }

        ElementArray armorElements = ArmorTypeLoader.GetElements(item);
        if (!armorElements.Empty)
        {
            AddTooltipsForElementArray(tooltips, armorElements);

            AbilityID armorAbility = ArmorTypeLoader.GetAbility(item);
            if (armorAbility != AbilityID.None)
            {
                tooltips.Add(new TooltipLine(Mod, "Ability", $"Provides ability: {LangHelper.AbilityName(armorAbility)}"));
            }

            return;
        }

        // todo: ammo tooltips
    }

    /// <summary>
    /// If <paramref name="colors"/> has more than 1 color, cycles through them. If it has 1 color, returns it. If it has 0 colors, returns <see cref="Color.White"/>.
    /// </summary>
    /// <param name="CycleSpeed"></param>
    /// <param name="colors"></param>
    /// <returns></returns>
    private static Color CyclingColorsIfNeeded(double CycleSpeed, Color[] colors)
    {
        return colors?.Length switch
        {
            0 or null => Color.White,
            1 => colors[0],
            _ => colors[(int)((Main.timeForVisualEffects * CycleSpeed) % colors.Length)],
        };
    }

    private void AddTooltipsForElementArray(List<TooltipLine> tooltips, ElementArray elementArray)
    {
        for (int i = 0; i < elementArray.Length; i++)
        {
            Element element = elementArray[i];
            tooltips.Add(new TooltipLine(Mod, $"ItemType{i + 1}", LangHelper.ElementName(element))
            {
                OverrideColor = ElementColors.GetColor(element)
            });
        }
    }
}
