using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefender.Models
{
    class EnemyModel
    {
        public Texture2D Ship { get; private set; }

        public Color Color{ get; private set; }

        public EnemyModel(Texture2D ship, Color color)
        {
            Ship = ship;
            Color = color;
        }
    }
}
