using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping
{
    public class Calc
    {
        //public static string Text(object attack, object defense)
        //{
        //    ElementHelper elementHelper = new ElementHelper();

        //    int defensePrimary = (int)elementHelper.Primary(defense);
        //    int defenseSecondary = (int)elementHelper.Secondary(defense);
        //    int defenseTertiary = (int)elementHelper.Tertiary(defense);
        //    int attackQuatrinary = (int)elementHelper.Quatrinary(attack);

        //    float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
        //    float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
        //    float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

        //    string text = string.Empty;
        //    int mult = (int)(multiplier1 * multiplier2 * multiplier3 * 10);
        //    if (mult == 0)
        //        text = "Immune!";
        //    if (mult == 0.25)
        //        text = ""
        //    return text;
        //}

        public static float Damage(object attack, object defense)
        {
            int defensePrimary = (int)ElementHelper.Primary(defense);
            int defenseSecondary = (int)ElementHelper.Secondary(defense);
            int defenseTertiary = (int)ElementHelper.Tertiary(defense);
            int attackQuatrinary = (int)ElementHelper.Quatrinary(attack);

            float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
            float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
            float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

            float dmg = multiplier1 * multiplier2 * multiplier3;
            //if (dmg != 0)
            //    if (!Main.expertMode)
            //        dmg = (dmg + dmg + 1) / 3;
            return dmg;
        }

        public static float STAB(object attack, object defense)
        {
            int defensePrimary = (int)ElementHelper.Primary(defense);
            int defenseSecondary = (int)ElementHelper.Secondary(defense);
            int defenseTertiary = (int)ElementHelper.Tertiary(defense);
            int attackQuatrinary = (int)ElementHelper.Quatrinary(attack);

            float multiplier = 1.0f;

            if (attackQuatrinary != (int)Element.none)
            {
                if (defensePrimary == attackQuatrinary)
                    multiplier *= ModContent.GetInstance<Config>().STAB;
                else if (defenseSecondary == attackQuatrinary)
                    multiplier *= ModContent.GetInstance<Config>().STAB;
                else if (defenseTertiary == attackQuatrinary)
                    multiplier *= ModContent.GetInstance<Config>().STAB;
            }
            //if (!Main.expertMode)
            //    multiplier = (multiplier + 1) / 2;
            return multiplier;
        }

        public static int Buff(object buff, object defense, int lifeRegen)
        {
            int defensePrimary = (int)ElementHelper.Primary(defense);
            int defenseSecondary = (int)ElementHelper.Secondary(defense);
            int defenseTertiary = (int)ElementHelper.Tertiary(defense);
            int attackQuatrinary = (int)ElementHelper.Quatrinary(buff);

            float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
            float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
            float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

            float mult = -lifeRegen;
            mult += (lifeRegen * multiplier1 * multiplier2 * multiplier3);
            return (int)mult;
        }

        public static int LifeRegen(object buff, object defense, int lifeRegen)
        {
            int defensePrimary = (int)ElementHelper.Primary(defense);
            int defenseSecondary = (int)ElementHelper.Secondary(defense);
            int defenseTertiary = (int)ElementHelper.Tertiary(defense);
            int attackQuatrinary = (int)ElementHelper.Quatrinary(buff);

            float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
            float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
            float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

            return (int)(lifeRegen * multiplier1 * multiplier2 * multiplier3);
        }

        public static int BadRegen(object buff, object defense, int buffBadRegen)
        {
            int defensePrimary = (int)ElementHelper.Primary(defense);
            int defenseSecondary = (int)ElementHelper.Secondary(defense);
            int defenseTertiary = (int)ElementHelper.Tertiary(defense);
            int attackQuatrinary = (int)ElementHelper.Quatrinary(buff);

            float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
            float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
            float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

            float multiplier = multiplier1 * multiplier2 * multiplier3;
            int lifeRegen = -buffBadRegen;
            lifeRegen = (int)(buffBadRegen * multiplier);
            return lifeRegen;
        }
    }
}
