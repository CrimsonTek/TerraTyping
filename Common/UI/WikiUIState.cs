using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace TerraTyping.Common.UI;

public class WikiUIState : UIState
{
    UIPanel alwaysExistingPanel;

    public override void OnInitialize()
    {
        alwaysExistingPanel = new UIPanel();
    }

    public override void Click(UIMouseEvent evt)
    {
        base.Click(evt);

        Main.NewText($"{evt.MousePosition}");
    }
}
