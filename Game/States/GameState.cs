using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.Sprites;
using Microsoft.Xna.Framework.Input;
using SpaceDefender.Managers;

namespace SpaceDefender.States
{
    public class GameState : State
    {
        private EnemyManager _enemyManager;

        private SpriteFont _font;

        private Player _player;

        private ScoreManager _scoreManager;

        private List<Sprite> _sprites;

        public int PlayerCount;

        public GameState(Game1 game, ContentManager content, Player player)
          : base(game, content)
        {
            _player = player;
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Font/8bit");

            _scoreManager = ScoreManager.Load();

            _sprites = new List<Sprite>();

            _sprites.Add(_player);

            _enemyManager = new EnemyManager(_content, _game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _content));

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            _enemyManager.Update(gameTime);
            if (_enemyManager.CanAdd && _sprites.Where(e => e is Enemy).Count() < _enemyManager.MaxEnemies)
            {
                _sprites.Add(_enemyManager.GetEnemy());
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            foreach (var leftSprite in _sprites.Where(c => c is ICollidable))
            {
                foreach (var rightSprite in _sprites.Where(c => c is ICollidable))
                {
                    // Don't do anything if they're the same sprite!
                    if (leftSprite == rightSprite)
                        continue;

                    // Don't do anything if they're not colliding
                    if (!leftSprite.Hitbox.Intersects(rightSprite.Hitbox))
                        continue;

                    ((ICollidable)leftSprite).OnCollide(rightSprite);
                }
            }

            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }

            // Add the children sprites to the list of sprites (ie bullets)
            int spriteCount = _sprites.Count;
            for (int i = 0; i < spriteCount; i++)
            {
                var sprite = _sprites[i];
                foreach (var child in sprite.Children)
                {
                    _sprites.Add(child);
                }
                sprite.Children = new List<Sprite>();
            }

            // If the player is dead,save the scores, and return to the highscore state
            if (_player.IsDead)
            {
                _scoreManager.Add(_player.Score);
                ScoreManager.Save(_scoreManager);
                _game.ChangeState(new HighscoresState(_game, _content));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.Draw(_content.Load<Texture2D>("Background/InGame"), _game.GraphicsDevice.Viewport.Bounds, Color.White);

            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            float x = 0;
            float y = 10f;
            spriteBatch.DrawString(_font, "Player " + _player.Score.PlayerName, new Vector2(x, y), Color.White);
            spriteBatch.DrawString(_font, "Health " + _player.Health, new Vector2(x, y + (_font.MeasureString(_player.Health.ToString()).Y)), Color.White);
            spriteBatch.DrawString(_font, "Score " + _player.Score.Value, new Vector2(x, y + (_font.MeasureString(_player.Health.ToString()).Y) + (_font.MeasureString(_player.Score.Value.ToString()).Y)), Color.White);

            spriteBatch.End();
        }
    }
}
