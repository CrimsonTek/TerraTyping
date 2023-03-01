using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace TerraTyping.Common.Configs;

public class ClientConfig
{
    [Label("Welcome Message")]
    [Tooltip("Whether or not a welcome message will be displayed when entering a world.")]
    [DefaultValue(true)]
    public bool WelcomeMessage = true;
}
