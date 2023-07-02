using System;
using UnityEngine;

namespace com.MazeGame.Model
{
  [Serializable]
  public class CellModel
  {
    private Direction _gates = Direction.None;

    public readonly Vector2Int Index = Vector2Int.zero;

    public Direction Directions { get; set; } = Direction.None;

    public Direction Gates {
      get => _gates;
      set
      {
        if (_gates == Direction.None) {
          _gates = value;
        }
      }
    }

    public CellModel() { }

    public CellModel(int x, int y) : this()
    {
      Index.x = x;
      Index.y = y;
    }

    public CellModel(int x, int y, Direction directions, Direction gates) : this(x, y)
    {
      Directions = directions;
      Gates = gates;
    }
  }
}