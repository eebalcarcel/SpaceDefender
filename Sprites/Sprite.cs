using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.Sprites
{
    public class Sprite : Component
    {
        protected Texture2D _texture;

        public List<Sprite> Children { get; set; }

        public bool IsRemoved { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Hitbox
        {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Sprite Parent;

        public Sprite(Texture2D texture)
        {
            _texture = texture;

            Children = new List<Sprite>();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
