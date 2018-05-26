using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDefender.States;
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

        private int _buttonSpacingMultiplier;

        private Color _textColor;

        private GraphicsDevice _graphicsDevice;

        #endregion

        #region Properties

        public EventHandler Click;

        public Color TextColor
        {
            get { return _textColor; }
            set {
                if (TextColor == null)
                    _textColor = Color.Black;
                else
                    _textColor = value;
            }
        }
        
        public Rectangle Rectangle { get; private set; }

        public string Text;

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font, GraphicsDevice graphicsDevice, int buttonSpacingMultiplier)
        {
            if (texture == null)
            {
                _texture = new Texture2D(graphicsDevice, 1, 1);
                _texture.SetData<Color>(new Color[] { Color.White });
            }
            else
                _texture = texture;

            if (buttonSpacingMultiplier < 0)
            {
                _buttonSpacingMultiplier = 0;
            }
            else
                _buttonSpacingMultiplier = buttonSpacingMultiplier;

            _graphicsDevice = graphicsDevice;

            _font = font;

            TextColor = Color.Black;

            Rectangle = new Rectangle(new Point((Game1.ScreenWidth / 2) - (State.ButtonSize.X / 2), (Game1.ScreenHeight / 2) - (State.ButtonSize.Y / 2) + State.ButtonsSpacing * _buttonSpacingMultiplier), State.ButtonSize);
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

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), TextColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            Rectangle = new Rectangle(new Point((Game1.ScreenWidth / 2) - (State.ButtonSize.X / 2), (Game1.ScreenHeight / 2) - (State.ButtonSize.Y / 2) + State.ButtonsSpacing * _buttonSpacingMultiplier), State.ButtonSize);

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
