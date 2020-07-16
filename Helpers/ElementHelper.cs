using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TerraTyping
{
    public static class ElementHelper
    {
        static readonly Mod weaponOut = ModLoader.GetMod("WeaponOut");

        public static Element Primary(object obj)
        {
            Element element = Element.none;
            if (obj is Tuple<Element, Element, Element> typeSet)
            {
                element = typeSet.Item1;
            }
            else if (obj is NPC npc)
            {
                if (DictionaryHelper.NPC(npc).ContainsKey(npc.type))
                    element = DictionaryHelper.NPC(npc)[npc.type].Item1;
            }
            return element;
        }

        public static Element Secondary(object obj)
        {
            Element element = Element.none;
            if (obj is Tuple<Element, Element, Element> typeSet)
            {
                element = typeSet.Item2;
            }
            else if (obj is NPC npc)
            {
                if (DictionaryHelper.NPC(npc).ContainsKey(npc.type))
                    element = DictionaryHelper.NPC(npc)[npc.type].Item2;
            }
            return element;
        }

        public static Element Tertiary(object obj)
        {
            Element element = Element.none;
            if (obj is Tuple<Element, Element, Element> typeSet)
            {
                element = typeSet.Item3;
            }
            else if (obj is NPC npc)
            {
                if (DictionaryHelper.NPC(npc).ContainsKey(npc.type))
                    element = DictionaryHelper.NPC(npc)[npc.type].Item3;
            }
            return element;
        }

        public static Element Quatrinary(object obj)
        {
            Element element = Element.none;
            if (obj is Item item)
            {
                if (DictionaryHelper.Item(item).ContainsKey(item.type))
                    element = DictionaryHelper.Item(item)[item.type];
                else if (DictionaryHelper.Ammo(item).ContainsKey(item.type))
                    element = DictionaryHelper.Ammo(item)[item.type];
            }
            else if (obj is NPC npc)
            {
                if (DictionaryHelper.NPC(npc).ContainsKey(npc.type))
                    element = DictionaryHelper.NPC(npc)[npc.type].Item4;
            }
            else if (obj is Projectile proj)
            {
                if (DictionaryHelper.Projectile(proj).ContainsKey(proj.type))
                    element = DictionaryHelper.Projectile(proj)[proj.type];
            }
            else if (obj is PlayerDeathReason pdr)
            {
                if (DictionaryHelper.Other(pdr).ContainsKey(pdr.SourceOtherIndex))
                    element = DictionaryHelper.Other(pdr)[pdr.SourceOtherIndex];
            }
            else if (obj is int buff)
            {
                if (DictionaryHelper.Buff(buff).ContainsKey(buff))
                    element = DictionaryHelper.Buff(buff)[buff];
            }
            return element;
        }

        public static bool Any(object obj, Element element)
        {
            if (Primary(obj) == element || Secondary(obj) == element || Tertiary(obj) == element || Quatrinary(obj) == element)
                return true;
            return false;
        }

        //public struct GetElement
        //{
        //    public Element Primary;
        //    public Element Secondary;
        //    public Element Tertiary;
        //    public Element Quatrinary;
        //    public GetElement(Item item)
        //    {
        //        Primary = Element.none;
        //        Secondary = Element.none;
        //        Tertiary = Element.none;
        //        Quatrinary = Element.none;
        //        if (item.modItem.mod == weaponOut)
        //        {
        //            Items.WeaponOut.TryGetValue(item.type, out Quatrinary);
        //        }
        //        else
        //        {
        //            Vanilla.Ammos.TryGetValue(item.type, out Quatrinary);
        //            Items.Type.TryGetValue(item.type, out Quatrinary);
        //        }
        //    }
        //    public GetElement(NPC npc)
        //    {
        //        Primary = Element.none;
        //        Secondary = Element.none;
        //        Tertiary = Element.none;
        //        Quatrinary = Element.none;
        //        if (npc.modNPC.mod == weaponOut)
        //        {
        //        }
        //        else if (npc.type == NPCID.Retinazer && npc.type == NPCID.Spazmatism)
        //        {
        //            if (npc.ai[0] >= 2)
        //            {
        //                Primary = Element.steel;
        //                Secondary = Element.flying;
        //                Tertiary = Element.none;
        //                Quatrinary = Element.steel;
        //            }
        //            else
        //            {
        //                Primary = Element.normal;
        //                Secondary = Element.flying;
        //                Tertiary = Element.none;
        //                Quatrinary = Element.normal;
        //            }
        //        }
        //        else if (Enemies.Type.ContainsKey(npc.type))
        //        {
        //            Primary = Enemies.Type[npc.type].Item1;
        //            Secondary = Enemies.Type[npc.type].Item2;
        //            Tertiary = Enemies.Type[npc.type].Item3;
        //            Quatrinary = Enemies.Type[npc.type].Item4;
        //        }
        //    }
        //}
    }
}
