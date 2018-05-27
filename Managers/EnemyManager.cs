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
        
        private List<EnemyModel> _animations;

        private Texture2D _bulletTexture;

        private GraphicsDevice _graphicsDevice;

        public bool CanAdd { get; set; }

        public int MaxEnemies { get; set; } = 10;

        public float SpawnTimer { get; set; } = 2.5f;

        public EnemyManager(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _animations = new List<EnemyModel>() {
               new EnemyModel(new Animation(content.Load<Texture2D>("Sprites/Enemy/ships/shipAnimated"), 4), Color.Magenta),
               new EnemyModel(new Animation(content.Load<Texture2D>("Sprites/Enemy/ships/ship2Animated"), 4), Color.Red),
               new EnemyModel(new Animation(content.Load<Texture2D>("Sprites/Enemy/ships/ship3Animated"), 4), Color.Yellow)
            };

            _graphicsDevice = graphicsDevice;
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
            EnemyModel animations = _animations[Game1.Random.Next(0, _animations.Count)];

            Bullet bullet = new Bullet(_bulletTexture, _graphicsDevice);
            bullet.Color = animations.Color;

            return new Enemy(animations.Ship, _graphicsDevice) {
                Bullet = bullet,
                Health = 1,
                Speed = 1.25f + (float)Game1.Random.NextDouble(),
                ShootingTimer = 1 + (float)Game1.Random.NextDouble(),
            };
        }
    }
}
