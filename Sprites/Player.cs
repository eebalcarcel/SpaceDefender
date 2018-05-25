using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.Models;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender.Sprites
{
    public class Player : Ship
    {
        private KeyboardState _currentKey;

        private KeyboardState _previousKey;

        public bool IsDead
        {
            get {
                return Health <= 0;
            }
        }

        public Input Input { get; set; }

        public Score Score { get; set; }

        public Player(Texture2D texture)
          : base(texture)
        {
            Speed = 3f;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsDead)
                return;

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            Vector2 velocity = Vector2.Zero;

            if (_currentKey.IsKeyDown(Input.Left))
                velocity.X -= Speed;
            else if (_currentKey.IsKeyDown(Input.Right))
                velocity.X += Speed;

            if (_currentKey.IsKeyDown(Input.Shoot) && _previousKey.IsKeyUp(Input.Shoot))
                Shoot(-5f);

            //Player leaving screen constraint
            if ((Position + velocity).X > (Game1.ScreenWidth - _texture.Width))
                Position = new Vector2(Game1.ScreenWidth - _texture.Width, Position.Y);
            else if ((Position + velocity).X < 0)
                Position = new Vector2(0, Position.Y);
            else
                Position += velocity;

        }

        public override void OnCollide(Sprite sprite)
        {
            if (IsDead)
                return;

            if ((sprite is Bullet && ((Bullet)sprite).Parent is Enemy) ||
               sprite is Enemy)
            {
                Health--;
            }
        }
    }
}
