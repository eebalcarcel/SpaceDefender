using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.Sprites
{
    public class Ship : Sprite, ICollidable
    {
        public int Health { get; set; }

        public Bullet Bullet { get; set; }

        public float Speed;

        public Ship(Texture2D texture) : base(texture)
        {
        }

        protected void Shoot(float speed)
        {
            Bullet bullet = Bullet.Clone() as Bullet;
            bullet.Position = new Vector2(this.Position.X + (Hitbox.Width/2) - (bullet.Hitbox.Width/2), this.Position.Y + (Hitbox.Height / 2) - (bullet.Hitbox.Height / 2)); 
            bullet.LifeSpan = 5f;
            bullet.Velocity = new Vector2(0f, speed);
            bullet.Parent = this;

            Children.Add(bullet);
        }

        public virtual void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
