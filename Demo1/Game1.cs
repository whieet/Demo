using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Xml.Linq;
using System.Threading;
namespace Demo1
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Tiles[,] _tileses; //地图图块

		private bool playerDeath = false;
		
		private Vector2 playPosition = new Vector2(10, 314);
		// run start		
		private Sprite runSprite;

		private Point runCurrentFrame = Point.Zero;

		private Point runFrameSize = new Point(26, 22);

		private Point runSheetSize = new Point(10, 1);
		
		private int runDerection = 0; //默认方向向右

		private bool runAlive = true;
		// run end
		
		// stand start
		private Sprite standSprite;
		
		private Point standCurrentFrame = Point.Zero;

		private Point standFrameSize = new Point(26, 22);

		private Point standSheetSize = new Point(1, 1);
		
		// stand end
		
		// jump start
		private Sprite jumpSprite;
		
		private Point jumpCurrentFrame = Point.Zero;

		private Point jumpFrameSize = new Point(26, 22);

		private Point jumpSheetSize = new Point(9, 1);

		private bool jumpAlive = false;
		// jump end
		
		// bullet start
		private Sprite bulletSprite;
		
		private Point bulletCurrentFrame = Point.Zero;

		private Point bulletFrameSize = new Point(8, 8);

		private Point bulletSheetSize = new Point(7, 1);

		private bool bulletAlive = false;
		// bullet end

		// enemy start

		private Sprite[] enemySprite;
		private Point enemyCurrentFrame = Point.Zero;
		Point enemyFrameSize = new Point(26, 22);
		Point enemySheetSize = new Point(1, 1);
		
		// enemy stop
		
		
		
		// enemybullet start
		private Sprite enemyBullet;
		
		private Point enemyBulletCurrentFrame = Point.Zero;

		private Point enemyBulletFrameSize = new Point(8, 8);

		private Point enemyBulletSheetSize = new Point(7, 1);

		private bool enemyBulletAlive = false;
		// enemybullet stop
		
		private int currentTime = 0;
		private int lastTime = 0;

		private float lastHeight = 0;

		private bool isJump = false;
		
		
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
			
			_tileses = GetTiles();
			base.Initialize();
		}

		public Tiles[,] GetTiles()
		{
			MapXml mapXml = new MapXml("Content/demo.tmx");
			var layers = mapXml.GetMapLayers();
			var tileset = mapXml.GetTileset();
			var data = layers[0].data;
			//foreach (string i in data)
			//{
			//	Console.WriteLine(i);
			//}
			var width = layers[0].width;
			var height = layers[0].height;
			var dataArrays = mapXml.GetDataArrays(data,width,height);
			Tiles[,] tiles = new Tiles[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					var gid = dataArrays[i, j];
					foreach (var VARIABLE in tileset)
					{
						if (gid >= VARIABLE.firstgid && gid < VARIABLE.tilecount + VARIABLE.firstgid)
						{
							
							int key = 0;
							Vector2[] sourcePos = new Vector2[VARIABLE.tilecount];
							//Console.WriteLine(VARIABLE.tilecount);
							for (int k = 0; k < VARIABLE.tilecount / VARIABLE.columns; k++)
							{
								for (int l = 0; l < VARIABLE.columns; l++)
								{
									sourcePos[key] = new Vector2(l * VARIABLE.tilewidth, k * VARIABLE.tileheight);
									key++;
								}
							}
							//Console.WriteLine(VARIABLE.name);
							Texture2D texture2D = Content.Load<Texture2D>(VARIABLE.name);
							
							tiles[i,j] = new Tiles(
								texture2D,
								new Vector2(i * VARIABLE.tilewidth, j * VARIABLE.tileheight),
								new Rectangle(
									(int)sourcePos[gid - VARIABLE.firstgid].X,
									(int)sourcePos[gid - VARIABLE.firstgid].Y,
									VARIABLE.tilewidth,
									VARIABLE.tileheight)
							);
						}
						else
						{
							continue;
						}
					}
				}
			}

			return tiles;
		}


