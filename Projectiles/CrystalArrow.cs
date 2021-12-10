﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Annihilation.Projectiles
{
    class CrystalArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Spike");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 32;
            projectile.damage = 19;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.timeLeft = 600;
        }
        private int type = Main.rand.Next(3);
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                if (type == 0)
                {
                    projectile.damage -= 3;
                    projectile.frame += 2;
                }
                else if (type == 1)
                {
                    projectile.damage += 4;
                    projectile.frame += 1;
                }
                projectile.ai[0] = 1;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(-90f);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item27);
        }
        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 255);
    }
}
