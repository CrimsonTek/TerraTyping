using System;
using Terraria.GameContent.ItemDropRules;

namespace TerraTyping.Content.Accessories.HeldItems
{
    public struct ItemDropRuleCondition : IItemDropRuleCondition
    {
        public Func<DropAttemptInfo, bool> canDrop;
        public bool canShowItemDropInUI;
        public string getConditionDescription;

        public ItemDropRuleCondition(Func<DropAttemptInfo, bool> canDrop, bool canShowItemDropInUI, string getConditionDescription)
        {
            if (canDrop is null)
            {
                throw new ArgumentNullException(nameof(canDrop));
            }

            this.canDrop = canDrop;
            this.canShowItemDropInUI = canShowItemDropInUI;
            this.getConditionDescription = getConditionDescription ?? string.Empty;
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            return canDrop(info);
        }

        public bool CanShowItemDropInUI()
        {
            return canShowItemDropInUI;
        }

        public string GetConditionDescription()
        {
            return getConditionDescription;
        }
    }
}
