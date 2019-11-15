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
        public Animation Ship { get; private set; }

        public Color Color{ get; private set; }

        public EnemyModel(Animation ship, Color color)
        {
            Ship = ship;
            Color = color;
        }
    }
}
