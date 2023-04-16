using System;

namespace TerraTyping.TypeLoaders;

public abstract partial class TypeLoader
{
    [Serializable]
    public class ParsedNoneException : Exception
    {
        public ParsedNoneException() { }
        public ParsedNoneException(string message) : base(message) { }
        public ParsedNoneException(string[] strings) : base(MessageMaker(strings)) { }
        public ParsedNoneException(string message, Exception inner) : base(message, inner) { }
        public ParsedNoneException(string[] strings, Exception inner) : base(MessageMaker(strings), inner) { }
        protected ParsedNoneException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        private static string MessageMaker(string[] strings)
        {
            return $"Parsed no elements from provided strings: [{string.Join(",", strings)}].";
        }
    }
}
