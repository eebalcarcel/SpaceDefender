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

        private List<EnemyModel> _textures;

        private Texture2D _bulletTexture;

        public bool CanAdd { get; set; }

        public int MaxEnemies { get; set; } = 10;

        public float SpawnTimer { get; set; } = 2.5f;

        public EnemyManager(ContentManager content)
        {
            _textures = new List<EnemyModel>() {
               new EnemyModel(content.Load<Texture2D>("Sprites/Enemy/ships/ship"), Color.Magenta),
               new EnemyModel(content.Load<Texture2D>("Sprites/Enemy/ships/ship2"), Color.Red),
               new EnemyModel(content.Load<Texture2D>("Sprites/Enemy/ships/ship3"), Color.Yellow)
            };

            _bulletTexture = content.Load<Texture2D>("Sprites/bullet");
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
            EnemyModel textures = _textures[Game1.Random.Next(0, _textures.Count)];

            Bullet bullet = new Bullet(_bulletTexture);
            bullet.Color = textures.Color;

            return new Enemy(textures.Ship) {
                Bullet = bullet,
                Health = 1,
                Speed = 1.25f + (float)Game1.Random.NextDouble(),
                ShootingTimer = 1 + (float)Game1.Random.NextDouble(),
            };
        }
    }
}
