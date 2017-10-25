using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Xml.Linq;

namespace Demo1
{
	public class Tiles
	{
		private Texture2D _texture2D;
		private Vector2 posVector2;
		private Rectangle _rectangle;
		public Tiles(Texture2D _texture2D, Vector2 posVector2, Rectangle _rectangle)
		{
			this._texture2D = _texture2D;
			this.posVector2 = posVector2;
			this._rectangle = _rectangle;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture2D, posVector2, _rectangle, Color.White, 0, Vector2.Zero, 1.0f,SpriteEffects.None,0);
		}
	}
}
