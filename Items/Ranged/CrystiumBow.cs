using Annihilation.Items.Materials;
using Annihilation.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Annihilation.Items.Ranged
{
	public class CrystiumBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystium Bow");
			Tooltip.SetDefault("Shoots a barage of Crystal Spikes every 5th shot");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 36;
			Item.height = 42;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = ItemUseStyleID.HoldingOut;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Arrow;
		}

		private int shots = 0;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			shots++;
			if (shots == 5)
			{
				int numberProjectiles = 2 + Main.rand.Next(3);
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(45));
					Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CrystalArrow>(), damage, knockBack, player.whoAmI);
				}
				shots = 0;
			}
			return true;
		}
	}
}
