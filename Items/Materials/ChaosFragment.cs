using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
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
            Item.width = 22;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            var drawPos = Item.Center - Main.screenPosition;
            var origTexture = TextureAssets.Item[Item.type].Value;
            var texture = ModContent.Request<Texture2D>("Annihilation/Items/Materials/ChaosFragment_Glow").Value;
            var orig = texture.Size() / 2f;
            Rectangle frame;
            frame = texture.Frame();

            spriteBatch.Draw(origTexture, drawPos, frame, lightColor, rotation, orig, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, drawPos, frame, Color.White, rotation, orig, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}