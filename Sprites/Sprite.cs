using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.Models;

namespace SpaceDefender.Sprites
{
    public class Sprite : Component
    {
        protected Texture2D _texture;

        public List<Sprite> Children { get; set; }

        public bool IsRemoved { get; set; }

        private Vector2 _position;

        public Color Color { get; set; } = Color.White;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = PositionPercentage.PositionOnWindow(value.X, value.Y);}
        }

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
            if (Game1.PreviousScreenWidth != Game1.ScreenWidth || Game1.PreviousScreenHeight != Game1.PreviousScreenHeight)
            {
                Position = new Vector2(PositionPercentage.ValuePercentage(Position.X, Game1.PreviousScreenWidth), PositionPercentage.ValuePercentage(Position.Y, Game1.ScreenHeight));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
