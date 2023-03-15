using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Terraria.ModLoader;

namespace TerraTyping.DataTypes
{
    public class ElementArray : IEnumerable<Element>
    {
        /// <summary>
        /// Do not modify.
        /// </summary>
        Element[] Elements { get; }

        private static Dictionary<Element[], WeakReference<ElementArray>> instantiatedElementArrays;
        internal static int uniqueElementArrays;
        internal static int totalElementArraysUsed;

        public Element this[int index] => Elements[index];

        public int Length { get; }

        public bool Empty { get; }

        /// <summary>
        /// An empty array.
        /// </summary>
        public static ElementArray Default { get; private set; }

        internal static void Load()
        {
            ElementArray @default = new ElementArray();
            Default = @default;
            instantiatedElementArrays = new Dictionary<Element[], WeakReference<ElementArray>>(new ElementArrayComparer())
            {
                { Array.Empty<Element>(), new WeakReference<ElementArray>(@default) }
            };
            uniqueElementArrays = 0;
            totalElementArraysUsed = 0;
        }

        internal static void Unload()
        {
            instantiatedElementArrays = null;
            uniqueElementArrays = 0;
            totalElementArraysUsed = 0;
        }

        internal static void Clean(bool log = true, bool forceGCCollect = true)
        {
            if (forceGCCollect)
            {
                GC.Collect();
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Element[]> unusedKVPs = new List<Element[]>();
            foreach ((Element[] key, WeakReference<ElementArray> value) in instantiatedElementArrays)
            {
                if (!value.TryGetTarget(out _))
                {
                    unusedKVPs.Add(key);
                }
            }

            for (int i = 0; i < unusedKVPs.Count; i++)
            {
                instantiatedElementArrays.Remove(unusedKVPs[i]);

                if (log && unusedKVPs.Count < 10)
                {
                    TerraTyping.Instance.Logger.Debug($"Removing {{{unusedKVPs[i]}}}");
                }
            }

            stopwatch.Stop();

            if (log)
            {
                TerraTyping.Instance.Logger.Info($"Removed {unusedKVPs.Count} collected references of ElementArrays in {stopwatch.Elapsed.TotalMilliseconds} milliseconds.");
            }
        }

        private ElementArray(params Element[] elements)
        {
            if (elements is null || elements.Length == 0)
            {
                Elements = Array.Empty<Element>();
                Length = 0;
                Empty = true;
                return;
            }

            Elements = new Element[elements.Length];
            Length = elements.Length;
            Empty = false;

            for (int i = 0; i < Length; i++)
            {
                Elements[i] = elements[i];
            }

            int elementHash = 0;
            for (int i = 0; i < Length; i++)
            {
                if ((elementHash & (0b1 << (int)elements[i])) != 0)
                {
                    throw new ArgumentException($"Argument {nameof(elements)} ({string.Join(", ", elements)}) has multiple of the same Element in it: {elements[i]}.");
                }

                elementHash |= (0b1 << (int)elements[i]);
            }
        }

        public bool HasElement(Element element)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Elements[i] == element)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasAnyElement(Element element1, Element element2)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Elements[i] == element1 ||
                    Elements[i] == element2)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Searches for each element, and returns true if any is found. <paramref name="selected"/> is the first element found.
        /// </summary>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        /// <param name="selected">The first element found.</param>
        /// <returns>Returns true if any element is found.</returns>
        public bool HasAnyElement(Element element1, Element element2, out Element selected)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Elements[i] == element1)
                {
                    selected = element1;
                    return true;
                }

                if (Elements[i] == element2)
                {
                    selected = element2;
                    return true;
                }
            }

            selected = Element.none;
            return false;
        }

        public bool HasAnyElement(Element element1, Element element2, Element element3)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Elements[i] == element1 ||
                    Elements[i] == element2 ||
                    Elements[i] == element3)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasAnyElement(Element element1, Element element2, Element element3, out Element selected)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Elements[i] == element1)
                {
                    selected = element1;
                    return true;
                }

                if (Elements[i] == element2)
                {
                    selected = element2;
                    return true;
                }

                if (Elements[i] == element3)
                {
                    selected = element3;
                    return true;
                }
            }

            selected = Element.none;
            return false;
        }

        public bool HasAnyElement(Element element1, Element element2, Element element3, Element element4)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Elements[i] == element1 ||
                    Elements[i] == element2 ||
                    Elements[i] == element3 ||
                    Elements[i] == element4)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasAnyElement(Element[] elements)
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < elements.Length; j++)
                {
                    if (Elements[i] == elements[j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public float DamageFrom(Element element, bool scaled)
        {
            float f = 1;
            for (int i = 0; i < Length; i++)
            {
                if (scaled)
                {
                    f *= Table.EffectivenessScaled(element, Elements[i]);
                }
                else
                {
                    f *= Table.EffectivenessUnscaled(element, Elements[i]);
                }
            }
            return f;
        }

        public bool ExactMatch(ElementArray compareTo, bool ignoreOrder = true)
        {
            if (ignoreOrder)
            {
                return ExactMatchIgnoreOrder(compareTo);
            }
            else
            {
                return ExactMatchOrder(compareTo);
            }
        }

        private bool ExactMatchIgnoreOrder(ElementArray compareTo)
        {
            if (Length != compareTo.Length)
            {
                return false;
            }

            int firstHash = 0;
            for (int i = 0; i < Length; i++)
            {
                firstHash |= (0b1 << (int)Elements[i]);
            }

            int secondHash = 0;
            for (int i = 0; i < compareTo.Length; i++)
            {
                secondHash |= (0b1 << (int)compareTo.Elements[i]);
            }

            return firstHash == secondHash;
        }

        private bool ExactMatchOrder(ElementArray compareTo)
        {
            if (Length != compareTo.Length)
            {
                return false;
            }

            for (int i = 0; i < Length; i++)
            {
                if (Elements[i] != compareTo.Elements[i])
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<Element> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return Elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return Elements[i];
            }
        }

        public string ToStringEach() => string.Join(", ", Elements);

        public static ElementArray Get(params Element[] elements)
        {
            // todo: remove 'Element.none's? Check for length?

            ElementArray elementArray;
            if (instantiatedElementArrays.TryGetValue(elements, out WeakReference<ElementArray> weakReference))
            {
                if (weakReference.TryGetTarget(out elementArray))
                {
                    return elementArray;
                }
                else
                {
                    elementArray = new ElementArray(elements);
                    weakReference.SetTarget(elementArray);
                    return elementArray;
                }
            }
            else
            {
                elementArray = new ElementArray(elements);
                instantiatedElementArrays[elements] = new WeakReference<ElementArray>(elementArray);
                return elementArray;
            }
        }

        struct ElementArrayComparer : IEqualityComparer<Element[]>
        {
            public bool Equals(Element[] x, Element[] y)
            {
                if (x.Length != y.Length)
                {
                    return false;
                }

                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetHashCode([DisallowNull] Element[] obj)
            {
                // 32 bits, groups of 8: (supports 4 elements)
                // 00000000 00000000 00000000 00000000
                // 32 bits, groups of 5: (supports 6 elements)
                // 00 00000 00000 00000 00000 00000 00000
                // 32 bits, groups of 6: (supports 5 elements)
                // 00 000000 000000 000000 000000 000000

                const int ElementBitLength = 5; // with 32 or more element types, this should be changed to 6

                int hash = 0;
                for (int i = 0; i < obj.Length; i++)
                {
                    hash ^= (((int)obj[i] + 1) << (i * ElementBitLength));
                }
                return hash;
            }
        }
    }
}
