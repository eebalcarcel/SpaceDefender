using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefender.Models
{
    class EnemyTexture
    {
        public Texture2D[] Textures { get; private set; }

        public Texture2D Ship { get; private set; }

        public Texture2D Bullet { get; private set; }

        public EnemyTexture(Texture2D ship, Texture2D bullet)
        {
            Ship = ship;
            Bullet = bullet;
            Textures = new Texture2D[2] { Ship, Bullet };
        }
    }
}