//		public Rectangle[] GetInvisiblePlaces()
//		{
//			MapXml mapXml = new MapXml("Content/demo.tmx");
//			var objects = mapXml.GetObjects();
//			Rectangle[] objectRectangles = new Rectangle[objects.Count];
//			int key = 0;
//			foreach (var VARIABLE in objects)
//			{
//				objectRectangles[key] = new Rectangle(VARIABLE.xpos,VARIABLE.ypos,VARIABLE.width,VARIABLE.height);
//				key++;
//			}
//
//			return objectRectangles;
//		}
		
		
		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			
			// run start
			runSprite = new Sprite(Content.Load<Texture2D>("player/john_run"), playPosition, runCurrentFrame, runFrameSize, runSheetSize);
			// run end
			
			// stand start
			standSprite = new Sprite(Content.Load<Texture2D>(@"player/john_static"),playPosition,standCurrentFrame,standFrameSize,standSheetSize);
			// stand end
			
			// jump start
			jumpSprite = new Sprite(Content.Load<Texture2D>(@"player/john_jump"),playPosition, jumpCurrentFrame, jumpFrameSize, jumpSheetSize);
			// jump end
			
			// bullet start
			bulletSprite = new Sprite(Content.Load<Texture2D>(@"player/weapon_bullet"),playPosition,bulletCurrentFrame,bulletFrameSize,bulletSheetSize);

			// bullet end
			
			// enemy start
			enemySprite = new Sprite[5];
			for (int i = 0; i < 5; i++)
			{
				enemySprite[i] = new Sprite(Content.Load<Texture2D>(@"enemy/grunt_crouch"),playPosition,enemyCurrentFrame,enemyFrameSize,enemySheetSize);
				enemySprite[i].spriteEffects = SpriteEffects.FlipHorizontally;

			}
			
			enemySprite[0].position = new Vector2(20 * 8, 41 * 8 - 11);
			enemySprite[1].position = new Vector2(30 * 8, 41 * 8 - 11);
			enemySprite[2].position = new Vector2(55 * 8, 26 * 8 - 11);
			enemySprite[3].position = new Vector2(89 * 8, 33 * 8 - 11);
			enemySprite[4].position = new Vector2(16 * 8, 13 * 8 - 11);
			enemySprite[4].spriteEffects = SpriteEffects.None;



			// enemy end
			
			
			// enemybullet start
			enemyBullet = new Sprite(Content.Load<Texture2D>(@"enemy/enemy_weapon_bullet"),playPosition,enemyBulletCurrentFrame,enemyBulletFrameSize,enemyBulletSheetSize);
			// enemybullet stop
			
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

			playPosition = runSprite.position;

			KeyboardState keyboardState = Keyboard.GetState();
			KeyboardState previourseKeyboardState = Keyboard.GetState();
			
			if (!playerDeath)
			{
				if (keyboardState.IsKeyDown(Keys.D))
				{
					jumpAlive = false;
					runAlive = true;
					playPosition.X++;
					runSprite.position.X = playPosition.X;
					runSprite.spriteEffects = SpriteEffects.None;
					
				}
				runSprite.Update(gameTime, spriteBatch);
				if (keyboardState.IsKeyDown(Keys.A))
				{
					jumpAlive = false;
					runAlive = true;
					playPosition.X--;
					runSprite.position.X = playPosition.X;
					runSprite.spriteEffects = SpriteEffects.FlipHorizontally;
					
				}
				runSprite.Update(gameTime, spriteBatch);
				if (keyboardState.IsKeyDown(Keys.Space))
				{
					isJump = true;
				}

				if (isJump)
				{
					jumpSprite.spriteEffects = runSprite.spriteEffects;
					jumpSprite.position = playPosition;
					jumpSprite.Update(gameTime,spriteBatch);
					if (lastHeight - jumpSprite.position.Y <= 40)
					{
						jumpSprite.position.Y -=5;
					}
					else
					{
						isJump = false;
					}
					playPosition.Y -= 40;
					jumpAlive = true;
					runAlive = false;
				}
				
				if (keyboardState.IsKeyDown(Keys.J))
				{
					if (!bulletAlive)
					{
						bulletAlive = true;
						bulletSprite.position = playPosition;
						Console.WriteLine(bulletSprite.position);
					}
				}
			}
			

//			bulletSprite.millisecondsPerFrame = 100;

			if (bulletAlive)
			{
				bulletSprite.spriteEffects = runSprite.spriteEffects;
				if (bulletSprite.spriteEffects == SpriteEffects.None)
				{
					bulletSprite.position.X += 5;
					bulletSprite.position.Y = playPosition.Y;
				}
				else
				{
					bulletSprite.position.X -= 5;
					bulletSprite.position.Y = playPosition.Y;

				}
				if (!new Rectangle(0, 0, 800, 480).Contains(bulletSprite.position))
				{
					bulletAlive = false;
				}
				
				bulletSprite.Update(gameTime, spriteBatch);
			}


			foreach (var VARIABLE in enemySprite)
			{
				VARIABLE.Update(gameTime, spriteBatch);
				if (runSprite.Collision(VARIABLE.position, runSprite.position, new Point(0, 0)))
				{
					runAlive = false;
					playerDeath = true;
				}

//				Rectangle[] rectangles = GetInvisiblePlaces();
//				foreach (var rect in rectangles)
//				{
//					if (VARIABLE.Collision(VARIABLE.position,rect))
//					{
//						VARIABLE.position.Y = rect.Y - 22;
//					}
//				}
			}
			
			
			
			Console.WriteLine(gameTime.ElapsedGameTime.Milliseconds);
			
			base.Update(gameTime);
		}
		

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);
			
			//TODO: Add your drawing code here
			spriteBatch.Begin();
			
			foreach (var VARIABLE in _tileses)
			{
				if (VARIABLE != null)
				{
					VARIABLE.Draw(spriteBatch);
				}
				
			}
			if (runAlive)
			{
				runSprite.Draw(gameTime,spriteBatch);
			}
			if (jumpAlive)
			{
				jumpSprite.Draw(gameTime,spriteBatch);

			}

			if (bulletAlive)
			{
				bulletSprite.Draw(gameTime,spriteBatch);
			}

			foreach (var VARIABLE in enemySprite)
			{
				VARIABLE.Draw(gameTime, spriteBatch);
			}
			
			spriteBatch.End();
			base.Draw(gameTime);
		}
		
		
	}
}
