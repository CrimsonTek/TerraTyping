using System;
using System.Collections.Generic;
using System.Linq;
using TerraTyping.Helpers;

namespace TerraTyping.TypeLoaders;

public abstract partial class TypeLoader
{
    protected class HeaderParser
    {
        private readonly List<Header> expectedHeaders;

        public HeaderParser()
        {
            expectedHeaders = new List<Header>();
        }

        public HeaderParser NewIndexHeader(string key, bool required)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            expectedHeaders.Add(new IndexHeader(key, required));

            return this;
        }

        public HeaderParser NewRangeHeader(string key, bool required)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            expectedHeaders.Add(new RangeHeader(key, required));

            return this;
        }

        public bool ParseHeader(ParseContext context, out LineParser lineParser, TypeLoader typeLoader)
        {
            List<Column> columns = new List<Column>();
            List<Header> expectedHeadersCopy = new List<Header>(expectedHeaders);
            for (int i = 0; i < context.Cells.Length; i++)
            {
                string key = context.Cells[i].ToLower();

                if (string.IsNullOrEmpty(context.Cells[i]))
                {
                    Logger.Log(Verbosity.Warn, $"HeaderParser/{typeLoader.GetType().Name}", $"Header {ColumnToIndex.IndexToColumn(i)} is blank. Add a header or remove unnecessary columns. File: '{context.FileName}'");
                    
                    continue;
                }

                if (key.GetOrNull(0) is '-')
                {
                    Logger.Log(Verbosity.Warn, $"HeaderParser/{typeLoader.GetType().Name}", $"Column with header \"{context.Cells[i]}\" will not be used and should be removed. File: '{context.FileName}'");
                    continue;
                }

                bool foundHeader = false;
                for (int j = 0; j < expectedHeadersCopy.Count; j++)
                {
                    Header header = expectedHeadersCopy[j];
                    if (header.ContainsKey(key))
                    {
                        columns.Add(header.ToColumn(ref i, key, context.Cells, header.rawKey));
                        foundHeader = true;
                        expectedHeadersCopy.RemoveAt(j);
                        break;
                    }
                }

                if (foundHeader)
                {
                    continue;
                }

                IEnumerable<string> validHeaders2 = (expectedHeaders.Select(header => header.rawKey));
                Logger.Log(Verbosity.Warn, $"HeaderParser/{typeLoader.GetType().Name}", $"Unrecognized header \"{context.Cells[i]}\". Fix any typos or remove unnecessary columns. File: '{context.FileName}'.\n\tValid headers for this file type (multiple valid options are separated by '|'): '{string.Join("', '", validHeaders2)}'.");
            }

            bool missingRequiredHeaders = false;
            for (int i = 0; i < expectedHeadersCopy.Count; i++)
            {
                if (expectedHeadersCopy[i].required)
                {
                    Logger.Log(Verbosity.Error, $"HeaderParser/{typeLoader.GetType().Name}", $"Missing required header '{expectedHeadersCopy[i].splitKeys.First()}'.");
                    missingRequiredHeaders = true;
                }
            }

            lineParser = new LineParser(columns);
            return !missingRequiredHeaders;
        }

        private static string[] Split(string key)
        {
            return key.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        class IndexHeader : Header
        {
            public IndexHeader(string rawKey, bool required) : base(rawKey, required) { }

            public override Column ToColumn(ref int i, string key, string[] cells, string masterKey)
            {
                return new IndexColumn(i, masterKey);
            }
        }

        class RangeHeader : Header
        {
            public RangeHeader(string rawKey, bool required) : base(rawKey, required) { }

            public override Column ToColumn(ref int i, string key, string[] cells, string masterKey)
            {
                int rangeStart = i;
                while ((i + 1 < cells.Length) && (cells[i + 1].Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    i++;
                }
                return new RangeColumn(rangeStart..(i + 1), masterKey);
            }
        }

        abstract class Header
        {
            public bool required;
            public string[] splitKeys;
            public string rawKey;

            public Header(string rawKey, bool required)
            {
                this.required = required;
                splitKeys = Split(rawKey);
                this.rawKey = rawKey;
            }

            public abstract Column ToColumn(ref int i, string key, string[] cells, string masterKey);
            public bool ContainsKey(string key)
            {
                return splitKeys.Contains(key);
            }
        }
    }
}


class IndexColumn : Column
{
    public int Index { get; set; }

    public IndexColumn(int index, string key) : base(key)
    {
        Index = index;
    }

    public override int GetIndex() => Index;
    public override Range GetRange() => throw new Exception("Tried to get range from an index column.");
}

class RangeColumn : Column
{
    public Range Range { get; set; }

    public RangeColumn(Range range, string key) : base(key)
    {
        Range = range;
    }

    public override int GetIndex() => throw new Exception("Tried to get index from a range column.");
    public override Range GetRange() => Range;
}

public abstract class Column
{
    private readonly string key;

    public Column(string key)
    {
        this.key = key;
    }

    public bool MatchesKey(string key)
    {
        return this.key.Equals(key, StringComparison.OrdinalIgnoreCase);
    }

    public abstract int GetIndex();
    public abstract Range GetRange();
}