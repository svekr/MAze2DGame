using System;

namespace com.MazeGame.Model
{
  [Flags]
  public enum Direction
  {
    None = 0,
    Up = 1,
    Right = 2,
    Down = 4,
    Left = 8
  }
}