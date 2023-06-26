using Microsoft.Xna.Framework;
using Terraria;

namespace TerraTyping.DataTypes
{
    public struct CombatTextInfo
    {
        public Rectangle rect;
        public Color color;
        public string text;
        public bool dramatic;
        public bool dot;

        public static CombatTextInfo NoMessage => new CombatTextInfo
        {
            text = string.Empty
        };

        public CombatTextInfo(Rectangle rect, Color color, string text, bool dramatic = false, bool dot = false)
        {
            this.rect = rect;
            this.color = color;
            this.text = text;
            this.dramatic = dramatic;
            this.dot = dot;
        }

        public void NewText()
        {
            if (!string.IsNullOrEmpty(text))
            {
                CombatText.NewText(rect, color, text, dramatic, dot);
            }
        }
    }
}
