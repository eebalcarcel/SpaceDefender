using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.Sprites;
using SpaceDefender.Controls;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

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
            SpriteFont font = _content.Load<SpriteFont>("Font/8bit");
            Song song = _content.Load<Song>("Sounds/Menu");
            //MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            _components = new List<Component>()
                {
                new Button(null, font, _game.GraphicsDevice, 0)
                {
                  Text = "Play",
                  Click = new EventHandler(PlayButtonClicked)
                },
                new Button(null, font, _game.GraphicsDevice, 1)
                {
                  Text = "Highscores",
                  Click = new EventHandler(HighscoresButtonClicked)
                },
                new Button(null, font, _game.GraphicsDevice, 2)
                {
                  Text = "Exit",
                  Click = new EventHandler(ExitButtonClicked)
                }
              };
        }


        public static Color ParseColor(string str)
        {
            string[] val = str.Split(',');
            byte r = 255, g = 255, b = 255, a = 255;

            if (val.Length >= 1) r = byte.Parse(val[0]);
            if (val.Length >= 2) g = byte.Parse(val[1]);
            if (val.Length >= 3) b = byte.Parse(val[2]);

            return Color.FromNonPremultiplied(r, g, b, a);
        }

        private void PlayButtonClicked(object sender, EventArgs args)
        {
            string playerName = Microsoft.VisualBasic.Interaction.InputBox("Type in the player name", "Player name", "Player", -1, -1);
            string shipColorString = Microsoft.VisualBasic.Interaction.InputBox("Type in the ship rgb color as r,g,b", "Ship color", "255,255,255", -1, -1);
            if (playerName.Length > 0 && shipColorString.Length > 0)
            {
                Texture2D playerTexture = _content.Load<Texture2D>("Sprites/Player/shipbw");
                Color shipColor = ParseColor(shipColorString); ;
                Bullet bullet = new Bullet(_content.Load<Texture2D>("Sprites/bullet"));
                bullet.Color = shipColor;
                Player player = new Player(playerTexture) {
                    Bullet = bullet,
                    Input = new Models.Input() {
                        Right = Keys.D,
                        Left = Keys.A,
                        Shoot = Keys.Space,
                    },
                    Color = shipColor,
                    Health = 10,
                    Score = new Models.Score() {
                        PlayerName = playerName,
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
