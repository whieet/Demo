using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Demo
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D texture;
		Texture2D texture1;

		TileMap tileMap;

		public Vector2[] SourcePos(int tilecount, int columns)
		{
			int key = 0;
			Vector2[] sourcePos = new Vector2[tilecount];
			for(int x = 0; x<tilecount / columns; x++)
			{
			     for(int y = 0; y < columns; y++)
			     {
			            sourcePos[key] = new Vector2(y* 8, x* 8);
						key++;
			     }
			}
			return sourcePos;
		}

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferHeight = 480;
			graphics.PreferredBackBufferWidth = 800;
			IsMouseVisible = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			texture = Content.Load<Texture2D>(@"jungle_tree1");
			texture1 = Content.Load<Texture2D>(@"tileset");
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			//TODO: Add your drawing code here
			tileMap = new TileMap("Content/demo.tmx");
			int[,] mapArray = tileMap.MapArray();

			spriteBatch.Begin();

			//spriteBatch.Draw(texture, Vector2.Zero, Color.White);
			//spriteBatch.Draw(texture1, new Vector2(100, 100), null, Color.White, 0.0f, Vector2.Zero, 1.0f,SpriteEffects.None, 0);

			for (var i = 0; i < 100; i++)
			{
				for (var j = 0; j < 60; j++)
				{
					//Console.WriteLine(mapArray[i,j]);
					//Console.WriteLine(SourcePos(42,6));
					
					if (mapArray[i,j] < 118 && mapArray[i,j] >=1)
					{ 	
						spriteBatch.Draw(texture1, new Vector2(i * 8, j * 8), new Rectangle((int)SourcePos(117,13)[mapArray[i, j] - 1].X, (int)SourcePos(117,13)[mapArray[i, j]  -1].Y, 8, 8),Color.White, 0.0f, Vector2.Zero, 1.0f,SpriteEffects.None, 0);
					}

					if (mapArray[i,j] >= 118)
					{
						spriteBatch.Draw(texture, new Vector2(i * 8, j * 8), new Rectangle((int)SourcePos(42,6)[mapArray[i, j] - 118].X, (int)SourcePos(42,6)[mapArray[i, j]  -118].Y, 8, 8),Color.White, 0.0f, Vector2.Zero, 1.0f,SpriteEffects.None, 0);
					}
				}
			}

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
