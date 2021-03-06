﻿using Terraria.ID;
using Terraria.ModLoader;

namespace Annihilation.Items.Materials
{
    public class ChaosFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Fragment");
            Tooltip.SetDefault("The fragments of a Chaotic Entity");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 999;
            item.value = 100;
            item.rare = ItemRarityID.Green;
        }
    }
}