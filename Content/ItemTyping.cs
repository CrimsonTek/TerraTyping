using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerraTyping.DataTypes;
using TerraTyping.TypeLoaders;
using TerraTyping.Common;

namespace TerraTyping
{
    /// <summary>
    /// Code from https://github.com/JavidPack/ModdersToolkit/blob/02f76e799c74686dcaf60ef355faf3396cc4f97d/Tools/Hitboxes/HitboxesTool.cs#L68
    /// </summary>
    public class ItemTyping : GlobalItem
    {
        [CloneByReference]
        public Rectangle?[] meleeHitbox = new Rectangle?[Main.maxPlayers];

        public override bool InstancePerEntity => true;

        public override GlobalItem NewInstance(Item item)
        {
            return base.NewInstance(item);
        }

        public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            meleeHitbox[player.whoAmI] = hitbox;
        }

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
            ElementArray weaponElements = WeaponTypeLoader.GetElements(item);
            if (!weaponElements.Empty)
            {
                WeaponTooltip(item, tooltips, weaponElements);
            }

            ElementArray armorElements = ArmorTypeLoader.GetElements(item);
            if (!armorElements.Empty)
            {
                ArmorTooltip(item, tooltips, armorElements);
            }

            ElementArray ammoElements = AmmoTypeLoader.GetElements(item);
            if (!ammoElements.Empty)
            {
                AmmoTooltip(item, tooltips, ammoElements);
            }

            ElementArray specialItemElements = SpecialItemTypeLoader.GetElements(item);
            if (!specialItemElements.Empty)
            {
                SpecialItemTooltip(item, tooltips, specialItemElements);
            }
        }

        private void WeaponTooltip(Item item, List<TooltipLine> tooltips, ElementArray weaponElements)
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
                        const double CycleSpeed = 1;
                        tooltips.Add(new TooltipLine(Mod, $"SpecialItemTooltip{i + 1}", st.TooltipString)
                        {
                            OverrideColor = CyclingColorsIfNeeded(CycleSpeed, colors)
                        });
                    }
                }
                else
                {
                    AddTooltipsForElementArray(tooltips, weaponElements);
                }
            }
            else
            {
                AddTooltipsForElementArray(tooltips, weaponElements);
            }

            WeaponWrapper offensiveType = new WeaponWrapper(item, Main.LocalPlayer);
            float mult = Calc.Stab(offensiveType, PlayerWrapper.GetWrapper(Main.LocalPlayer));
            if (mult != 1)
            {
                tooltips.Add(new TooltipLine(Mod, "STABTooltip", $"STAB: {mult:P0}"));
            }
        }

        private void SpecialItemTooltip(Item item, List<TooltipLine> tooltips, ElementArray specialItemElements)
        {
            SpecialTooltip[] specialTooltips = SpecialItemTypeLoader.GetSpecialTooltips(item);
            if (specialTooltips is not null)
            {
                if (specialTooltips.Length != 0)
                {
                    for (int i = 0; i < specialTooltips.Length; i++)
                    {
                        SpecialTooltip st = specialTooltips[i];
                        Color[] colors = st.Colors;
                        const double CycleSpeed = 1;
                        tooltips.Add(new TooltipLine(Mod, $"SpecialItemTooltip{i + 1}", st.TooltipString)
                        {
                            OverrideColor = CyclingColorsIfNeeded(CycleSpeed, colors)
                        });
                    }
                }
                else
                {
                    AddTooltipsForElementArray(tooltips, specialItemElements);
                }
            }
            else
            {
                AddTooltipsForElementArray(tooltips, specialItemElements);
            }
        }

        private void ArmorTooltip(Item item, List<TooltipLine> tooltips, ElementArray armorElements)
        {
            AddTooltipsForElementArray(tooltips, armorElements);

            AbilityID armorAbility = ArmorTypeLoader.GetAbility(item);
            if (armorAbility != AbilityID.None)
            {
                tooltips.Add(new TooltipLine(Mod, "Ability", $"Provides ability: {LangHelper.AbilityName(armorAbility)}"));
            }
        }

        private void AmmoTooltip(Item item, List<TooltipLine> tooltips, ElementArray ammoElements)
        {
            AddTooltipsForElementArray(tooltips, ammoElements);
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
                    OverrideColor = TerraTypingColors.GetColor(element)
                });
            }
        }
    }
}
