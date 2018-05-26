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

        public Player(Texture2D texture)
          : base(texture)
        {
            Speed = 3;
            Position = new Vector2(50 - PositionPercentage.ValuePercent(_texture.Width / 2, Game1.ScreenWidth), 100 - PositionPercentage.ValuePercent(_texture.Height, Game1.ScreenHeight));
        }

        public override void Update(GameTime gameTime)
        {
            if (IsDead)
                return;

            if (Game1.PreviousScreenWidth != Game1.ScreenWidth || Game1.PreviousScreenHeight != Game1.PreviousScreenHeight)
            {
                Position = new Vector2(PositionPercentage.ValuePercent(Position.X, Game1.PreviousScreenWidth), 100 - PositionPercentage.ValuePercent(_texture.Height, Game1.ScreenHeight));
            }

            Vector2 velocity = Vector2.Zero;

            if (Game1.CurrentKey.IsKeyDown(Input.Left))
                velocity.X -= Speed;
            else if (Game1.CurrentKey.IsKeyDown(Input.Right))
                velocity.X += Speed;

            if (Game1.CurrentKey.IsKeyDown(Input.Shoot) && Game1.PreviousKey.IsKeyUp(Input.Shoot))
                Shoot(-5f);

            //Player leaving screen constraint
            if ((Position + velocity).X > (Game1.ScreenWidth - _texture.Width))
                Position = new Vector2(PositionPercentage.ValuePercent(Game1.ScreenWidth - _texture.Width, Game1.ScreenWidth), PositionPercentage.ValuePercent(Position.Y, Game1.ScreenHeight));
            else if ((Position + velocity).X < 0)
                Position = new Vector2(PositionPercentage.ValuePercent(0, Game1.ScreenWidth), PositionPercentage.ValuePercent(Position.Y, Game1.ScreenHeight));
            else
                Position = PositionPercentage.VectorsAddition(Position, velocity);           

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
