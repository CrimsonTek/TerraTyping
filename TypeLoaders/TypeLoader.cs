using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;
using Terraria.ModLoader;
using TerraTyping.Core;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;

namespace TerraTyping.TypeLoaders;

public abstract partial class TypeLoader : ILoadable
{
    public Mod TerraTyping { get; private set; }
    protected ParseContext Context { get; private set; } = new ParseContext();
    public static ILog Logger { get; internal set; }

    void ILoadable.Load(Mod mod)
    {
        TerraTyping = mod;
        Load();
    }
    void ILoadable.Unload()
    {
        Unload();
    }

    /// <summary>
    /// Do not include ".csv"
    /// </summary>
    protected abstract string CSVFileName { get; }

    public static void SetupAllDefaultVanillaTypes()
    {
        AmmoTypeLoader.Instance.LoadVanillaTypes();
        ArmorTypeLoader.Instance.LoadVanillaTypes();
        NPCTypeLoader.Instance.LoadVanillaTypes();
        ProjectileTypeLoader.Instance.LoadVanillaTypes();
        SpecialItemTypeLoader.Instance.LoadVanillaTypes();
        WeaponTypeLoader.Instance.LoadVanillaTypes();
    }
    public static void SetupAllDefaultModTypes()
    {
        foreach (Mod mod in ModLoader.Mods)
        {
            AmmoTypeLoader.Instance.LoadModTypes(mod);
            ArmorTypeLoader.Instance.LoadModTypes(mod);
            NPCTypeLoader.Instance.LoadModTypes(mod);
            ProjectileTypeLoader.Instance.LoadModTypes(mod);
            SpecialItemTypeLoader.Instance.LoadModTypes(mod);
            WeaponTypeLoader.Instance.LoadModTypes(mod);
        }
    }
    public static void SetupAllUserOverrides()
    {
        AmmoTypeLoader.Instance.LoadOverrideTypes();
        ArmorTypeLoader.Instance.LoadOverrideTypes();
        NPCTypeLoader.Instance.LoadOverrideTypes();
        ProjectileTypeLoader.Instance.LoadOverrideTypes();
        SpecialItemTypeLoader.Instance.LoadOverrideTypes();
        WeaponTypeLoader.Instance.LoadOverrideTypes();
    }

