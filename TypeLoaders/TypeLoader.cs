using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;

namespace TerraTyping.TypeLoaders;

public abstract class TypeLoader : ILoadable
{
    protected static class ColumnToIndex
    {
        public const int A = 0;
        public const int B = 1;
        public const int C = 2;
        public const int D = 3;
        public const int E = 4;
        public const int F = 5;
        public const int G = 6;
        public const int H = 7;
        public const int I = 8;
        public const int J = 9;
        public const int K = 10;
        public const int L = 11;
        public const int M = 12;
        public const int N = 13;
        public const int O = 14;
        public const int P = 15;
        public const int Q = 16;
        public const int R = 17;
        public const int S = 18;
        public const int T = 19;
        public const int U = 20;
        public const int V = 21;
        public const int W = 22;
        public const int X = 23;
        public const int Y = 24;
        public const int Z = 25;
    }

    public Mod Mod { get; private set; }

    public static ILog Logger { get; internal set; }

    public static bool IsLoadingTypes { get; internal set; }

    void ILoadable.Load(Mod mod)
    {
        Mod = mod;
        Load();
    }

    void ILoadable.Unload()
    {
        Unload();
    }

    protected abstract string CSVFileName { get; }

    protected abstract void InitTypeInfoCollection();

    public void SetupTypes()
    {
        InitTypeInfoCollection();
        LoadFile();
    }

    private void LoadFile()
    {
        string fileLocation = $"CsvTypes/Vanilla/{CSVFileName}.csv";

        if (Mod is null)
        {
            Logger.Error($"{nameof(Mod)} is null. Loading cancelled.");
            return;
        }

        if (!Mod.FileExists(fileLocation))
        {
            Logger.Error($"Type Loader: Unable to find file: \'{fileLocation}\'");
            return;
        }

        using Stream stream = Mod.GetFileStream(fileLocation, true); // newFileStream must be true otherwise it crashes
        StreamReader streamReader = new StreamReader(stream);

        int lineCount = 0;
        string line;

        //int nameColumn = -1;
        //List<int> defenseColumns = new List<int>();
        //List<int> offenseColumns = new List<int>();
        //List<int> abilityBasicColumns = new List<int>();
        //int abilityHiddenColumn = -1;
        //string modifyTypeByEnvironmentMethodNameColumn = null;

        line = ReadLineAndCount(streamReader, ref lineCount);
        //if (line is not null)
        //{
        //    // first line, init columns
        //    string[] cells = line.Split(',');
        //    for (int i = 0; i < cells.Length; i++)
        //    {
        //        string cell = cells[i];
        //        switch (cell)
        //        {
        //            case "name":
        //                nameColumn = i;
        //                break;
        //            case "defType":
        //                defenseColumns.Add(i);
        //                break;
        //        }
        //    }
        //}

        while ((line = ReadLineAndCount(streamReader, ref lineCount)) is not null)
        {
            string[] cells = CSVParser(line, out bool allLinesAreNullOrWhiteSpace);
            if (!allLinesAreNullOrWhiteSpace)
            {
                try
                {
                    ParseLine(line, cells, lineCount);
                }
                catch (Exception e)
                {
                    Logger.Error($"Threw exception while parsing {CSVFileName}: Row: '{line}':line {lineCount}", e);
                }
            }
        }
    }

    public virtual void Load() { }

    public virtual void Unload() { }

    protected abstract void ParseLine(string line, string[] cells, int lineCount);

    protected static ElementArray ParseAtLeastOneElement(string[] strings)
    {
        List<Element> tempList = new List<Element>(strings.Length);
        for (int i = 0; i < strings.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(strings[i]))
            {
                if (!Enum.TryParse(strings[i], true, out Element element))
                {
                    throw new Exception($"Unable to parse '{strings[i]}' to type '{nameof(Element)}'");
                }
                tempList.Add(element);
            }
        }

        if (tempList.Count == 0)
        {
            throw new ParsedNoneException(strings);
        }

        return ElementArray.Get(tempList.ToArray());
    }

    protected static AbilityContainer ParseAbilities(string ability1, string ability2, string hiddenAbility)
    {
        const string ErrorMessage = $"Unable to parse '{{0}}' to type '{nameof(AbilityID)}'";

        string[] abilityStrings = new string[] { ability1, ability2, hiddenAbility };
        AbilityID[] abilityIDs = new AbilityID[3];
        for (int i = 0; i < abilityStrings.Length; i++)
        {
            string str = abilityStrings[i].Replace(" ", string.Empty);
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (!Enum.TryParse(str, true, out AbilityID abilityID))
                {
                    throw new Exception(string.Format(ErrorMessage, str));
                }

                abilityIDs[i] = abilityID;
            }
        }

        return new AbilityContainer(abilityIDs[0], abilityIDs[1], abilityIDs[2]);
    }

    private static string[] CSVParser(string line, out bool allLinesAreNullOrWhiteSpace)
    {
        if (line.Length == 0)
        {
            allLinesAreNullOrWhiteSpace = true;
            return Array.Empty<string>();
        }
        else if (line.Length == 1)
        {
            if (line[0] is ',')
            {
                allLinesAreNullOrWhiteSpace = true;
                return new string[] { "", "" };
            }
            else
            {
                allLinesAreNullOrWhiteSpace = false;
                return new string[] { line };
            }
        }

        allLinesAreNullOrWhiteSpace = true;
        List<string> cells = new List<string>();
        StringBuilder stringBuilder = new StringBuilder();

        bool verbatimMode = false;
        char cNext = default;
        for (int i = 0; i + 1 < line.Length; i++)
        {
            char c = line[i];
            cNext = line[i + 1];
            if (c is not ',' || verbatimMode)
            {
                if (c is '"')
                {
                    if (cNext is '"')
                    {
                        stringBuilder.Append('"');
                        i++;
                        allLinesAreNullOrWhiteSpace = false;
                    }
                    else
                    {
                        verbatimMode = !verbatimMode;
                    }
                }
                else
                {
                    stringBuilder.Append(c);
                    allLinesAreNullOrWhiteSpace = false;
                }
            }
            else
            {
                cells.Add(stringBuilder.ToString());
                stringBuilder.Clear();
            }
        }

        // simplified version of the loop because it's the end
        if (cNext is ',')
        {
            cells.Add(stringBuilder.ToString());
            stringBuilder.Clear();
        }
        else if (cNext is not '"')
        {
            allLinesAreNullOrWhiteSpace = false;
            stringBuilder.Append(cNext);
        }

        cells.Add(stringBuilder.ToString());

        return cells.ToArray();
    }

    private static string ReadLineAndCount(StreamReader streamReader, ref int lineCount)
    {
        lineCount++;
        return streamReader.ReadLine();
    }

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
