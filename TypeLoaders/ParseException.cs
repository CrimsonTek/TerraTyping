using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.TypeLoaders;

[Serializable]
public class ParseException : Exception
{
    public ParseException(string message, ParseContext context) : base(MessageFixer(message, context)) { }

    static string MessageFixer(string oldMessage, ParseContext context)
    {
        string sourceString = context.ModSource is not null ? $"Mod Source: {context.ModSource}" : $"Source: User";
        return $"{oldMessage}\r\n" +
            $"  File Name: {context.FileName}\r\n" +
            $"  Line Number: {context.LineCount}\r\n" +
            $"  Line Content: {context.Line}\r\n" +
            $"  {sourceString}";
    }
}