using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.Managers;
using SpaceDefender.Models;

namespace SpaceDefender.Sprites
{
    public class Sprite : Component
    {
        protected Texture2D _texture;

        protected AnimationManager _animationManager;

        protected Animation _animation;

        public List<Sprite> Children { get; set; }

        public bool IsRemoved { get; set; }

        protected Vector2 _position;

        public Color Color { get; set; } = Color.White;

        public Vector2 Position
        {
            get { return _position; }
            set {
                _position = PositionPercentage.PositionOnWindow(value.X, value.Y);
                if (_animationManager != null)
                {
                    _animationManager.Position = _position;
                }
            }
        }

        public Rectangle Hitbox
        {
            get {
                if (_animationManager != null)
                    return new Rectangle((int)Position.X, (int)Position.Y, _animation.FrameWidth, _animation.FrameHeight);

                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Sprite Parent;

        public Sprite(Texture2D texture)
        {
            _texture = texture;

            Children = new List<Sprite>();
        }

        public Sprite(Animation animation)
        {
            _animation = animation;
            _animationManager = new AnimationManager(_animation);

            Children = new List<Sprite>();
        }

        public override void Update(GameTime gameTime)
        {
            if (_animationManager != null)
                _animationManager.Play(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else
                spriteBatch.Draw(_texture, Position, Color);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
