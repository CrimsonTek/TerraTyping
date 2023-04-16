using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraTyping.Commands;

public class _PrintCSVCommand : ModCommand
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }

    public override string Command => "PrintCSV";

    public override string Usage => "/PrintCSV (ModName) ";

    public override CommandType Type => CommandType.Chat;

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        if (args.Length > 0 && ModLoader.TryGetMod(args[0], out Mod mod))
        {

        }
    }

    private static void CanConvert()
    {

    }
}
