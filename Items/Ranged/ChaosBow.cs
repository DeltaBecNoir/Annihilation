using Annihilation.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Annihilation.Items.Materials;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Annihilation.Items.Ranged
{
	public class ChaosBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Bow");
			// Tooltip.SetDefault("BOGABOGA");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 22;
			Item.height = 52;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 7000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.useAmmo = AmmoID.Arrow;
			Item.shoot = ModContent.ProjectileType<ChaosArrow>();
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Arrow;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.WoodenArrowFriendly) type = ModContent.ProjectileType<ChaosArrow>();
		}
		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(Type)
			   .AddIngredient(ModContent.ItemType<ChaosFragment>(), 10)
			   .AddTile(TileID.Anvils)
			   .Register();
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			var drawPos = Item.Center - Main.screenPosition;
			var origTexture = TextureAssets.Item[Item.type].Value;
			var texture = ModContent.Request<Texture2D>("Annihilation/Items/Ranged/ChaosBow_Glow").Value;
			var orig = texture.Size() / 2f;
			Rectangle frame;
			frame = texture.Frame();

			spriteBatch.Draw(origTexture, drawPos, frame, lightColor, rotation, orig, scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, drawPos, frame, Color.White, rotation, orig, scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
