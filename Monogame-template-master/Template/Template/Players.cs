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
    class Players : Basklass
    {
     
           public Players(Texture2D texture)
              : base(texture)
            {
                Speed = 10f; //snabbheten på spelarna
            }

            public override void Update(GameTime gameTime, List<Basklass> sprites) 
            {
                if (Keyboard.GetState().IsKeyDown(Input.Up))
                    Velocity.Y = -Speed;
                else if (Keyboard.GetState().IsKeyDown(Input.Down))
                    Velocity.Y = Speed;

                Position += Velocity;

                Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.ScreenHeight - texture.Height); //så dem inte lämnar skärmen

                Velocity = Vector2.Zero;
            }
        }
    }

