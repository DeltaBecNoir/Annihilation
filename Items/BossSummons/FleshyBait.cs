using Annihilation.NPCs.Bosses.Kulvectus;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Annihilation.Items.BossSummons
{
    public class FleshyBait : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fleshy Bait");
            // Tooltip.SetDefault("Summons the Fallen Omen at night");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noUseGraphic = true;
            Item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<Kulvectus>());
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            // Spawn Kulvectus near the player
            int npcType = ModContent.NPCType<Kulvectus>();
            NPC.NewNPC(null, (int)player.Center.X - (96 / 2), (int)player.Center.Y - (136 / 2) - 540, npcType);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
}