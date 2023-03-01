using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Accessories.HeldItems
{
    public class HeldItemsNPC : GlobalNPC
    {
        private static ModItem charcoal;
        private static ModItem magnet;
        private static ModItem twistedSpoon;
        private static ModItem silverPowder;
        private static ModItem ghostTag;
        private static ModItem bloodyHeart;
        private static ModItem poisonBarb;
        private static ModItem sharpBeak;
        private static ModItem dragonFang;
        private static ModItem dustySkull;
        private static ModItem miracleSeed;
        private static ModItem hardStone;

        public ModItem Charcoal { get { if (charcoal is null) charcoal = Mod.Find<ModItem>("Charcoal"); return charcoal; } }
        public ModItem Magnet { get { if (magnet is null) magnet = Mod.Find<ModItem>("Magnet"); return magnet; } }
        public ModItem TwistedSpoon { get { if (twistedSpoon is null) twistedSpoon = Mod.Find<ModItem>("TwistedSpoon"); return twistedSpoon; } }
        public ModItem SilverPowder { get { if (silverPowder is null) silverPowder = Mod.Find<ModItem>("SilverPowder"); return silverPowder; } }
        public ModItem GhostTag { get { if (ghostTag is null) ghostTag = Mod.Find<ModItem>("GhostTag"); return ghostTag; } }
        public ModItem BloodyHeart { get { if (bloodyHeart is null) bloodyHeart = Mod.Find<ModItem>("BloodyHeart"); return bloodyHeart; } }
        public ModItem PoisonBarb { get { if (poisonBarb is null) poisonBarb = Mod.Find<ModItem>("PoisonBarb"); return poisonBarb; } }
        public ModItem SharpBeak { get { if (sharpBeak is null) sharpBeak = Mod.Find<ModItem>("SharpBeak"); return sharpBeak; } }
        public ModItem DragonFang { get { if (dragonFang is null) dragonFang = Mod.Find<ModItem>("DragonFang"); return dragonFang; } }
        public ModItem DustySkull { get { if (dustySkull is null) dustySkull = Mod.Find<ModItem>("DustySkull"); return dustySkull; } }
        public ModItem MiracleSeed { get { if (miracleSeed is null) miracleSeed = Mod.Find<ModItem>("MiracleSeed"); return miracleSeed; } }
        public ModItem HardStone { get { if (hardStone is null) hardStone = Mod.Find<ModItem>("HardStone"); return hardStone; } }

        public override bool InstancePerEntity => true;

        public override void Unload()
        {
            charcoal = null;
            magnet = null;
            twistedSpoon = null;
            silverPowder = null;
            ghostTag = null;
            bloodyHeart = null;
            poisonBarb = null;
            sharpBeak = null;
            dragonFang = null;
            dustySkull = null;
            miracleSeed = null;
            hardStone = null;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            CharcoalDrop(npc, npcLoot);
            MagnetDrop(npc, npcLoot);
        }

        private void CharcoalDrop(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ConditionalDropChanceChangeInExpert(new ItemDropRuleCondition(
                (dropAttemtInfo) => dropAttemtInfo.player.ZoneUnderworldHeight, true, "Drops in the underworld."), Charcoal.Type, 150, 100));
        }

        private void MagnetDrop(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ConditionalDropChanceChangeInExpert(new ItemDropRuleCondition(
                (dropAttemptInfo) => dropAttemptInfo.player.ZoneNormalUnderground, true, "Drops in caves."), Magnet.Type, 175, 125));
        }

        private void TwistedSpoonDrop(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ConditionalDropChanceChangeInExpert(new ItemDropRuleCondition(
                (dropAttemptInfo) => dropAttemptInfo.player.ZoneDungeon, true, "Drops in the dungeon."), TwistedSpoon.Type, 150, 100));
        }

        private void SilverPowderDrop(NPC npc, NPCLoot npcLoot)
        {
            if (npc.catchItem != 0)
            {
                npcLoot.Add(ItemDropRule.Common(twistedSpoon.Type, 25));
            }
        }

        private void GhostTagDrop(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ConditionalDropChanceChangeInExpert(new ItemDropRuleCondition(
                (dropAttemptInfo) => Main.moonPhase == 0 && !Main.dayTime && !dropAttemptInfo.IsInSimulation,
                true, "Drops during the new moon."), GhostTag.Type, 75, 50));
        }

        private void BloodyHeartDrop(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ConditionalDropChanceChangeInExpert(new Conditions.IsBloodMoonAndNotFromStatue(), BloodyHeart.Type, 133, 100));
        }

        private void MiscEnemyDrops(NPC npc, NPCLoot npcLoot)
        {
            switch (npc.type)
            {
                case NPCID.QueenBee:
                    npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), PoisonBarb.Type));
                    break;
                case NPCID.Vulture:
                    npcLoot.Add(ItemDropRule.NormalvsExpert(SharpBeak.Type, 35, 25));
                    break;
                case NPCID.WyvernHead or NPCID.PigronCorruption or NPCID.PigronCrimson or NPCID.PigronHallow:
                    npcLoot.Add(ItemDropRule.Common(DragonFang.Type, 10));
                    break;
                case NPCID.Skeleton:
                case NPCID.SmallSkeleton:
                case NPCID.BigSkeleton:
                case NPCID.HeadacheSkeleton:
                case NPCID.SmallHeadacheSkeleton:
                case NPCID.BigHeadacheSkeleton:
                case NPCID.MisassembledSkeleton:
                case NPCID.SmallMisassembledSkeleton:
                case NPCID.BigMisassembledSkeleton:
                case NPCID.PantlessSkeleton:
                case NPCID.SmallPantlessSkeleton:
                case NPCID.BigPantlessSkeleton:
                case NPCID.SkeletonTopHat:
                case NPCID.SkeletonAstonaut:
                case NPCID.SkeletonAlien:
                    npcLoot.Add(ItemDropRule.NormalvsExpert(DustySkull.Type, 150, 125));
                    break;
            }
        }

        private static LeadingConditionRule ConditionalDropChanceChangeInExpert(IItemDropRuleCondition dropCondition, int type, int normalChance, int expertChange)
        {
            LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.NotExpert());
            leadingConditionRule.OnSuccess(ItemDropRule.ByCondition(dropCondition, type, chanceDenominator: normalChance)); // normal
            leadingConditionRule.OnFailedConditions(ItemDropRule.ByCondition(dropCondition, type, chanceDenominator: expertChange)); // expert
            return leadingConditionRule;
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Dryad)
            {
                shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("MiracleSeed").Type);
                nextSlot++;
            }
        }
    }
}
