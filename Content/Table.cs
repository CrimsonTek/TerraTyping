using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;

namespace TerraTyping;

public class Table : ILoadable
{
    private static Mod mod;

    public static float Mult { get; private set; }
    public static float Divi { get; private set; }
    public static float[,] EffectivenessTable { get; private set; }

    void ILoadable.Load(Mod mod)
    {
        Table.mod = mod;
    }

    void ILoadable.Unload()
    {
        EffectivenessTable = null;
    }

    public static void NewTable(ServerConfig serverConfig)
    {
        Mult = serverConfig.Multiplier;
        Divi = serverConfig.Divisor;
        try
        {
            EffectivenessTable = BuildTable();
        }
        catch (Exception e)
        {
            TerraTyping.Instance.Logger.Error($"Unable to create table.", e);
        }
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
        const int arraySize = 21;
        float[,] table = new float[arraySize, arraySize];
        string fileLocation = $"CsvTypes/Vanilla/table.csv";

        if (!mod.FileExists(fileLocation))
        {
            mod.Logger.Error($"Table: Unable to find file: \'{fileLocation}\'");
            return NewBlankArray(arraySize, arraySize, 1);
        }

        using Stream stream = mod.GetFileStream(fileLocation, true); // newFileStream must be true otherwise it probably crashes, according to TypeLoader.cs
        StreamReader streamReader = new StreamReader(stream);

        int i = 0;
        string line;
        while ((line = streamReader.ReadLine()) is not null)
        {
            if (i >= table.GetLength(0))
            {
                mod.Logger.Error($"Table file has too many rows.");
                return NewBlankArray(arraySize, arraySize, 1);
            }

            string[] cells = line.Split(',');
            for (int j = 0; j < cells.Length; j++)
            {
                if (j >= table.GetLength(1))
                {
                    mod.Logger.Error($"Table file has too many columns.");
                    return NewBlankArray(arraySize, arraySize, 1);
                }

                string cell = cells[j];
                if (!float.TryParse(cell, out float f))
                {
                    mod.Logger.Error($"Could not parse {cell} to float.");
                    return NewBlankArray(arraySize, arraySize, 1);
                }

                switch (f)
                {
                    case 2:
                        table[i, j] = Mult;
                        break;

                    case 0.5f:
                        table[i, j] = Divi;
                        break;

                    case 1:
                    case 0:
                        table[i, j] = f;
                        break;

                    default:
                        mod.Logger.Error($"{cell} is not an acceptable value. (Acceptable values: 0, 0.5, 1, 2)");
                        return NewBlankArray(arraySize, arraySize, 1);
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
        /* Nor */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Divi,   0f,   1f,   1f, Divi,   1f,   1f,   1f,  1f},
        /* Fir */ { 1f, Divi, Divi,   1f, Mult, Mult,   1f,   1f,   1f,   1f,   1f, Mult, Divi,   1f, Divi,   1f, Mult,   1f, Mult,   1f,  1f},
        /* Wat */ { 1f, Mult, Divi,   1f, Divi,   1f,   1f,   1f, Mult,   1f,   1f,   1f, Mult,   1f, Divi,   1f,   1f,   1f, Mult,   1f,  1f},
        /* Ele */ { 1f,   1f, Mult, Divi, Divi,   1f,   1f,   1f,   0f, Mult,   1f,   1f,   1f,   1f, Divi,   1f,   1f,   1f,   1f,   1f,  1f},
        /* Gra */ { 1f, Divi, Mult,   1f, Divi,   1f,   1f, Divi, Mult, Divi,   1f, Divi, Mult,   1f, Divi,   1f, Divi,   1f,   1f, Divi,  1f},
        /* Ice */ { 1f, Divi, Divi,   1f, Mult, Divi,   1f,   1f, Mult, Mult,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   1f,   1f,   1f,  1f},
        /* Fig */ { 2f,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   1f, Divi, Divi, Divi, Mult,   0f,   1f, Mult, Mult, Divi, Mult, Mult,  1f},
        /* Poi */ { 1f,   1f,   1f,   1f, Mult,   1f,   1f, Divi, Divi,   1f,   1f,   1f, Divi, Divi,   1f,   1f,   0f, Mult, Mult,   0f,  1f},
        /* Gro */ { 1f, Mult,   1f, Mult, Divi,   1f,   1f, Mult,   1f,   0f,   1f, Divi, Mult,   1f,   1f,   1f, Mult,   1f,   1f,   1f,  1f},
        /* Fly */ { 1f,   1f,   1f, Divi, Mult,   1f, Mult,   1f,   1f,   1f,   1f, Mult, Divi,   1f,   1f,   1f, Divi,   1f,   1f,   1f,  1f},
        /* Psy */ { 1f,   1f,   1f,   1f,   1f,   1f, Mult, Mult,   1f,   1f, Divi,   1f,   1f,   1f,   1f,   0f, Divi,   1f, Divi, Divi,  1f},
        /* Bug */ { 1f, Divi,   1f,   1f, Mult,   1f, Divi, Divi,   1f, Divi, Mult,   1f,   1f, Divi,   1f, Mult, Divi, Divi,   1f,   1f,  1f},
        /* Roc */ { 1f, Mult,   1f,   1f,   1f, Mult, Divi,   1f, Divi, Mult,   1f, Mult,   1f,   1f,   1f,   1f, Divi,   1f,   1f, Mult,  1f},
        /* Gho */ { 0f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f,   1f, Mult,   1f, Divi,   1f,   1f, Divi, Divi,  1f},             
        /* Dra */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f, Divi,   0f, Divi, Mult,  1f},
        /* Dar */ { 1f,   1f,   1f,   1f,   1f,   1f, Divi,   1f,   1f,   1f, Mult,   1f,   1f, Mult,   1f, Divi,   1f, Divi, Divi,   1f,  1f},
        /* Ste */ { 1f, Divi, Divi, Divi,   1f, Mult,   1f,   1f,   1f,   1f,   1f,   1f, Mult,   1f,   1f,   1f, Divi, Mult,   1f, Mult,  1f},
        /* Fai */ { 1f, Divi,   1f,   1f,   1f,   1f, Mult, Divi,   1f,   1f,   1f,   1f,   1f,   1f, Mult, Mult, Divi,   1f, Mult,   1f,  1f},
        /* Blo */ { 1f,   1f, Divi,   1f,   1f, Mult, Mult, Mult,   1f,   1f, Mult,   1f,   1f, Divi,   1f,   1f, Divi, Divi, Divi, Mult,  1f},
        /* Bon */ { 1f, Divi,   1f,   1f,   1f, Divi, Mult,   1f, Mult,   1f, Mult,   1f, Divi, Divi,   1f, Mult, Divi,   1f, Mult, Divi,  1f},
        /* Non */ { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,  1f},
        };
    }

    private static float[,] NewBlankArray(int length, int height, int defaultValue)
    {
        float[,] floats = new float[length, height];

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                floats[i, j] = defaultValue;
            }
        }

        return floats;
    }
}
