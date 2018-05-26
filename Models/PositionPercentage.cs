using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefender.Models
{
    class PositionPercentage
    {

        private static int percent = 100;

        //Returns the screen location of a percentage
        public static Vector2 ScreenLocation(float x, float y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);

            if (x == 0)
                return new Vector2(0, Game1.ScreenHeight / (100 / y));
            else if (y == 0)
                return new Vector2(Game1.ScreenWidth / (100 / x), 0);
            else
                return new Vector2(Game1.ScreenWidth / (100 / x), Game1.ScreenHeight / (100 / y));
        }

        //Returns value location percentage
        public static float ValuePercent(float value, float screen)
        {
            return (value * percent) / screen;
        }

        //Correctly adds two vectors
        public static Vector2 VectorsAddition(Vector2 vector, Vector2 vector2)
        {
            return new Vector2(PositionPercentage.ValuePercent(vector.X + vector2.X, Game1.ScreenWidth), PositionPercentage.ValuePercent(vector.Y + vector2.Y, Game1.ScreenHeight));
        }

    }
}
