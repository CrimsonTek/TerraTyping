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
        // player attacking npc
        //public static float Damage(int attackID,
        //                           int defenseID,
        //                           float npcai0,
        //                           Dictionary<int, Element> attackDict,
        //                           Dictionary<int, Tuple<Element, Element, Element, Element>> defenseDict)
        //{
        //    if (attackDict.ContainsKey(attackID) && defenseDict.ContainsKey(defenseID))
        //    {
        //        if (defenseID == NPCID.Retinazer || defenseID == NPCID.Spazmatism)
        //        {
        //            if (npcai0 >= 2)
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.steel];
        //                float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.flying];
        //                float multiplier3 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.none];
        //                return (multiplier1 * multiplier2 * multiplier3);
        //            }
        //            else
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.normal];
        //                float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.flying];
        //                float multiplier3 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.none];
        //                return (multiplier1 * multiplier2 * multiplier3);
        //            }
        //        }
        //        else
        //        {
        //            float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)defenseDict[defenseID].Item1];
        //            float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)defenseDict[defenseID].Item2];
        //            float multiplier3 = Table.Effectiveness[(int)attackDict[attackID], (int)defenseDict[defenseID].Item3];
        //            return (multiplier1 * multiplier2 * multiplier3);
        //        }
        //    }
        //    else
        //        return 1;
        //}

        //public static bool? CanBeHit(int attackType,
        //                             int defenseType,
        //                             float npcai0,
        //                             Dictionary<int, Element> attackDict,
        //                             Dictionary<int, Tuple<Element, Element, Element, Element>> defenseDict)
        //{
        //    if (attackDict.ContainsKey(attackType) && defenseDict.ContainsKey(defenseType))
        //    {
        //        if (defenseType == NPCID.Retinazer || defenseType == NPCID.Spazmatism)
        //        {
        //            if (npcai0 >= 2)
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.steel];
        //                float multiplier2 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.flying];
        //                float multiplier3 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.none];
        //                if (multiplier1 * multiplier2 * multiplier3 == 0) return false;
        //                return null;
        //            }
        //            else
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.normal];
        //                float multiplier2 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.flying];
        //                float multiplier3 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.none];
        //                if (multiplier1 * multiplier2 * multiplier3 == 0) return false;
        //                return null;
        //            }
        //        }
        //        else
        //        {
        //            float multiplier1 = Table.Effectiveness[(int)attackDict[attackType], (int)defenseDict[defenseType].Item1];
        //            float multiplier2 = Table.Effectiveness[(int)attackDict[attackType], (int)defenseDict[defenseType].Item2];
        //            float multiplier3 = Table.Effectiveness[(int)attackDict[attackType], (int)defenseDict[defenseType].Item3];
        //            if (multiplier1 * multiplier2 * multiplier3 == 0) return false;
        //            return null;
        //        }
        //    }
        //    else
        //        return null;
        //}

        //// projectile attacking player
        //public static float Damage(int attackID,
        //                           Tuple<Element, Element, Element> typeSet,
        //                           float npcai0,
        //                           Dictionary<int, Element> attackDict)
        //{
        //    if (attackDict.ContainsKey(attackID))
        //    {
        //        if (attackID == NPCID.Retinazer || attackID == NPCID.Spazmatism)
        //        {
        //            if (npcai0 >= 2)
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)Element.steel, (int)typeSet.Item1];
        //                float multiplier2 = Table.Effectiveness[(int)Element.steel, (int)typeSet.Item2];
        //                return (multiplier1 * multiplier2);
        //            }
        //            else
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)Element.normal, (int)typeSet.Item1];
        //                float multiplier2 = Table.Effectiveness[(int)Element.normal, (int)typeSet.Item2];
        //                return (multiplier1 * multiplier2);
        //            }
        //        }
        //        else
        //        {
        //            float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)typeSet.Item1];
        //            float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)typeSet.Item2];
        //            return (multiplier1 * multiplier2);
        //        }
        //    }
        //    else
        //        return 1;
        //}


        //public static bool CanBeHit(int attackID,
        //                           Tuple<Element, Element, Element> typeSet,
        //                           float npcai0,
        //                           Dictionary<int, Element> attackDict)
        //{
        //    if (attackDict.ContainsKey(attackID))
        //    {
        //        if (attackID == NPCID.Retinazer || attackID == NPCID.Spazmatism)
        //        {
        //            if (npcai0 >= 2)
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)Element.steel, (int)typeSet.Item1];
        //                float multiplier2 = Table.Effectiveness[(int)Element.steel, (int)typeSet.Item2];
        //                if (multiplier1 * multiplier2 == 0) return false;
        //                return true;
        //            }
        //            else
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)Element.normal, (int)typeSet.Item1];
        //                float multiplier2 = Table.Effectiveness[(int)Element.normal, (int)typeSet.Item2];
        //                if (multiplier1 * multiplier2 == 0) return false;
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)typeSet.Item1];
        //            float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)typeSet.Item2];
        //            if (multiplier1 * multiplier2 == 0) return false;
        //            return true;
        //        }
        //    }
        //    else
        //        return true;
        //}

        //// npc attacking player
        //public static float Damage(int attackID,
        //                           Tuple<Element, Element, Element> typeSet,
        //                           float npcai0,
        //                           Dictionary<int, Tuple<Element, Element, Element, Element>> attackDict)
        //{
        //    if (attackDict.ContainsKey(attackID))
        //    {
        //        if (attackID == NPCID.Retinazer || attackID == NPCID.Spazmatism)
        //        {
        //            if (npcai0 >= 2)
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)Element.steel, (int)typeSet.Item1];
        //                float multiplier2 = Table.Effectiveness[(int)Element.steel, (int)typeSet.Item2];
        //                return (multiplier1 * multiplier2);
        //            }
        //            else
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)Element.normal, (int)typeSet.Item1];
        //                float multiplier2 = Table.Effectiveness[(int)Element.normal, (int)typeSet.Item2];
        //                return (multiplier1 * multiplier2);
        //            }
        //        }
        //        else
        //        {
        //            float multiplier1 = Table.Effectiveness[(int)attackDict[attackID].Item4, (int)typeSet.Item1];
        //            float multiplier2 = Table.Effectiveness[(int)attackDict[attackID].Item4, (int)typeSet.Item2];
        //            return (multiplier1 * multiplier2);
        //        }
        //    }
        //    else
        //        return 1;
        //}

        //public static bool CanBeHit(int attackID,
        //                           Tuple<Element, Element, Element> typeSet,
        //                           float npcai0,
        //                           Dictionary<int, Tuple<Element, Element, Element, Element>> attackDict)
        //{
        //    if (attackDict.ContainsKey(attackID))
        //    {
        //        if (attackID == NPCID.Retinazer || attackID == NPCID.Spazmatism)
        //        {
        //            if (npcai0 >= 2)
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)Element.steel, (int)typeSet.Item1];
        //                float multiplier2 = Table.Effectiveness[(int)Element.steel, (int)typeSet.Item2];
        //                if (multiplier1 * multiplier2 == 0) return false;
        //                return true;
        //            }
        //            else
        //            {
        //                float multiplier1 = Table.Effectiveness[(int)Element.normal, (int)typeSet.Item1];
        //                float multiplier2 = Table.Effectiveness[(int)Element.normal, (int)typeSet.Item2];
        //                if (multiplier1 * multiplier2 == 0) return false;
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            float multiplier1 = Table.Effectiveness[(int)attackDict[attackID].Item4, (int)typeSet.Item1];
        //            float multiplier2 = Table.Effectiveness[(int)attackDict[attackID].Item4, (int)typeSet.Item2];
        //            if (multiplier1 * multiplier2 == 0) return false;
        //            return true;
        //        }
        //    }
        //    else
        //        return true;
        //}

        //public static int Damage(int damage, object offense, object defense)
        //{
        //    DictionaryHelper dictionaryHelper = new DictionaryHelper();
        //    int offensePrimary = (int)Element.none;
        //    int defensePrimary = (int)Element.none;
        //    int defenseSeconday = (int)Element.none;
        //    int defenseTertiary = (int)Element.none;


        //    if (offense is Item item)
        //    {
        //        Dictionary<int, Element> attackDict = dictionaryHelper.Item(item);
        //        if (attackDict.ContainsKey(item.type))
        //            offensePrimary = (int)attackDict[item.type];
        //    }
        //    else if (offense is Projectile projectile)
        //    {
        //        Dictionary<int, Element> attackDict = dictionaryHelper.Projectile(projectile);
        //        if (attackDict.ContainsKey(projectile.type))
        //            offensePrimary = (int)attackDict[projectile.type];
        //    }

        //    if (defense is NPC npc)
        //    {
        //        Dictionary<int, Tuple<Element, Element, Element, Element>> defenseDict = dictionaryHelper.NPC(npc);
        //        if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
        //        {
        //            if (npc.ai[0] >= 2)
        //            {
        //                defensePrimary = (int)Element.steel;
        //                defenseSeconday = (int)Element.flying;
        //                defenseTertiary = (int)Element.none;
        //            }
        //            else
        //            {
        //                defensePrimary = (int)Element.normal;
        //                defenseSeconday = (int)Element.flying;
        //                defenseTertiary = (int)Element.none;
        //            }
        //        }
        //        if (defenseDict.ContainsKey(npc.type))
        //        {
        //            defensePrimary = (int)defenseDict[npc.type].Item1;
        //            defenseSeconday = (int)defenseDict[npc.type].Item2;
        //            defenseTertiary = (int)defenseDict[npc.type].Item3;
        //        }
        //    }

        //    float multiplier1 = 1;
        //    float multiplier2 = 1;
        //    float multiplier3 = 1;

        //    if (defense.type == NPCID.Retinazer || defense.type == NPCID.Spazmatism)
        //    {
        //        if (defense.ai[0] >= 2)
        //        {
        //            multiplier1 = Table.Effectiveness[offensePrimary, (int)Element.steel];
        //            multiplier2 = Table.Effectiveness[offensePrimary, (int)Element.flying];
        //            multiplier3 = Table.Effectiveness[offensePrimary, (int)Element.none];
        //        }
        //        else
        //        {
        //            multiplier1 = Table.Effectiveness[offensePrimary, (int)Element.normal];
        //            multiplier2 = Table.Effectiveness[offensePrimary, (int)Element.flying];
        //            multiplier3 = Table.Effectiveness[offensePrimary, (int)Element.none];
        //        }
        //    }
        //    else
        //    {
        //        multiplier1 = Table.Effectiveness[offensePrimary, (int)defenseDict[defense.type].Item1];
        //        multiplier2 = Table.Effectiveness[offensePrimary, (int)defenseDict[defense.type].Item2];
        //        multiplier3 = Table.Effectiveness[offensePrimary, (int)defenseDict[defense.type].Item3];
        //    }
        //    return (int)(damage * multiplier1 * multiplier2 * multiplier3);
        //}

        public static int Damage(object attack, object defense, int damage = 100)
        {
            ElementHelper elementHelper = new ElementHelper();

            int defensePrimary = (int)elementHelper.Primary(defense);
            int defenseSecondary = (int)elementHelper.Secondary(defense);
            int defenseTertiary = (int)elementHelper.Tertiary(defense);
            int attackQuatrinary = (int)elementHelper.Quatrinary(attack);

            float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
            float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
            float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];
            //if (Table.Effectiveness[attackQuatrinary, defensePrimary] == 2.0)
            //    multiplier1 = 2.0f;

            return (int)(damage * multiplier1 * multiplier2 * multiplier3);
        }

        public static float STAB(object attack, object defense)
        {
            ElementHelper elementHelper = new ElementHelper();

            int defensePrimary = (int)elementHelper.Primary(defense);
            int defenseSecondary = (int)elementHelper.Secondary(defense);
            int defenseTertiary = (int)elementHelper.Tertiary(defense);
            int attackQuatrinary = (int)elementHelper.Quatrinary(attack);

            float multipler = 1.0f;

            if (defensePrimary == attackQuatrinary)
                multipler *= Config.STAB;
            else if (defenseSecondary == attackQuatrinary)
                multipler *= Config.STAB;
            else if (defenseTertiary == attackQuatrinary)
                multipler *= Config.STAB;
            return multipler;
        }

        public static int LifeRegen(object buff, object defense, int lifeRegen)
        {
            ElementHelper elementHelper = new ElementHelper();

            int defensePrimary = (int)elementHelper.Primary(defense);
            int defenseSecondary = (int)elementHelper.Secondary(defense);
            int defenseTertiary = (int)elementHelper.Tertiary(defense);
            int attackQuatrinary = (int)elementHelper.Quatrinary(buff);

            float multiplier1 = Table.Effectiveness[attackQuatrinary, defensePrimary];
            float multiplier2 = Table.Effectiveness[attackQuatrinary, defenseSecondary];
            float multiplier3 = Table.Effectiveness[attackQuatrinary, defenseTertiary];

            return (int)(lifeRegen * multiplier1 * multiplier2 * multiplier3);
        }

        public static int BadRegen(object buff, object defense, int buffBadRegen)
        {
            ElementHelper elementHelper = new ElementHelper();

            int defensePrimary = (int)elementHelper.Primary(defense);
            int defenseSecondary = (int)elementHelper.Secondary(defense);
            int defenseTertiary = (int)elementHelper.Tertiary(defense);
            int attackQuatrinary = (int)elementHelper.Quatrinary(buff);

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
