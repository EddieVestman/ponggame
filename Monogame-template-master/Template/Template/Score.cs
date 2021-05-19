using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Score
    {
        public int Score1;
        public int Score2;

        private SpriteFont font;

        public Score(SpriteFont font)
        {
           this.font = font;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Score1.ToString(), new Vector2(320, 70), Color.White);
            spriteBatch.DrawString(font, Score2.ToString(), new Vector2(430, 70), Color.White);
        }
    }
}
