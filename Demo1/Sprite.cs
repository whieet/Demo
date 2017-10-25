using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Demo1
{
    
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public Color color;
        public float rotation;
        public Vector2 origin;
        public float scale;
        public SpriteEffects spriteEffects;
        public float layerDepth;
        public Point currentFrame;
        public Point frameSize;
        public Point sheetSize;
        public int timeSinceLastFrame; 
        public int millisecondsPerFrame;
        
        public Sprite(Texture2D texture, Vector2 position, Point currentFrame, Point frameSize, Point sheetSize)
        {
            this.texture = texture;
            this.position = position;
            this.currentFrame = currentFrame;
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            color = Color.White;
            rotation = 0;
            origin = Vector2.Zero;
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0;
            timeSinceLastFrame = 0; 
            millisecondsPerFrame = 80;
        }

        public virtual void Update(GameTime gameTime,SpriteBatch spriteBatch)
        {
			// gameTime.ElapsedGameTime 属性来检测自上一帧之后经过了多少时间。这个属性表示上一次调用Update方法后经过的时间（一帧多少秒）
			// Milliseconds经过的时间毫秒数 
            // timeSinceLastFrame 用来追踪自上一帧之后经过了多少时间
            // millisecondsPverycdFrame 变量用来指定在移动当前的帧索引之前您想要等待多长时间。
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; 
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame-= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                    {
                        currentFrame.Y = 0;
                    }
                }
            }
            
            
        }

        public virtual void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, 
                position, 
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y),
                color, 
                rotation, 
                origin, 
                scale, 
                spriteEffects, 
                layerDepth);
        }

        public virtual bool Collision(Vector2 aPosition, Vector2 bPosition, Point collisionRectOffset)
        {
            Rectangle aRectangle = new Rectangle(
                (int)aPosition.X + collisionRectOffset.X,
                (int)aPosition.Y + collisionRectOffset.Y,
                frameSize.X - collisionRectOffset.X * 2,
                frameSize.Y - collisionRectOffset.Y * 2
                );
            Rectangle bRectangle = new Rectangle(
                (int)bPosition.X + collisionRectOffset.X,
                (int)bPosition.Y + collisionRectOffset.Y,
                frameSize.X - collisionRectOffset.X * 2,
                frameSize.Y - collisionRectOffset.Y * 2
            );

            return aRectangle.Intersects(bRectangle);
        }
        
    }
}