    private void LoadVanillaTypes()
    {
        LoadUltimate(TerraTyping, $"CsvTypes\\Vanilla\\{CSVFileName}.csv", null, true);
    }
    private void LoadModTypes(Mod mod)
    {
        LoadUltimate(TerraTyping, $"CsvTypes\\{mod.Name}\\{CSVFileName}.csv", mod, false);
    }
    private void LoadOverrideTypes()
    {
        string curDir = Directory.GetCurrentDirectory();
        string terraTypingFolderPath = $"{curDir}\\ModContent\\TerraTyping";
        if (!Directory.Exists(terraTypingFolderPath))
        {
            return;
        }

        string[] files = Directory.GetFiles(terraTypingFolderPath, $"*{CSVFileName}*.csv");
        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            string subString = file[($"{terraTypingFolderPath}\\{CSVFileName}").Length..^4];

            if (!File.Exists(file))
            {
                Logger.Log(Verbosity.Error, GetType().Name, $"File '{file}' not found.");
                continue;
            }

            if (subString.Equals("Override"))
            {
                LoadUltimate(null, file, null, true);
            }
            else if (ModLoader.TryGetMod(subString, out Mod mod))
            {
                LoadUltimate(null, file, mod, true);
            }
            else
            {
                Logger.Log(Verbosity.Warn, GetType().Name, $"File not read. Expected '{CSVFileName}(mod name or 'Override').csv'. No mod named '{subString}' could be found. If the mod is not loaded, you may disregard this message.", ("File Name", file));
            }
        }
    }
    internal void LoadOtherTypesFromCall(Mod callingMod, string fileName, Mod modToGiveTypes)
    {
        LoadUltimate(callingMod, fileName, modToGiveTypes, true);
    }

    private void LoadUltimate(Mod getFileFrom, string fileName, Mod modTargetForTypes, bool logMissingFile)
    {
        Context.FileName = fileName;
        Context.LineCount = -1;
        Context.ModSource = getFileFrom;

        StreamReader streamReader;
        if (getFileFrom is not null)
        {
            if (!getFileFrom.FileExists(fileName))
            {
                if (logMissingFile)
                {
                    Logger.Log(Verbosity.Error, GetType().Name, $"Unable to find file: '{fileName}' from mod {getFileFrom}");
                }

                return;
            }

            Stream stream = getFileFrom.GetFileStream(fileName, true); // newFileStream must be true otherwise it crashes
            streamReader = new StreamReader(stream);
        }
        else
        {
            if (!File.Exists(fileName))
            {
                if (logMissingFile)
                {
                    throw new ParseException("Unable to find file.", Context);
                }
            }

            streamReader = new StreamReader(File.OpenRead(fileName));
        }

        int parseCount = 0;
        LineParser lineParser = default;
        int lineCount = -1;
        string line;
        while ((line = ReadLineAndCount(streamReader, ref lineCount)) is not null)
        {
            Context.LineCount = lineCount;
            Context.Line = line;
            string[] cells = CSVParser(line, out bool allLinesAreNullOrWhiteSpace);
            if (allLinesAreNullOrWhiteSpace)
            {
                continue;
            }
            Context.Cells = cells;

            if (lineCount == 0)
            {
                if (!ParseHeader(cells, fileName, out lineParser))
                {
                    Logger.Log(Verbosity.Error, GetType().Name, $"Failed to parse header for file '{fileName}'. This file will be skipped.");
                    break;
                }

                continue;
            }

            try
            {
                if (modTargetForTypes is null)
                {
                    if (ParseLine(lineParser))
                    {
                        parseCount++;
                    }
                }
                else
                {
                    if (ParseLineMod(modTargetForTypes, lineParser))
                    {
                        parseCount++;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(Verbosity.Error, GetType().Name, $"Threw exception while parsing {CSVFileName}: Row (#{lineCount}): '{line}'.", e);
            }
        }
        Logger.Log(Verbosity.Info, GetType().Name, $"Parsed {parseCount} lines from '{fileName}'.");
        streamReader.Dispose();
    }

    public virtual void Load() { }
    public virtual void Unload() { }
    public abstract void InitTypeInfoCollection();
    protected abstract bool ParseLine(LineParser lineParser);
    protected abstract bool ParseLineMod(Mod modToGiveTypes, LineParser lineParser);
    protected abstract bool ParseHeader(string[] cells, string fileName, out LineParser lineParser);
    protected (SpecialTooltip[] specialTooltips, bool overrideSpecialTooltip) GetSpecialTooltips(string[] cells, LineParser lineParser)
    {
        if (lineParser.TryGetIndex(HeaderKeys.SpecialTooltip, out int specialTooltipsCellIndex))
        {
            return ItemTypeLoaderUtils.GetSpecialTooltips(Context.Cells[specialTooltipsCellIndex]);
        }
        else
        {
            return (null, false);
        }
    }
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
    protected static bool TryParseAtLeastOneElement(string[] strings, out ElementArray elements, out string error)
    {
        List<Element> tempList = new List<Element>(strings.Length);
        for (int i = 0; i < strings.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(strings[i]))
            {
                if (!Enum.TryParse(strings[i], true, out Element element))
                {
                    elements = ElementArray.Default;
                    error = $"Unable to parse '{strings[i]}' to type '{nameof(Element)}'";
                    return false;
                }
                tempList.Add(element);
            }
        }

        if (tempList.Count == 0)
        {
            elements = ElementArray.Default;
            error = "Parsed no elements.";
            return false;
        }

        elements = ElementArray.Get(tempList.ToArray());
        error = string.Empty;
        return true;
    }
    protected bool TryParseAtLeastOneElement(string[] strings, out ElementArray elements)
    {
        if (!TryParseAtLeastOneElement(strings, out elements, out string error))
        {
            Logger.Error(
                $"An error occured while parsing line \"{Context.Line}\" in file \"{Context.FileName}\":\n" +
                $"\t{error}");
            return false;
        }

        return true;
    }
    protected static AbilityContainer ParseAbilities(string[] basicAbilityStrings, string[] hiddenAbilityStrings)
    {
        List<Ability> basicAbilities = new List<Ability>();
        List<Ability> hiddenAbilities = new List<Ability>();
        foreach (string abilityStr in basicAbilityStrings)
        {
            ParseAbility(basicAbilities, abilityStr, true);
        }
        foreach (string abilityStr in hiddenAbilityStrings)
        {
            ParseAbility(hiddenAbilities, abilityStr, true);
        }
        return new AbilityContainer(basicAbilities.ToArray(), hiddenAbilities.ToArray());
    }
    protected static bool TryParseAbilities(string[] basicAbilityStrings, string[] hiddenAbilityStrings, out AbilityContainer abilityContainer)
    {
        List<Ability> basicAbilities = new List<Ability>();
        List<Ability> hiddenAbilities = new List<Ability>();
        foreach (string abilityStr in basicAbilityStrings)
        {
            if (!ParseAbility(basicAbilities, abilityStr, false))
            {
                abilityContainer = default;
                return false;
            }
        }
        foreach (string abilityStr in hiddenAbilityStrings)
        {
            if (!ParseAbility(hiddenAbilities, abilityStr, false))
            {
                abilityContainer = default;
                return false;
            }
        }
        abilityContainer = new AbilityContainer(basicAbilities.ToArray(), hiddenAbilities.ToArray());
        return true;
    }
    private static bool ParseAbility(List<Ability> abilityList, string abilityStr, bool throwIfCantParseToAbilityID)
    {
        const string ErrorMessage = $"Unable to parse '{{0}}' to type '{nameof(Ability)}'";
        string str = abilityStr.Replace(" ", string.Empty);
        if (string.IsNullOrWhiteSpace(str))
        {
            return false;
        }

        if (!Enum.TryParse(str, true, out Ability result))
        {
            if (throwIfCantParseToAbilityID)
            {
                throw new ArgumentException(string.Format(ErrorMessage, str));
            }
            else
            {
                return false;
            }
        }

        abilityList.Add(result);
        return true;
    }
    /// <param name="elementRange">Inclusive start, exclusive end.</param>
    protected bool TryParseLineGeneric<T>(Mod modToGiveTypes, Range elementRange, int internalNameCellIndex, out ElementArray elements, out T modType)
        where T : IModType
    {
        string internalName = Context.Cells.SafeGet(internalNameCellIndex);

        if (!modToGiveTypes.TryFind(internalName, out modType))
        {
            Logger.Error(
                $"An error occured while parsing line \"{Context.Line}\" in file \"{Context.FileName}\":\n" +
                $"\tInstance of {typeof(T)} named \"{internalName}\" could not be found.");
            elements = default;
            return false;
        }

        if (!TryParseAtLeastOneElement(Context.Cells.SafeGet(elementRange), out elements, out string error))
        {
            Logger.Error(
                $"An error occured while parsing line \"{Context.Line}\" in file \"{Context.FileName}\":\n" +
                $"\t{error}");
            return false;
        }

        return true;
    }
    /// <param name="elementRange">Inclusive start, exclusive end.</param>
    protected bool TryParseLineGeneric(Range elementRange, int internalNameCellIndex, out ElementArray elements, out int i)
    {
        string internalName = Context.Cells.SafeGet(internalNameCellIndex);

        if (!int.TryParse(internalName, out i))
        {
            Logger.Error(
                $"An error occured while parsing line \"{Context.Line}\" in file \"{Context.FileName}\":\n" +
                $"\tID '{internalName}' could not be parsed to int.");
            elements = default;
            return false;
        }

        if (!TryParseAtLeastOneElement(Context.Cells.SafeGet(elementRange), out elements, out string error))
        {
            Logger.Error(
                $"An error occured while parsing line \"{Context.Line}\" in file \"{Context.FileName}\":\n" +
                $"\t{error}");
            return false;
        }

        return true;
    }
    protected static bool TryGetHeaderIndex(string[] cells, int index, string headerTextToMatch)
    {
        if (cells[index].Equals(headerTextToMatch, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }
    protected static bool TryGetHeaderRange(string[] cells, ref int index, string headerTextToMatch, out int rangeStart, out int rangeEnd)
    {
        if (cells[index].Equals(headerTextToMatch, StringComparison.OrdinalIgnoreCase))
        {
            rangeStart = index;

            while ((index + 1 < cells.Length) && (cells[index].Equals(headerTextToMatch, StringComparison.OrdinalIgnoreCase)))
            {
                index++;
            }

            rangeEnd = index + 1;

            return true;
        }
        else
        {
            rangeStart = rangeEnd = 0;
        }

        return false;
    }
    protected static bool TryGetHeaderRange(string[] cells, ref int index, string headerText, out Range range)
    {
        if (cells[index].Equals(headerText, StringComparison.OrdinalIgnoreCase))
        {
            int rangeStart = index;

            while ((index + 1 < cells.Length) && (cells[index].Equals(headerText, StringComparison.OrdinalIgnoreCase)))
            {
                index++;
            }

            range = rangeStart..(index + 1);
            return true;
        }
        else
        {
            range = default;
            return false;
        }
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

        bool currentlyInQuotes = false;
        char charNext = default;
        for (int i = 0; i + 1 < line.Length; i++)
        {
            char charCurrent = line[i];
            charNext = line[i + 1];
            if (charCurrent is not ',' || currentlyInQuotes)
            {
                if (charCurrent is '"')
                {
                    if (charNext is '"')
                    {
                        stringBuilder.Append('"');
                        i++;
                        allLinesAreNullOrWhiteSpace = false;
                    }
                    else
                    {
                        currentlyInQuotes = !currentlyInQuotes;
                    }
                }
                else
                {
                    stringBuilder.Append(charCurrent);
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
        if (charNext is ',')
        {
            cells.Add(stringBuilder.ToString());
            stringBuilder.Clear();
        }
        else if (charNext is not '"')
        {
            allLinesAreNullOrWhiteSpace = false;
            stringBuilder.Append(charNext);
        }

        cells.Add(stringBuilder.ToString());

        return cells.ToArray();
    }
    private static string ReadLineAndCount(StreamReader streamReader, ref int lineCount)
    {
        lineCount++;
        return streamReader.ReadLine();
    }
}
