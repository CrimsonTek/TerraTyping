using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TerraTyping.Common.Configs;

public class ClientConfig : ModConfig
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
    public static ClientConfig Instance;

    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    public bool WelcomeMessage { get; set; } = true;

    [DefaultValue(true)]
    public bool ShowTypesOfCritters { get; set; } = true;

    [DefaultValue(true)]
    public bool ShowTypesOfTownNPCs { get; set; } = true;
}
