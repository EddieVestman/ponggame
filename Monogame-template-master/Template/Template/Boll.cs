using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
     class Boll : Basklass
    {
        
        private float timer = 0f; // Ökar hastigheten under en tid
        private Vector2? startposition = null;
        private float hastighet;
        private bool spelar;
        public Score Score;
        private int hastighetinc = 10; // Hur ofta hastigheten ökar 

        public Boll(Texture2D texture)
          : base(texture)
        {
            Speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Basklass> sprites)
        {
            if (startposition == null)
            {
                startposition = Position;
                hastighet = Speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) //startar spelet 
                spelar = true;

            if (!spelar)
                return;

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > hastighetinc)
            {
                Speed++;
                timer = 0;
            }

            foreach (var sprite in sprites) //gör så att bollen studsar ifall den träffar bats, tak eller golv
            {
                if (sprite == this)
                    continue;

                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
                if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
            }

            if (Position.Y <= 0 || Position.Y + texture.Height >= Game1.ScreenHeight)
                Velocity.Y = -Velocity.Y;

            if (Position.X <= 0) 
            {
                Score.Score2++;
                Restart();
            }

            if (Position.X + texture.Width >= Game1.ScreenWidht)
            {
                Score.Score1++;
                Restart();
            }

            Position += Velocity * Speed;
        }

        public void Restart()
        {
            var direction = Game1.Random.Next(0, 4); 

            switch (direction)
            {
                case 0:
                    Velocity = new Vector2(1, 1);
                    break;
                case 1:
                    Velocity = new Vector2(1, -1);
                    break;
                case 2:
                    Velocity = new Vector2(-1, -1);
                    break;
                case 3:
                    Velocity = new Vector2(-1, 1);
                    break;
            }

            Position = (Vector2)startposition; //startar om spelat med samma startposition
            Speed = (float)hastighet;
            timer = 0;
            spelar = false;
        }
    }
}

