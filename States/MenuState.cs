using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.Sprites;
using SpaceDefender.Controls;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceDefender.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, ContentManager content)
          : base(game, content)
        {
        }
        
        public override void LoadContent()
        {
            SpriteFont font = _content.Load<SpriteFont>("Fonts/8bit");
            Song song = _content.Load<Song>("Sounds/Menu");
            //MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            _components = new List<Component>()
                {
                new Button(null, font, _game.GraphicsDevice)
                {
                  Text = "Play",
                  Rectangle = new Rectangle(new Point(_buttonXAlignment, _buttonYAlignment + _buttonsSpacing), _buttonSize),
                  Click = new EventHandler(PlayButtonClicked),
                  Layer = 0.1f
                },
                new Button(null, font, _game.GraphicsDevice)
                {
                  Text = "Highscores",
                  Rectangle = new Rectangle(new Point(_buttonXAlignment, _buttonYAlignment + _buttonsSpacing*2), _buttonSize),
                  Click = new EventHandler(HighscoresButtonClicked),
                  Layer = 0.1f
                },
                new Button(null, font, _game.GraphicsDevice)
                {
                  Text = "Exit",
                  Rectangle = new Rectangle(new Point(_buttonXAlignment, _buttonYAlignment + _buttonsSpacing*3), _buttonSize),
                  Click = new EventHandler(ExitButtonClicked),
                  Layer = 0.1f
                }
              };
        }


        private void PlayButtonClicked(object sender, EventArgs args)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Insert player name. Do not user 'Player' if you don't want your score to be overwritten ", "Player name", "Player", -1, -1);
            if (name.Length > 0)
            {
                Texture2D playerTexture = _content.Load<Texture2D>("Sprites/Player/ship");
                Player player = new Player(playerTexture) {
                    Position = new Vector2(((Game1.ScreenWidth / 2) - (playerTexture.Width / 2)), (Game1.ScreenHeight) - (playerTexture.Height) - 10),
                    Layer = 0.3f,
                    Bullet = new Bullet(_content.Load<Texture2D>("Sprites/Player/bullet")),
                    Input = new Models.Input() {
                        Right = Keys.D,
                        Left = Keys.A,
                        Shoot = Keys.Space,
                    },
                    Health = 10,
                    Score = new Models.Score() {
                        PlayerName = name,
                        Value = 0,
                    },
                };

                MediaPlayer.Stop();

                _game.ChangeState(new GameState(_game, _content, player));
            }
        }

        private void HighscoresButtonClicked(object sender, EventArgs args)
        {
            _game.ChangeState(new HighscoresState(_game, _content));
        }

        private void ExitButtonClicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(_content.Load<Texture2D>("Background/MainMenu"), _game.GraphicsDevice.Viewport.Bounds, Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
