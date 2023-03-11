using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace TerraTyping.Common.UI;

public class WikiUIState : UIState
{
    UIPanel topElement;

    public override void OnInitialize()
    {
        topElement = new UIPanel()
        {
            Width = new StyleDimension(0, 0.875f),
            MaxWidth = new StyleDimension(900, 0),
            MinWidth = new StyleDimension(700, 0),
            Top = new StyleDimension(220, 0),
            Height = new StyleDimension(-220, 0.8f),
            HAlign = 0.5f
        };
        Append(topElement);

        UITextPanel<LocalizedText> backButton = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, large: true)
        {
            Width = new StyleDimension(-10, 0.5f),
            Height = new StyleDimension(50, 0),
            VAlign = 1,
            HAlign = 0.5f,
            Top = new StyleDimension(-25, 0),
        };
        backButton.OnMouseOver += BackButton_OnMouseOver;
        backButton.OnMouseOut += BackButton_OnMouseOut;
        backButton.OnMouseDown += BackButton_OnMouseDown;
        backButton.SetSnapPoint("BackButton", 0);
        topElement.Append(backButton);

        UIPanel mainPanel = new UIPanel()
        {
            Width = new StyleDimension(0, 1f),
            Height = new StyleDimension(-90, 1f),
            BackgroundColor = new Color(33, 43, 79) * 0.8f,
        };
        topElement.Append(mainPanel);
        mainPanel.PaddingTop -= 4;
        mainPanel.PaddingBottom -= 4;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (topElement.ContainsPoint(Main.MouseScreen))
        {
            Main.LocalPlayer.mouseInterface = true;
        }
    }

    private void BackButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(SoundID.MenuTick);
        ((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
        ((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
    }

    private void BackButton_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
        ((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
        ((UIPanel)evt.Target).BorderColor = Color.Black;
    }

    private void BackButton_OnMouseDown(UIMouseEvent evt, UIElement listeningElement)
    {
        ModContent.GetInstance<MySystem>().DeactivateWikiUI();
    }
}
