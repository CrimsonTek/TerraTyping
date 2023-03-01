//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.Localization;
//using Terraria.ID;
//using Terraria.ModLoader;
//using TerraTyping.DataTypes;
//using TerraTyping.Dictionaries;
//using TerraTyping.Abilities;

//namespace TerraTyping.Content;

//public class ItemTooltips : GlobalItem
//{
//    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
//    {
//        ElementArray weaponElements = Weapons.GetInfo(item.type).Elements;
//        if (!weaponElements.Empty)
//        {
//            AddTooltipsForElementArray(tooltips, weaponElements);
//            return;
//        }

//        ArmorTypeInfo armorTypeInfo = Armors.GetInfo(item.type);
//        ElementArray armorElements = armorTypeInfo.Elements;
//        if (!armorElements.Empty)
//        {
//            AddTooltipsForElementArray(tooltips, armorElements);

//            if (armorTypeInfo.AbilityID != AbilityID.None)
//            {
//                tooltips.Add(new TooltipLine(Mod, "Ability", $"Provides ability: {LangHelper.AbilityName(armorTypeInfo.AbilityID)}"));
//            }

//            return;
//        }

//        // 
//    }

//    private void AddTooltipsForElementArray(List<TooltipLine> tooltips, ElementArray elementArray)
//    {
//        for (int i = 0; i < elementArray.Length; i++)
//        {
//            Element element = elementArray[i];
//            tooltips.Add(new TooltipLine(Mod, $"Type{i + 1}", LangHelper.ElementName(element))
//            {
//                OverrideColor = Colors.type[element]
//            });
//        }
//    }
//}
