using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace TerraTyping.Common.Configs;

public class ClientConfig : ModConfig
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
    public static ClientConfig Instance;

    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Label("Welcome Message")]
    [Tooltip("Whether or not a welcome message will be displayed when entering a world.")]
    [DefaultValue(true)]
    public bool WelcomeMessage = true;
}
