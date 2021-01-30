﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Annihilation.NPCs.Ansolar
{
    [AutoloadBossHead]
    class Ansolar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ansolar, Crystium Guardian");
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 58;
            npc.aiStyle = -1;
            npc.damage = 21;
            npc.defense = 14;
            npc.lifeMax = 4200;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit42;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[BuffID.Confused] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Ansolar");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }
        public static bool ThornsReflector = false;
        private bool init = false;
        private int timer1 = 300;
        private int timer2 = 30;
        private int timer3 = 1200;
        private int timer4 = 1200;
        private int timer5 = 2400;
        private int timer6 = 2400;
        private int timer7 = 2400;
        private int timer8 = 2400;
        private int timer9 = 2400;
        private int timer10 = 2400;
        private int timer11 = 2400;
        private int timer12 = 2400;
        public bool Piece1 = false;
        public bool Piece2 = false;
        public bool Piece3 = false;
        public bool Piece4 = false;
        public bool Piece5 = false;
        public bool Piece6 = false;
        public bool Piece7 = false;
        public bool Piece8 = false;
        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = Main.player[npc.target];
                if (!player.active || player.dead || !Main.dayTime)
                {
                    npc.TargetClosest(false);
                    player = Main.player[npc.target];
                    if (!player.active || player.dead || !Main.dayTime)
                    {
                        npc.velocity = new Vector2(0f, 10f);
                        if (npc.timeLeft > 10)
                        {
                            npc.timeLeft = 10;
                        }
                        return;
                    }
                }
                if (init == false)
                {
                    NPC.NewNPC((int)npc.Center.X + 30, (int)npc.Center.Y + 30, ModContent.NPCType<AnsolarClawR>(), 0, 0, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X - 30, (int)npc.Center.Y + 30, ModContent.NPCType<AnsolarClawL>(), 0, 0, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 20, ModContent.NPCType<Shield>(), 0, 1, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X + 20, (int)npc.Center.Y - 20, ModContent.NPCType<Shield>(), 0, 2, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X + 20, (int)npc.Center.Y, ModContent.NPCType<Shield>(), 0, 3, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X + 20, (int)npc.Center.Y + 20, ModContent.NPCType<Shield>(), 0, 4, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 20, ModContent.NPCType<Shield>(), 0, 5, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y + 20, ModContent.NPCType<Shield>(), 0, 6, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<Shield>(), 0, 7, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y - 20, ModContent.NPCType<Shield>(), 0, 8, npc.whoAmI);
                    init = true;
                }
                if (npc.ai[0] == 0)
                {
                    npc.ai[0] = Main.rand.Next(1, 3);
                }
                if (!NPC.AnyNPCs(ModContent.NPCType<AnsolarClawR>()))
                {
                    timer3--;
                    if (timer3 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X + 30, (int)npc.Center.Y + 30, ModContent.NPCType<AnsolarClawR>(), 0, 0, npc.whoAmI);
                        timer3 = 1200;
                    }
                }
                if (!NPC.AnyNPCs(ModContent.NPCType<AnsolarClawL>()))
                {
                    timer4--;
                    if (timer4 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X - 30, (int)npc.Center.Y + 30, ModContent.NPCType<AnsolarClawL>(), 0, 0, npc.whoAmI);
                        timer4 = 1200;
                    }
                }
                if (Piece1)
                {
                    timer5--;
                    if (timer5 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 20, ModContent.NPCType<Shield>(), 0, 1, npc.whoAmI);
                        Piece1 = false;
                        timer5 = 2400;
                    }
                }
                if (Piece2)
                {
                    timer6--;
                    if (timer6 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X + 20, (int)npc.Center.Y - 20, ModContent.NPCType<Shield>(), 0, 2, npc.whoAmI);
                        Piece2 = false;
                        timer6 = 2400;
                    }
                }
                if (Piece3)
                {
                    timer7--;
                    if (timer7 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X + 20, (int)npc.Center.Y, ModContent.NPCType<Shield>(), 0, 3, npc.whoAmI);
                        Piece3 = false;
                        timer7 = 2400;
                    }
                }
                if (Piece4)
                {
                    timer8--;
                    if (timer8 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X + 20, (int)npc.Center.Y + 20, ModContent.NPCType<Shield>(), 0, 4, npc.whoAmI);
                        Piece4 = false;
                        timer8 = 2400;
                    }
                }
                if (Piece5)
                {
                    timer9--;
                    if (timer9 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 20, ModContent.NPCType<Shield>(), 0, 5, npc.whoAmI);
                        Piece5 = false;
                        timer9 = 2400;
                    }
                }
                if (Piece6)
                {
                    timer10--;
                    if (timer10 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y + 20, ModContent.NPCType<Shield>(), 0, 6, npc.whoAmI);
                        Piece6 = false;
                        timer10 = 2400;
                    }
                }
                if (Piece7)
                {
                    timer11--;
                    if (timer11 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<Shield>(), 0, 7, npc.whoAmI);
                        Piece7 = false;
                        timer11 = 2400;
                    }
                }
                if (Piece8)
                {
                    timer12--;
                    if (timer12 <= 0)
                    {
                        NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y - 20, ModContent.NPCType<Shield>(), 0, 8, npc.whoAmI);
                        Piece8 = false;
                        timer12 = 2400;
                    }
                }
                if (npc.ai[0] == 1)
                {
                    npc.velocity.X = 3f;
                    if (npc.Center.Y > player.Center.Y - 210f)
                    {
                        npc.velocity.Y = -8f;
                    }
                    else if (npc.Center.Y < player.Center.Y - 210f)
                    {
                        npc.velocity.Y = 8f;
                    }
                    else
                    {
                        npc.velocity.Y = 0f;
                    }
                    if (npc.Center.X >= player.Center.X + 250f)
                    {
                        npc.velocity.X = -3f;
                        npc.ai[0] = 2;
                    }
                }
                if (npc.ai[0] == 2)
                {
                    npc.velocity.X = -3f;
                    if (npc.Center.Y > player.Center.Y - 210f)
                    {
                        npc.velocity.Y = -8f;
                    }
                    else if (npc.Center.Y < player.Center.Y - 230f)
                    {
                        npc.velocity.Y = 8f;
                    }
                    else
                    {
                        npc.velocity.Y = 0f;
                    }
                    if (npc.Center.X <= player.Center.X - 250f)
                    {
                        npc.velocity.X = 3f;
                        npc.ai[0] = 1;
                    }
                }
                timer1--;
                if (timer1 <= 0)
                {
                    timer2--;
                    int type = Main.rand.Next(0, 4);
                    if (timer2 == 29)
                    {
                        if (type == 1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike1>(), npc.damage / 3, 1);
                        }
                        else if (type == 2)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike2>(), npc.damage / 3, 1);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike3>(), npc.damage / 3, 1);
                        }
                    }
                    if (timer2 == 24)
                    {
                        if (type == 1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike1>(), npc.damage / 3, 1);
                        }
                        else if (type == 2)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike2>(), npc.damage / 3, 1);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike3>(), npc.damage / 3, 1);
                        }
                    }
                    if (timer2 == 19)
                    {
                        if (type == 1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike1>(), npc.damage / 3, 1);
                        }
                        else if (type == 2)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike2>(), npc.damage / 3, 1);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike3>(), npc.damage / 3, 1);
                        }
                    }
                    if (timer2 == 14)
                    {
                        if (type == 1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike1>(), npc.damage / 3, 1);
                        }
                        else if (type == 2)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike2>(), npc.damage / 3, 1);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike3>(), npc.damage / 3, 1);
                        }
                    }
                    if (timer2 == 9)
                    {
                        if (type == 1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike1>(), npc.damage / 3, 1);
                        }
                        else if (type == 2)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike2>(), npc.damage / 3, 1);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike3>(), npc.damage / 3, 1);
                        }
                    }
                    if (timer2 == 4)
                    {
                        if (type == 1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike1>(), npc.damage / 3, 1);
                        }
                        else if (type == 2)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike2>(), npc.damage / 3, 1);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2((player.Center.X + Main.rand.Next(-30, 31)) - npc.Center.X, player.Center.Y - npc.Center.Y) / 30f, ModContent.ProjectileType<Spike3>(), npc.damage / 3, 1);
                        }
                    }
                    if (timer2 <= 0)
                    {
                        timer1 = 300;
                        timer2 = 30;
                    }
                }
            }
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (ThornsReflector && damage >= 1)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} was killed trying to hurt Ansolar... What does he have? Thorns III?"), npc.damage, 0);
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (ThornsReflector && damage >= 1 && projectile.owner == Main.myPlayer)
            {
                projectile.penetrate += 1;
                projectile.friendly = false;
                projectile.hostile = true;
                projectile.damage = npc.damage / 3;
                projectile.velocity.X -= projectile.velocity.X * 2;
                projectile.velocity.Y -= projectile.velocity.Y * 2;
            }
        }
    }
    class AnsolarClawR : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ansolar's Right Claw");
        }
        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 44;
            npc.aiStyle = -1;
            npc.damage = 32;
            npc.defense = 12;
            npc.lifeMax = 2100;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit42;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[BuffID.Confused] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Ansolar");
        }
        private int timer1 = Main.rand.Next(200, 601);
        private int timer2 = 180;
        private bool boolvalue = false;
        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = Main.player[npc.target];
                if (!player.active || player.dead || !Main.dayTime)
                {
                    npc.TargetClosest(false);
                    player = Main.player[npc.target];
                    if (!player.active || player.dead || !Main.dayTime)
                    {
                        npc.velocity = new Vector2(0f, 10f);
                        if (npc.timeLeft > 10)
                        {
                            npc.timeLeft = 10;
                        }
                        return;
                    }
                }
                if (!NPC.AnyNPCs(ModContent.NPCType<Ansolar>()))
                {
                    npc.timeLeft = 1;
                }
                if (npc.ai[0] == 0)
                {
                    NPC npcmain = Main.npc[(int)npc.ai[1]];
                    if (npcmain != null)
                    {
                        npc.position.X = npcmain.Center.X + 50 - (npc.width / 2);
                        npc.position.Y = npcmain.Center.Y + 50 - (npc.height / 2);
                    }
                }
                timer1--;
                if (timer1 <= 0)
                {
                    npc.ai[0] = 1;
                }
                if (npc.ai[0] == 1 && !boolvalue)
                {
                    float velocityx = player.Center.X - npc.Center.X;
                    float velocityy = player.Center.Y - npc.Center.Y;
                    npc.velocity.X = (velocityx + velocityx) / 30f;
                    npc.velocity.Y = (velocityy + velocityy) / 30f;
                    timer2--;
                    if (timer2 <= 0)
                    {
                        timer2 = 180;
                        npc.ai[0] = 2;
                        boolvalue = true;
                    }
                }
                if (npc.ai[0] == 2 || boolvalue)
                {
                    NPC npcmain = Main.npc[(int)npc.ai[1]];
                    if (npcmain != null)
                    {
                        float velocityx = npcmain.Center.X - npc.Center.X;
                        float velocityy = npcmain.Center.Y - npc.Center.Y;
                        npc.velocity.X = ((velocityx + velocityx) + 50) / 30f;
                        npc.velocity.Y = ((velocityy + velocityy) + 50) / 30f;
                        if (npc.position.Y <= npcmain.Center.Y + 50 - (npc.height / 2))
                        {
                            timer1 = Main.rand.Next(200, 601);
                            npc.ai[0] = 0;
                            boolvalue = false;
                        }
                    }
                }
            }
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (Ansolar.ThornsReflector && damage >= 1)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} was killed trying to hurt Ansolar... What does he have? Thorns III?"), npc.damage, 0);
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (Ansolar.ThornsReflector && damage >= 1 && projectile.owner == Main.myPlayer)
            {
                projectile.penetrate += 1;
                projectile.friendly = false;
                projectile.hostile = true;
                projectile.damage = npc.damage / 3;
                projectile.velocity.X -= projectile.velocity.X * 2;
                projectile.velocity.Y -= projectile.velocity.Y * 2;
            }
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
    class AnsolarClawL : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ansolar's Left Claw");
        }
        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 44;
            npc.aiStyle = -1;
            npc.damage = 32;
            npc.defense = 12;
            npc.lifeMax = 2100;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit42;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[BuffID.Confused] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Ansolar");
        }
        private int timer1 = Main.rand.Next(200, 601);
        private int timer2 = 180;
        private bool boolvalue = false;
        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = Main.player[npc.target];
                if (!player.active || player.dead || !Main.dayTime)
                {
                    npc.TargetClosest(false);
                    player = Main.player[npc.target];
                    if (!player.active || player.dead || !Main.dayTime)
                    {
                        npc.velocity = new Vector2(0f, 10f);
                        if (npc.timeLeft > 10)
                        {
                            npc.timeLeft = 10;
                        }
                        return;
                    }
                }
                if (!NPC.AnyNPCs(ModContent.NPCType<Ansolar>()))
                {
                    npc.timeLeft = 1;
                }
                if (npc.ai[0] == 0)
                {
                    NPC npcmain = Main.npc[(int)npc.ai[1]];
                    if (npcmain != null)
                    {
                        npc.position.X = npcmain.Center.X - 50 - (npc.width / 2);
                        npc.position.Y = npcmain.Center.Y + 50 - (npc.height / 2);
                    }
                }
                timer1--;
                if (timer1 <= 0)
                {
                    npc.ai[0] = 1;
                }
                if (npc.ai[0] == 1 && !boolvalue)
                {
                    float velocityx = player.Center.X - npc.Center.X;
                    float velocityy = player.Center.Y - npc.Center.Y;
                    npc.velocity.X = (velocityx + velocityx) / 30f;
                    npc.velocity.Y = (velocityy + velocityy) / 30f;
                    timer2--;
                    if (timer2 <= 0)
                    {
                        timer2 = 180;
                        npc.ai[0] = 2;
                        boolvalue = true;
                    }
                }
                if (npc.ai[0] == 2 || boolvalue)
                {
                    NPC npcmain = Main.npc[(int)npc.ai[1]];
                    if (npcmain != null)
                    {
                        float velocityx = npcmain.Center.X - npc.Center.X;
                        float velocityy = npcmain.Center.Y - npc.Center.Y;
                        npc.velocity.X = ((velocityx + velocityx) - 50) / 30f;
                        npc.velocity.Y = ((velocityy + velocityy) + 50) / 30f;
                        if (npc.position.Y <= npcmain.Center.Y + 50 - (npc.height / 2))
                        {
                            timer1 = Main.rand.Next(200, 601);
                            npc.ai[0] = 0;
                            boolvalue = false;
                        }
                    }
                }
            }
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (Ansolar.ThornsReflector && damage >= 1)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} was killed trying to hurt Ansolar... What does he have? Thorns III?"), npc.damage, 0);
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (Ansolar.ThornsReflector && damage >= 1 && projectile.owner == Main.myPlayer)
            {
                projectile.penetrate += 1;
                projectile.friendly = false;
                projectile.hostile = true;
                projectile.damage = npc.damage / 3;
                projectile.velocity.X -= projectile.velocity.X * 2;
                projectile.velocity.Y -= projectile.velocity.Y * 2;
            }
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
    class Shield : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield");
            Main.npcFrameCount[npc.type] = 9;
        }
        public override void SetDefaults()
        {
            npc.width = 20;
            npc.height = 20;
            npc.aiStyle = -1;
            npc.damage = 25;
            npc.defense = 8;
            npc.lifeMax = 1000;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit42;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[BuffID.Confused] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Ansolar");
        }
        public override bool PreNPCLoot()
        {
            if (npc.ai[0] == 1)
            {
                Ansolar ans = (Ansolar)Main.npc[(int)npc.ai[1]].modNPC;
                if (ans != null)
                {
                    ans.Piece1 = true;
                }
            }
            if (npc.ai[0] == 2)
            {
                Ansolar ans = (Ansolar)Main.npc[(int)npc.ai[1]].modNPC;
                if (ans != null)
                {
                    ans.Piece2 = true;
                }
            }
            if (npc.ai[0] == 3)
            {
                Ansolar ans = (Ansolar)Main.npc[(int)npc.ai[1]].modNPC;
                if (ans != null)
                {
                    ans.Piece3 = true;
                }
            }
            if (npc.ai[0] == 4)
            {
                Ansolar ans = (Ansolar)Main.npc[(int)npc.ai[1]].modNPC;
                if (ans != null)
                {
                    ans.Piece4 = true;
                }
            }
            if (npc.ai[0] == 5)
            {
                Ansolar ans = (Ansolar)Main.npc[(int)npc.ai[1]].modNPC;
                if (ans != null)
                {
                    ans.Piece5 = true;
                }
            }
            if (npc.ai[0] == 6)
            {
                Ansolar ans = (Ansolar)Main.npc[(int)npc.ai[1]].modNPC;
                if (ans != null)
                {
                    ans.Piece6 = true;
                }
            }
            if (npc.ai[0] == 7)
            {
                Ansolar ans = (Ansolar)Main.npc[(int)npc.ai[1]].modNPC;
                if (ans != null)
                {
                    ans.Piece7 = true;
                }
            }
            if (npc.ai[0] == 8)
            {
                Ansolar ans = (Ansolar)Main.npc[(int)npc.ai[1]].modNPC;
                if (ans != null)
                {
                    ans.Piece8 = true;
                }
            }
            return false;
        }
        private int DeltaTime = 0;
        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = Main.player[npc.target];
                if (!player.active || player.dead || !Main.dayTime)
                {
                    npc.TargetClosest(false);
                    player = Main.player[npc.target];
                    if (!player.active || player.dead || !Main.dayTime)
                    {
                        npc.velocity = new Vector2(0f, 10f);
                        if (npc.timeLeft > 10)
                        {
                            npc.timeLeft = 10;
                        }
                        return;
                    }
                }
                if (!NPC.AnyNPCs(ModContent.NPCType<Ansolar>()) || npc.ai[0] == 0)
                {
                    npc.timeLeft = 1;
                }
                if (npc.ai[0] == 5)
                {
                    npc.frame.Y = 20;
                }
                if (npc.ai[0] == 6)
                {
                    npc.frame.Y = 40;
                }
                if (npc.ai[0] == 7)
                {
                    npc.frame.Y = 60;
                }
                if (npc.ai[0] == 8)
                {
                    npc.frame.Y = 80;
                }
                if (npc.ai[0] == 1)
                {
                    npc.frame.Y = 100;
                }
                if (npc.ai[0] == 2)
                {
                    npc.frame.Y = 120;
                }
                if (npc.ai[0] == 3)
                {
                    npc.frame.Y = 140;
                }
                if (npc.ai[0] == 4)
                {
                    npc.frame.Y = 160;
                }
                NPC npcmain = Main.npc[(int)npc.ai[1]];
                if (npcmain != null)
                {
                    DeltaTime++;
                    if (DeltaTime == 18000)
                    {
                        DeltaTime = 0;
                    }
                    if (npc.ai[0] == 1)
                    {
                        npc.Center = npcmain.Center + Vector2.One.RotatedBy((0.05 * DeltaTime) + (MathHelper.Pi * 0.25)) * 30f;
                        npc.rotation = DeltaTime * 0.05f;
                    }
                    if (npc.ai[0] == 2)
                    {
                        npc.Center = npcmain.Center + Vector2.One.RotatedBy((0.05 * DeltaTime) + (MathHelper.Pi * 0.50)) * 30f;
                        npc.rotation = DeltaTime * 0.05f;
                    }
                    if (npc.ai[0] == 3)
                    {
                        npc.Center = npcmain.Center + Vector2.One.RotatedBy((0.05 * DeltaTime) + (MathHelper.Pi * 0.75)) * 30f;
                        npc.rotation = DeltaTime * 0.05f;
                    }
                    if (npc.ai[0] == 4)
                    {
                        npc.Center = npcmain.Center + Vector2.One.RotatedBy((0.05 * DeltaTime) + (MathHelper.Pi * 1.00)) * 30f;
                        npc.rotation = DeltaTime * 0.05f;
                    }
                    if (npc.ai[0] == 5)
                    {
                        npc.Center = npcmain.Center + Vector2.One.RotatedBy((0.05 * DeltaTime) + (MathHelper.Pi * 1.25)) * 30f;
                        npc.rotation = DeltaTime * 0.05f;
                    }
                    if (npc.ai[0] == 6)
                    {
                        npc.Center = npcmain.Center + Vector2.One.RotatedBy((0.05 * DeltaTime) + (MathHelper.Pi * 1.50)) * 30f;
                        npc.rotation = DeltaTime * 0.05f;
                    }
                    if (npc.ai[0] == 7)
                    {
                        npc.Center = npcmain.Center + Vector2.One.RotatedBy((0.05 * DeltaTime) + (MathHelper.Pi * 1.75)) * 30f;
                        npc.rotation = DeltaTime * 0.05f;
                    }
                    if (npc.ai[0] == 8)
                    {
                        npc.Center = npcmain.Center + Vector2.One.RotatedBy((0.05 * DeltaTime) + (MathHelper.Pi * 2.00)) * 30f;
                        npc.rotation = DeltaTime * 0.05f;
                    }
                }
            }
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (Ansolar.ThornsReflector && damage >= 1)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} was killed trying to hurt Ansolar... What does he have? Thorns III?"), npc.damage, 0);
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (Ansolar.ThornsReflector && damage >= 1 && projectile.owner == Main.myPlayer)
            {
                projectile.penetrate += 1;
                projectile.friendly = false;
                projectile.hostile = true;
                projectile.damage = npc.damage / 3;
                projectile.velocity.X -= projectile.velocity.X * 2;
                projectile.velocity.Y -= projectile.velocity.Y * 2;
            }
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}