using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace TerraTyping.Items
{
    public class Calc
    {
        // player attacking npc
        public static float Damage(int attackID,
                                   int defenseID,
                                   float npcai0,
                                   Dictionary<int, Element.Type> attackDict,
                                   Dictionary<int, Tuple<Element.Type, Element.Type, Element.Type, Element.Type>> defenseDict)
        {
            if (attackDict.ContainsKey(attackID) && defenseDict.ContainsKey(defenseID))
            {
                if (defenseID == NPCID.Retinazer || defenseID == NPCID.Spazmatism)
                {
                    if (npcai0 >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.Type.steel];
                        float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.Type.none];
                        return (multiplier1 * multiplier2 * multiplier3);
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.Type.normal];
                        float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)attackDict[attackID], (int)Element.Type.none];
                        return (multiplier1 * multiplier2 * multiplier3);
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)defenseDict[defenseID].Item1];
                    float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)defenseDict[defenseID].Item2];
                    float multiplier3 = Table.Effectiveness[(int)attackDict[attackID], (int)defenseDict[defenseID].Item3];
                    return (multiplier1 * multiplier2 * multiplier3);
                }
            }
            else
                return 1;
        }

        public static bool? CanBeHit(int attackType,
                                     int defenseType,
                                     float npcai0,
                                     Dictionary<int, Element.Type> attackDict,
                                     Dictionary<int, Tuple<Element.Type, Element.Type, Element.Type, Element.Type>> defenseDict)
        {
            if (attackDict.ContainsKey(attackType) && defenseDict.ContainsKey(defenseType))
            {
                if (defenseType == NPCID.Retinazer || defenseType == NPCID.Spazmatism)
                {
                    if (npcai0 >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.Type.steel];
                        float multiplier2 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.Type.none];
                        if (multiplier1 * multiplier2 * multiplier3 == 0) return false;
                        return null;
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.Type.normal];
                        float multiplier2 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)attackDict[attackType], (int)Element.Type.none];
                        if (multiplier1 * multiplier2 * multiplier3 == 0) return false;
                        return null;
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)attackDict[attackType], (int)defenseDict[defenseType].Item1];
                    float multiplier2 = Table.Effectiveness[(int)attackDict[attackType], (int)defenseDict[defenseType].Item2];
                    float multiplier3 = Table.Effectiveness[(int)attackDict[attackType], (int)defenseDict[defenseType].Item3];
                    if (multiplier1 * multiplier2 * multiplier3 == 0) return false;
                    return null;
                }
            }
            else
                return null;
        }

        // projectile attacking player
        public static float Damage(int attackID,
                                   Tuple<Element.Type, Element.Type, Element.Type> typeSet,
                                   float npcai0,
                                   Dictionary<int, Element.Type> attackDict)
        {
            if (attackDict.ContainsKey(attackID))
            {
                if (attackID == NPCID.Retinazer || attackID == NPCID.Spazmatism)
                {
                    if (npcai0 >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item2];
                        return (multiplier1 * multiplier2);
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item2];
                        return (multiplier1 * multiplier2);
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)typeSet.Item1];
                    float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)typeSet.Item2];
                    return (multiplier1 * multiplier2);
                }
            }
            else
                return 1;
        }


        public static bool CanBeHit(int attackID,
                                   Tuple<Element.Type, Element.Type, Element.Type> typeSet,
                                   float npcai0,
                                   Dictionary<int, Element.Type> attackDict)
        {
            if (attackDict.ContainsKey(attackID))
            {
                if (attackID == NPCID.Retinazer || attackID == NPCID.Spazmatism)
                {
                    if (npcai0 >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item2];
                        if (multiplier1 * multiplier2 == 0) return false;
                        return true;
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item2];
                        if (multiplier1 * multiplier2 == 0) return false;
                        return true;
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)attackDict[attackID], (int)typeSet.Item1];
                    float multiplier2 = Table.Effectiveness[(int)attackDict[attackID], (int)typeSet.Item2];
                    if (multiplier1 * multiplier2 == 0) return false;
                    return true;
                }
            }
            else
                return true;
        }

        // npc attacking player
        public static float Damage(int attackID,
                                   Tuple<Element.Type, Element.Type, Element.Type> typeSet,
                                   float npcai0,
                                   Dictionary<int, Tuple<Element.Type, Element.Type, Element.Type, Element.Type>> attackDict)
        {
            if (attackDict.ContainsKey(attackID))
            {
                if (attackID == NPCID.Retinazer || attackID == NPCID.Spazmatism)
                {
                    if (npcai0 >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item2];
                        return (multiplier1 * multiplier2);
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item2];
                        return (multiplier1 * multiplier2);
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)attackDict[attackID].Item4, (int)typeSet.Item1];
                    float multiplier2 = Table.Effectiveness[(int)attackDict[attackID].Item4, (int)typeSet.Item2];
                    return (multiplier1 * multiplier2);
                }
            }
            else
                return 1;
        }

        public static bool CanBeHit(int attackID,
                                   Tuple<Element.Type, Element.Type, Element.Type> typeSet,
                                   float npcai0,
                                   Dictionary<int, Tuple<Element.Type, Element.Type, Element.Type, Element.Type>> attackDict)
        {
            if (attackDict.ContainsKey(attackID))
            {
                if (attackID == NPCID.Retinazer || attackID == NPCID.Spazmatism)
                {
                    if (npcai0 >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item2];
                        if (multiplier1 * multiplier2 == 0) return false;
                        return true;
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item2];
                        if (multiplier1 * multiplier2 == 0) return false;
                        return true;
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)attackDict[attackID].Item4, (int)typeSet.Item1];
                    float multiplier2 = Table.Effectiveness[(int)attackDict[attackID].Item4, (int)typeSet.Item2];
                    if (multiplier1 * multiplier2 == 0) return false;
                    return true;
                }
            }
            else
                return true;
        }
    }
}
