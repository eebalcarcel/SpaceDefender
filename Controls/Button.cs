using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefender.Controls
{
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        private Texture2D _texture;

        #endregion

        #region Properties

        public EventHandler Click;

        public bool Clicked { get; private set; }
        
        public Color TextColor { get; set; }

        public Rectangle Rectangle { get; set; }

        public string Text;

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font, GraphicsDevice graphicsDevice)
        {
            if (texture == null)
            {
                _texture = new Texture2D(graphicsDevice, 1, 1);
                _texture.SetData<Color>(new Color[] { Color.White });
            }
            else 
                _texture = texture;

            _font = font;

            TextColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;

            if (_isHovering)
                color = Color.DeepSkyBlue;

            spriteBatch.Draw(_texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                float x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                float y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), TextColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None,  0.01f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
