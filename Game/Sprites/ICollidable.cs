﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefender.Sprites
{
  public interface ICollidable
  {
    void OnCollide(Sprite sprite);
  }
}
