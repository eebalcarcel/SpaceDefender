using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpaceDefender.Models;

namespace SpaceDefender.Sprites
{
    public class Enemy : Ship
    {
        private float _timer;

        public float ShootingTimer;

        public Enemy(Texture2D texture)
          : base(texture)
        {
            Position = new Vector2(PositionPercentage.ValuePercentage(Game1.Random.Next(0, Game1.ScreenWidth - _texture.Width), Game1.ScreenWidth), PositionPercentage.ValuePercentage(-_texture.Height/1.5f, Game1.ScreenHeight));
        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= ShootingTimer)
            {
                Shoot(5f);
                _timer = 0;
            }            

            Position = PositionPercentage.VectorsAddition(Position, new Vector2(0, Speed));

            // if the enemy leaves the screen
            if (Position.Y > Game1.ScreenHeight)
                IsRemoved = true;
        }

        public override void OnCollide(Sprite sprite)
        {
            // If we hit a bullet that belongs to a player OR
            // If we crash into a player that is still alive
            if ((sprite is Bullet && ((Bullet)sprite).Parent is Player) ||
               (sprite is Player && !((Player)sprite).IsDead))
            {
                Health--;

                if (Health <= 0)
                    IsRemoved = true;
            }
        }
    }
}
