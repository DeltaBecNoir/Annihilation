using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace Annihilation.NPCs.Bosses.Kulvectus
{
    public class Kulvectus : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kulvectus, Fallen Omen");
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 68;
            NPC.damage = 12;
            NPC.defense = 8;
            NPC.lifeMax = 1500;
            NPC.rarity = 4; // Enemies Rarity = 1, Miniboss Rarity = 2, Rare Enemies Rarity = 3, Bosses And NPCs Rarity = 4, Late Game Bosses Rarity = 5.
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("The Hungre!")
            });
        }
    }
}
