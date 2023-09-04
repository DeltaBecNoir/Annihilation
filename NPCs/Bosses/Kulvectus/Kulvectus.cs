using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Annihilation.NPCs.Bosses.Kulvectus
{
    public class Kulvectus : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kulvectus, Fallen Omen");
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.width = 96;
            NPC.height = 136;
            NPC.damage = 12;
            NPC.defense = 8;
            NPC.lifeMax = 1500;
            NPC.noGravity = true;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit8;
            NPC.DeathSound = SoundID.NPCDeath10;
            NPC.rarity = 4; // Enemies Rarity = 1, Miniboss Rarity = 2, Rare Enemies Rarity = 3, Bosses And NPCs Rarity = 4, Late Game Bosses Rarity = 5.
            if (!Main.dedServ) Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Kulvectus");
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("The Hungre!")
            });
        }
        private bool init = true;
        private int init2 = 0;
        private int init3 = 0;
        public override void AI()
        {
            if (NPC.target > 0 || NPC.target != 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            if (!init && Main.dayTime)
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y -= 0.1f;
            }
            if (init)
            {
                if (init2 == 0)
                {
                    NPC.position = new Vector2(player.Center.X - (NPC.width / 2), player.Center.Y - (NPC.height / 2) - 420);
                    init2 = 1;
                }
                else if (player.Center.Y > NPC.Center.Y + 180 && init2 == 1)
                {
                    NPC.velocity.X = 0f;
                    NPC.velocity.Y += 0.1f;
                }
                else
                {
                    init2 = 2;
                    NPC.velocity.X = 0f;
                    NPC.velocity.Y = 0f;
                    if (init3 == 0)
                    {
                        SoundEngine.PlaySound(new("Annihilation/Sounds/Custom/KulvectusRoar") { Volume = 3f }, NPC.Center);
                    }
                    init3++;
                    if (player.Center.X > NPC.Center.X && NPC.Distance(player.Center) <= 200f)
                    {
                        player.velocity.X += NPC.Distance(player.Center) / 300f;
                    }
                    if (player.Center.X < NPC.Center.X && NPC.Distance(player.Center) <= 200f)
                    {
                        player.velocity.X -= NPC.Distance(player.Center) / 300f;
                    }
                    if (player.Center.Y > NPC.Center.Y && NPC.Distance(player.Center) <= 200f)
                    {
                        player.velocity.Y += NPC.Distance(player.Center) / 300f;
                    }
                    if (player.Center.Y < NPC.Center.Y && NPC.Distance(player.Center) <= 200f)
                    {
                        player.velocity.Y -= NPC.Distance(player.Center) / 300f;
                    }
                    if (init3 >= 300)
                    {
                        init = false;
                        init3 = 300;
                    }
                }
            }
        }
    }
}
