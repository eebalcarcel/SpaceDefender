using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDefender.Models;
using SpaceDefender.Sprites;

namespace SpaceDefender.Controls
{
    class HealthBar : Component
    {

        #region Fields

        private Player _player;
        
        private Texture2D _texture;
        
        private Rectangle _rectangle;

        private int _rectangleWidth;

        #endregion

        #region Properties

        public Rectangle Rectangle
        {
            get { return _rectangle; }
            set {
                Vector2 position = new Vector2(value.X, value.Y);
                _rectangle = new Rectangle((int)position.X, (int)position.Y, value.Width, value.Height);
                _rectangleWidth = value.Width;
            }
        }
        
        public int Size { get; set; }

        #endregion


        public HealthBar(Player player, GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            _player = player;
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData<Color>(new Color[] { Color.White });
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.Red);
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle = new Rectangle(Rectangle.X, Rectangle.Y, (_rectangleWidth * _player.Health / 10), Rectangle.Height);
        }
    }
}
