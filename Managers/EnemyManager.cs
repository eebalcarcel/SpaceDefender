using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceDefender.Sprites;
using SpaceDefender.Models;

namespace SpaceDefender.Managers
{
    public class EnemyManager
    {

        private float _timer;

        private List<EnemyTexture> _textures;

        public bool CanAdd { get; set; }

        public int MaxEnemies { get; set; }

        public float SpawnTimer { get; set; }

        public EnemyManager(ContentManager content)
        {
            _textures = new List<EnemyTexture>() {
               new EnemyTexture(content.Load<Texture2D>("Sprites/Enemy/ships/ship"), content.Load<Texture2D>("Sprites/Enemy/bullets/bullet")),
               new EnemyTexture(content.Load<Texture2D>("Sprites/Enemy/ships/ship2"), content.Load<Texture2D>("Sprites/Enemy/bullets/bullet2")),
               new EnemyTexture(content.Load<Texture2D>("Sprites/Enemy/ships/ship3"), content.Load<Texture2D>("Sprites/Enemy/bullets/bullet3"))
            };

            MaxEnemies = 10;
            SpawnTimer = 2.5f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CanAdd = false;

            if (_timer > SpawnTimer)
            {
                CanAdd = true;

                _timer = 0;
            }
        }

        public Enemy GetEnemy()
        {
            EnemyTexture textures = _textures[Game1.Random.Next(0, _textures.Count)];

            return new Enemy(textures.Ship) {
                Bullet = new Bullet(textures.Bullet),
                Health = 1,
                Position = new Vector2(Game1.Random.Next(0, Game1.ScreenWidth - textures.Ship.Width), -textures.Ship.Height),
                Speed = 1.25f + (float)Game1.Random.NextDouble(),
                ShootingTimer = 1 + (float)Game1.Random.NextDouble(),
            };
        }
    }
}
