using System;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Walls
{
  public class Walls : CellsView<WallsCell>
  {
    override protected void SetCellData(WallsCell cellObject, int indexX, int indexY, Vertex[,] cellsData)
    {
      Vertex cellData = cellsData[indexX, indexY];
      Direction outerDirections = GetCellOuterDirections(cellData, cellsData);
      cellObject.Set(cellData.directions, outerDirections);
    }

    private Direction GetCellOuterDirections(Vertex vertex, Vertex[,] cellsData) {
      Direction result = Direction.None;
      Array directions = Enum.GetValues(typeof(Direction));
      for (int i = 1; i < directions.Length; i++)
      {
        Direction direction = (Direction) directions.GetValue(i);
        if (GetNeighbourCell(vertex, direction, cellsData) == null)
        {
          result |= direction;
        }
      }
      return result;
    }

    private Vertex GetNeighbourCell(Vertex cellData, Direction direction, Vertex[,] cellsData)
    {
      switch (direction)
      {
        case Direction.Up:
          if (cellData.y + 1 < cellsData.GetLength(1))
          {
            return cellsData[cellData.x, cellData.y + 1];
          }
          return null;
        case Direction.Right:
          if (cellData.x + 1 < cellsData.GetLength(0))
          {
            return cellsData[cellData.x + 1, cellData.y];
          }
          return null;
        case Direction.Down:
          if (cellData.y > 0)
          {
            return cellsData[cellData.x, cellData.y - 1];
          }
          return null;
        case Direction.Left:
          if (cellData.x > 0)
          {
            return cellsData[cellData.x - 1, cellData.y];
          }
          return null;
        default:
          return null;
      }
    }
  }
}