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

    public static float Multiplier { get; private set; }
    public static float Divisor { get; private set; }
    public static float[,] EffectivenessTable { get; private set; }

    public static void Load()
    {
        NewTable(ModContent.GetInstance<ServerConfig>());
    }

    public static void Unload()
    {
        EffectivenessTable = null;
    }

    public static void NewTable(ServerConfig serverConfig)
    {
        Multiplier = serverConfig.Multiplier;
        Divisor = serverConfig.Divisor;
        EffectivenessTable = BuildNewTable();
    }

    public static float Effectiveness(Element attack, Element defense)
    {
        if ((int)attack < 20 && (int)defense < 20)
        {
            return EffectivenessTable[(int)attack, (int)defense];
        }
        else
        {
            return 1;
        }
    }

    public static float Effectiveness(int attack, int defense)
    {
        if (attack < 20 && defense < 20)
        {
            return EffectivenessTable[attack, defense];
        }
        else
        {
            return 1;
        }
    }

    private static float[,] BuildNewTable()
    {
        float[,] table = new float[TableSize, TableSize];
        const string fileLocation = $"CsvTypes/Vanilla/tableData.csv";

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

                switch (f)
                {
                    case 2:
                        table[i, j] = Multiplier;
                        break;

                    case 0.5f:
                        table[i, j] = Divisor;
                        break;

                    case 1:
                    case 0:
                        table[i, j] = f;
                        break;

                    default:
                        TerraTyping.Instance.Logger.Error($"{cell} is not an acceptable value. (Acceptable values: 0, 0.5, 1, 2)");
                        return BlankTable();
                }
            }

            i++;
        }

        return table;
    }

    public static float[,] BuildTable()
    {
        return new float[21, 21]
        {
        //         Nor   Fir   Wat   Ele   Gra   Ice   Fig   Poi   Gro   Fly   Psy   Bug   Roc   Gho   Dra   Dar   Ste   Fai   Blo   Bon   Non
        /* Nor */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Divisor,   0f,   1f,   1f, Divisor,   1f,   1f,   1f,  1f},
        /* Fir */ { 1f, Divisor, Divisor,   1f, Multiplier, Multiplier,   1f,   1f,   1f,   1f,   1f, Multiplier, Divisor,   1f, Divisor,   1f, Multiplier,   1f, Multiplier,   1f,  1f},
        /* Wat */ { 1f, Multiplier, Divisor,   1f, Divisor,   1f,   1f,   1f, Multiplier,   1f,   1f,   1f, Multiplier,   1f, Divisor,   1f,   1f,   1f, Multiplier,   1f,  1f},
        /* Ele */ { 1f,   1f, Multiplier, Divisor, Divisor,   1f,   1f,   1f,   0f, Multiplier,   1f,   1f,   1f,   1f, Divisor,   1f,   1f,   1f,   1f,   1f,  1f},
        /* Gra */ { 1f, Divisor, Multiplier,   1f, Divisor,   1f,   1f, Divisor, Multiplier, Divisor,   1f, Divisor, Multiplier,   1f, Divisor,   1f, Divisor,   1f,   1f, Divisor,  1f},
        /* Ice */ { 1f, Divisor, Divisor,   1f, Multiplier, Divisor,   1f,   1f, Multiplier, Multiplier,   1f,   1f,   1f,   1f, Multiplier,   1f, Divisor,   1f,   1f,   1f,  1f},
        /* Fig */ { 2f,   1f,   1f,   1f,   1f, Multiplier,   1f, Divisor,   1f, Divisor, Divisor, Divisor, Multiplier,   0f,   1f, Multiplier, Multiplier, Divisor, Multiplier, Multiplier,  1f},
        /* Poi */ { 1f,   1f,   1f,   1f, Multiplier,   1f,   1f, Divisor, Divisor,   1f,   1f,   1f, Divisor, Divisor,   1f,   1f,   0f, Multiplier, Multiplier,   0f,  1f},
        /* Gro */ { 1f, Multiplier,   1f, Multiplier, Divisor,   1f,   1f, Multiplier,   1f,   0f,   1f, Divisor, Multiplier,   1f,   1f,   1f, Multiplier,   1f,   1f,   1f,  1f},
        /* Fly */ { 1f,   1f,   1f, Divisor, Multiplier,   1f, Multiplier,   1f,   1f,   1f,   1f, Multiplier, Divisor,   1f,   1f,   1f, Divisor,   1f,   1f,   1f,  1f},
        /* Psy */ { 1f,   1f,   1f,   1f,   1f,   1f, Multiplier, Multiplier,   1f,   1f, Divisor,   1f,   1f,   1f,   1f,   0f, Divisor,   1f, Divisor, Divisor,  1f},
        /* Bug */ { 1f, Divisor,   1f,   1f, Multiplier,   1f, Divisor, Divisor,   1f, Divisor, Multiplier,   1f,   1f, Divisor,   1f, Multiplier, Divisor, Divisor,   1f,   1f,  1f},
        /* Roc */ { 1f, Multiplier,   1f,   1f,   1f, Multiplier, Divisor,   1f, Divisor, Multiplier,   1f, Multiplier,   1f,   1f,   1f,   1f, Divisor,   1f,   1f, Multiplier,  1f},
        /* Gho */ { 0f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Multiplier,   1f,   1f, Multiplier,   1f, Divisor,   1f,   1f, Divisor, Divisor,  1f},             
        /* Dra */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Multiplier,   1f, Divisor,   0f, Divisor, Multiplier,  1f},
        /* Dar */ { 1f,   1f,   1f,   1f,   1f,   1f, Divisor,   1f,   1f,   1f, Multiplier,   1f,   1f, Multiplier,   1f, Divisor,   1f, Divisor, Divisor,   1f,  1f},
        /* Ste */ { 1f, Divisor, Divisor, Divisor,   1f, Multiplier,   1f,   1f,   1f,   1f,   1f,   1f, Multiplier,   1f,   1f,   1f, Divisor, Multiplier,   1f, Multiplier,  1f},
        /* Fai */ { 1f, Divisor,   1f,   1f,   1f,   1f, Multiplier, Divisor,   1f,   1f,   1f,   1f,   1f,   1f, Multiplier, Multiplier, Divisor,   1f, Multiplier,   1f,  1f},
        /* Blo */ { 1f,   1f, Divisor,   1f,   1f, Multiplier, Multiplier, Multiplier,   1f,   1f, Multiplier,   1f,   1f, Divisor,   1f,   1f, Divisor, Divisor, Divisor, Multiplier,  1f},
        /* Bon */ { 1f, Divisor,   1f,   1f,   1f, Divisor, Multiplier,   1f, Multiplier,   1f, Multiplier,   1f, Divisor, Divisor,   1f, Multiplier, Divisor,   1f, Multiplier, Divisor,  1f},
        /* Non */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,  1f},
        };
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
