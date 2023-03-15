using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Helpers;

namespace TerraTyping;

public class Table
{
    private const int TableSize = 21;

    private static float[,] effectivenessTable;

    public static float Multiplier { get; private set; }
    public static float Divisor { get; private set; }

    public static void Load()
    {
        effectivenessTable = BuildNewTable();
    }

    public static void Unload()
    {
        effectivenessTable = null;
    }

    public static void NewMultiplierAndDivisorValues(ServerConfig serverConfig)
    {
        Multiplier = serverConfig.Multiplier;
        Divisor = serverConfig.Divisor;
    }

    public static float EffectivenessScaled(Element attack, Element defense)
    {
        int attackInt = (int)attack;
        int defenseInt = (int)defense;
        if (attackInt < TableSize && defenseInt < TableSize)
        {
            return effectivenessTable[attackInt, defenseInt] switch
            {
                2 => Multiplier,
                0.5f => Divisor,
                0 => 0,
                1 => 1,
                _ => throw new NotImplementedException(),
            };
        }
        else
        {
            return 1;
        }
    }

    public static float EffectivenessUnscaled(Element attack, Element defense)
    {
        int attackInt = (int)attack;
        int defenseInt = (int)defense;
        if (attackInt < TableSize && defenseInt < TableSize)
        {
            return effectivenessTable[attackInt, defenseInt];
        }
        else
        {
            return 1;
        }
    }

    private static float[,] BuildNewTable()
    {
        float[,] table = new float[TableSize, TableSize];
        const string fileLocation = $"CsvTypes/Vanilla/table.csv";

        if (!TerraTyping.Instance.FileExists(fileLocation))
        {
            TerraTyping.Instance.Logger.Error($"Table: Unable to find file: \'{fileLocation}\'");
            return BlankTable();
        }

        using Stream stream = TerraTyping.Instance.GetFileStream(fileLocation, false); // newFileStream must be true otherwise it probably crashes, according to TypeLoader.cs
        StreamReader streamReader = new StreamReader(stream);

        int i = 0;
        string line;
        while ((line = streamReader.ReadLine()) is not null)
        {
            if (i >= table.GetLength(0))
            {
                TerraTyping.Instance.Logger.Error($"Table file has too many rows.");
                return BlankTable();
            }

            string[] cells = line.Split(',');
            for (int j = 0; j < cells.Length; j++)
            {
                if (j >= table.GetLength(1))
                {
                    TerraTyping.Instance.Logger.Error($"Table file has too many columns.");
                    return BlankTable();
                }

                string cell = cells[j];
                if (!float.TryParse(cell, out float f))
                {
                    TerraTyping.Instance.Logger.Error($"Could not parse {cell} to float.");
                    return BlankTable();
                }

                if (f == 2 || f == 1 || f == 0.5f || f == 0)
                {
                    table[i, j] = f;
                }
                else
                {
                    TerraTyping.Instance.Logger.Error($"{cell} is not an acceptable value. (Acceptable values: 0, 0.5, 1, 2)");
                    return BlankTable();
                }
            }

            i++;
        }

        return table;
    }

    private static float[,] BlankTable()
    {
        float[,] floats = new float[TableSize, TableSize];

        for (int i = 0; i < TableSize; i++)
        {
            for (int j = 0; j < TableSize; j++)
            {
                floats[i, j] = 1;
            }
        }

        return floats;
    }
}
