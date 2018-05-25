using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceDefender.Sprites
{
    public class Enemy : Ship
    {
        private float _timer;

        public float ShootingTimer;

        public Enemy(Texture2D texture)
          : base(texture)
        { }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= ShootingTimer)
            {
                Shoot(5f);
                _timer = 0;
            }

            Position += new Vector2(0, Speed);

            // if the enemy leaves the screen
            if (Position.Y < -_texture.Height)
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
