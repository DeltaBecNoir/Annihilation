﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Annihilation.NPCs.Ansolar
{
    class Spike3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystium Shard");
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 28;
            projectile.damage = 27;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 1)
            {
                projectile.damage = 13;
                projectile.ai[0] = 0;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(-90f);
        }
    }
}
