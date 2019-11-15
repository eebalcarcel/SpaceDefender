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

        public bool IsDead
        {
            get {
                return Health <= 0;
            }
        }

        public Input Input { get; set; }

        public Score Score { get; set; }

        public Player(Texture2D texture, GraphicsDevice graphicsDevice)
          : base(texture, graphicsDevice)
        {
            Speed = 3;
            Position = new Vector2((graphicsDevice.Viewport.Width / 2) - (_texture.Width / 2), graphicsDevice.Viewport.Height - _texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsDead)
                return;            

            Vector2 velocity = Vector2.Zero;
            
            if (Game1.CurrentKey.IsKeyDown(Input.Left))
                velocity.X -= Speed;
            else if (Game1.CurrentKey.IsKeyDown(Input.Right))
                velocity.X += Speed;

            if (Game1.CurrentKey.IsKeyDown(Input.Shoot) && Game1.PreviousKey.IsKeyUp(Input.Shoot))
                Shoot(-5f);

            //Player leaving screen constraint
            if ((Position + velocity).X > (graphicsDevice.Viewport.Width - _texture.Width))
                Position = new Vector2(graphicsDevice.Viewport.Width - _texture.Width, Position.Y);
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
