using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDefender.Controls;
using SpaceDefender.Managers;

namespace SpaceDefender.States
{
    public class HighscoresState : State
    {
        private List<Component> _components;

        private SpriteFont _font;

        private ScoreManager _scoreManager;

        public HighscoresState(Game1 game, ContentManager content)
          : base(game, content)
        {
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Fonts/8bit");

            _scoreManager = ScoreManager.Load();

            _components = new List<Component>()
            {
                new Button(null, _font, _game.GraphicsDevice)
                {
                  Text = "Main Menu",
                  Rectangle = new Rectangle(new Point(_buttonXAlignment, _buttonYAlignment + _buttonsSpacing*3), _buttonSize),
                  Click = new EventHandler(MainMenuButtonClicked)
                },
            };
        }

        private void MainMenuButtonClicked(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _content));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                MainMenuButtonClicked(this, new EventArgs());

            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(_content.Load<Texture2D>("Background/Highscores"), _game.GraphicsDevice.Viewport.Bounds, Color.White);


            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.DrawString(_font, "Highscores \n" + string.Join("\n", _scoreManager.HighScores.Select(c => c.PlayerName + " " + c.Value)), new Vector2(400, 100), Color.Red);

            spriteBatch.End();
        }
    }
}
