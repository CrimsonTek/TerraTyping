using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerraTyping.DataTypes;
using TerraTyping.TypeLoaders;
using TerraTyping.Helpers;
using TerraTyping.Core;
using Terraria.Localization;
using Terraria.ID;

namespace TerraTyping.Common.TModLoaderGlobals
{
    /// <summary>
    /// Hitbox code from https://github.com/JavidPack/ModdersToolkit/blob/02f76e799c74686dcaf60ef355faf3396cc4f97d/Tools/Hitboxes/HitboxesTool.cs#L68
    /// </summary>
    public class ItemTyping : GlobalItem
    {
        /// <summary>
        /// This determines the speed at which colors will cycle when multiple colors are used for a special tooltip.
        /// </summary>
        const double CycleSpeed = 1;

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
            float STAB = GetSTAB(offensiveType);
            damage = damage.CombineWith(new StatModifier(1f, STAB));
        }

        private static float GetSTAB(WeaponWrapper weaponWrapper)
        {
            return Calc.Stab(weaponWrapper, PlayerWrapper.GetWrapper(weaponWrapper.GetPlayer));
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
            SpecialTooltip[] specialTooltips = WeaponTypeLoader.GetSpecialTooltips(item, out bool overrideTypeTooltip);
            if (specialTooltips is not null)
            {
                AddSpecialTooltips(tooltips, weaponElements, specialTooltips, overrideTypeTooltip);
            }
            else
            {
                AddTooltipsForElementArray(tooltips, weaponElements);
            }

            WeaponWrapper offensiveType = new WeaponWrapper(item, Main.LocalPlayer);
            float stab = GetSTAB(offensiveType);
            if (stab != 1)
            {
                string tooltip = Language.GetText("Mods.TerraTyping.Tooltip.STAB").Format(stab.ToString("P0"));
                tooltips.Add(new TooltipLine(Mod, "STABTooltip", tooltip));
            }
        }

        private void SpecialItemTooltip(Item item, List<TooltipLine> tooltips, ElementArray specialItemElements)
        {
            SpecialTooltip[] specialTooltips = SpecialItemTypeLoader.GetSpecialTooltips(item, out bool overrideTypeTooltip);
            if (specialTooltips is not null)
            {
                AddSpecialTooltips(tooltips, specialItemElements, specialTooltips, overrideTypeTooltip);
            }
            else
            {
                AddTooltipsForElementArray(tooltips, specialItemElements);
            }
        }

        private void ArmorTooltip(Item item, List<TooltipLine> tooltips, ElementArray armorElements)
        {
            AddTooltipsForElementArray(tooltips, armorElements);

            Ability armorAbility = ArmorTypeLoader.GetAbility(item);
            if (armorAbility != Ability.None)
            {
                string abilityName = LangHelper.AbilityName(armorAbility, true);
                string tooltip = Language.GetText("Mods.TerraTyping.Tooltip.ArmorAbility").Format(abilityName);
                tooltips.Add(new TooltipLine(Mod, "Ability", tooltip));
            }
        }

        private void AmmoTooltip(Item item, List<TooltipLine> tooltips, ElementArray ammoElements)
        {
            SpecialTooltip[] specialTooltips = AmmoTypeLoader.GetSpecialTooltips(item, out bool overrideTypeTooltip);
            if (specialTooltips is not null)
            {
                AddSpecialTooltips(tooltips, ammoElements, specialTooltips, overrideTypeTooltip);
            }
            else
            {
                AddTooltipsForElementArray(tooltips, ammoElements);
            }
        }

        private void AddSpecialTooltips(List<TooltipLine> tooltips, ElementArray elements, SpecialTooltip[] specialTooltips, bool overrideTypeTooltip)
        {
            if (!overrideTypeTooltip)
            {
                AddTooltipsForElementArray(tooltips, elements);
            }

            if (specialTooltips.Length != 0)
            {
                for (int i = 0; i < specialTooltips.Length; i++)
                {
                    SpecialTooltip st = specialTooltips[i];
                    if (!string.IsNullOrEmpty(st.TooltipString))
                    {
                        Color[] colors = st.Colors;
                        tooltips.Add(new TooltipLine(Mod, $"SpecialItemTooltip{i + 1}", st.TooltipString)
                        {
                            OverrideColor = CyclingColorsIfNeeded(CycleSpeed, colors)
                        });
                    }
                }
            }
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
                _ => colors[(int)(Main.timeForVisualEffects * CycleSpeed % colors.Length)],
            };
        }

        private void AddTooltipsForElementArray(List<TooltipLine> tooltips, ElementArray elementArray)
        {
            for (int i = 0; i < elementArray.Length; i++)
            {
                tooltips.Add(new TooltipLine(Mod, $"ItemType{i + 1}", LangHelper.ElementName(elementArray[i], true))
                {
                    OverrideColor = TerraTypingColors.GetColor(elementArray[i])
                });
            }
        }
    }
}
