using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Annihilation.NPCs.Forest
{
	public class OccultaLurker : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Occulta Lurker");
			Main.npcFrameCount[NPC.type] = 5;
		}

		public override void SetDefaults()
		{
			NPC.width = 42;
			NPC.height = 42;
			NPC.damage = 7;
			NPC.defense = 3;
			NPC.lifeMax = 75;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 3;
			AIType = NPCID.SnowFlinx;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
		{
			//3hi31mg
			var clr = new Color(255, 255, 255, 255); // full white
			var drawPos = NPC.Center - Main.screenPosition;
			var origTexture = TextureAssets.Npc[NPC.type].Value;
			var texture = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;
			var orig = NPC.frame.Size() / 2f;
			SpriteEffects flip = NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Main.spriteBatch.Draw(origTexture, drawPos, NPC.frame, lightColor, NPC.rotation, orig, NPC.scale, flip, 0f);
			Main.spriteBatch.Draw(texture, drawPos, NPC.frame, clr, NPC.rotation, orig, NPC.scale, flip, 0f);
			return false;
		}
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * 0.25f;
		}
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.20f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
		}
		public float AIState
		{
			get => NPC.ai[0];
            set => NPC.ai[0] = value;

        }
		const int Moving = 0, Hiding = 1;
		public override void AI()
		{
			//animation wahhhhhhh
			NPC.spriteDirection = NPC.direction;
			Player player = Main.player[NPC.target];
			if (AIState == Moving)
			{
                NPC.aiStyle = 3;
                AIType = NPCID.SnowFlinx;
                if (player.direction != NPC.direction)
				{
					AIState = Hiding;
				}

            }
			if (AIState == Hiding)
			{
				NPC.aiStyle = 0;
				AIType = 0;
                if (player.direction == NPC.direction)
                {
                    AIState = Moving;
                }

            }
        }
	}
}
