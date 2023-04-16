using System;
using System.Collections.Generic;

namespace TerraTyping.TypeLoaders;

public class LineParser
{
    private readonly List<Column> columns;

    public LineParser(List<Column> columns)
    {
        this.columns = columns;
    }

    public int GetIndex(string key)
    {
        for (int i = 0; i < columns.Count; i++)
        {
            if (columns[i].MatchesKey(key))
            {
                return columns[i].GetIndex();
            }
        }

        throw new KeyNotFoundException($"Could not find index that matches '{key}'");
    }

    public Range GetRange(string key)
    {
        for (int i = 0; i < columns.Count; i++)
        {
            if (columns[i].MatchesKey(key))
            {
                return columns[i].GetRange();
            }
        }

        throw new KeyNotFoundException($"Could not find range that matches '{key}'");
    }

    public int GetIndexOrDefault(string key, int defaultIndex)
    {
        for (int i = 0; i < columns.Count; i++)
        {
            if (columns[i].MatchesKey(key))
            {
                return columns[i].GetIndex();
            }
        }

        return defaultIndex;
    }

    public Range GetRangeOrDefault(string key, Range defaultRange)
    {
        for (int i = 0; i < columns.Count; i++)
        {
            if (columns[i].MatchesKey(key))
            {
                return columns[i].GetRange();
            }
        }

        return defaultRange;
    }

    public bool TryGetIndex(string key, out int value)
    {
        for (int i = 0; i < columns.Count; i++)
        {
            if (columns[i].MatchesKey(key))
            {
                value = columns[i].GetIndex();
                return true;
            }
        }

        value = default;
        return false;
    }

    public bool TryGetRange(string key, out Range value)
    {
        for (int i = 0; i < columns.Count; i++)
        {
            if (columns[i].MatchesKey(key))
            {
                value = columns[i].GetRange();
                return true;
            }
        }

        value = default;
        return false;
    }
}