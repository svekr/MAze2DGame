using com.MazeGame.Model;

namespace Utils
{
  static public class FieldModelUtil
  {
    static public CellModel GetNeighbourCell(CellModel cellModel, Direction direction, CellModel[,] fieldModel)
    {
      switch (direction)
      {
        case Direction.Up:
          if (cellModel.Index.y + 1 < fieldModel.GetLength(1))
          {
            return fieldModel[cellModel.Index.x, cellModel.Index.y + 1];
          }
          return null;
        case Direction.Right:
          if (cellModel.Index.x + 1 < fieldModel.GetLength(0))
          {
            return fieldModel[cellModel.Index.x + 1, cellModel.Index.y];
          }
          return null;
        case Direction.Down:
          if (cellModel.Index.y > 0)
          {
            return fieldModel[cellModel.Index.x, cellModel.Index.y - 1];
          }
          return null;
        case Direction.Left:
          if (cellModel.Index.x > 0)
          {
            return fieldModel[cellModel.Index.x - 1, cellModel.Index.y];
          }
          return null;
        default:
          return null;
      }
    }

    static public Direction InverseDirection(Direction direction)
    {
      switch (direction)
      {
        case Direction.Up:
          return Direction.Down;
        case Direction.Right:
          return Direction.Left;
        case Direction.Down:
          return Direction.Up;
        case Direction.Left:
          return Direction.Right;
        default:
          return direction;
      }
    }
  }
}