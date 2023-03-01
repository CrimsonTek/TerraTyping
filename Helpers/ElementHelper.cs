using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.Helpers
{
    public static class ElementHelper
    {
        static bool hasBeenInit;
        static Element[] allElementsIncludeNone;
        static Element[] allElementsExcludeNone;

        public static Element[] AllElementsIncludeNone
        {
            get
            {
                if (!hasBeenInit)
                {
                    Init();
                }

                return allElementsIncludeNone;
            }
            private set => allElementsIncludeNone = value;
        }
        public static Element[] AllElementsExcludeNone
        {
            get
            {
                if (!hasBeenInit)
                {
                    Init();
                }

                return allElementsExcludeNone;
            }
            private set => allElementsExcludeNone = value;
        }

        public static Element FromIndex(int i)
        {
            return AllElementsIncludeNone[i];
        }

        public static Element[] GetAll(bool includeNone)
        {
            if (includeNone)
            {
                return AllElementsIncludeNone;
            }
            else
            {
                return AllElementsExcludeNone;
            }
        }

        public static Element[] GetAllDontIncludeNone()
        {
            return AllElementsExcludeNone;
        }

        public static Element[] GetAllIncludeNone()
        {
            return AllElementsIncludeNone;
        }

        public static int ElementCount(bool includeNone)
        {
            if (includeNone)
            {
                return AllElementsIncludeNone.Length;
            }
            else
            {
                return AllElementsExcludeNone.Length;
            }
        }

        private static void Init()
        {
            AllElementsIncludeNone = Enum.GetValues<Element>();
            AllElementsExcludeNone = Enum.GetValues<Element>().Where((element) => element is not Element.none).ToArray();
            hasBeenInit = true;
        }

        internal static void Load()
        {
            Init();
        }

        internal static void Unload()
        {
            AllElementsIncludeNone = null;
            AllElementsExcludeNone = null;
        }
    }
}
