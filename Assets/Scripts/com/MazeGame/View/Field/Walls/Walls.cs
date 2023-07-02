using System;
using UnityEngine;
using com.MazeGame.Model;
using Utils;

namespace com.MazeGame.View.Field.Walls
{
  public class Walls : CellsView<WallsCell>
  {
    override protected void SetCellData(WallsCell cellObject, Vector2Int index, CellModel[,] fieldModel)
    {
      CellModel cellModel = fieldModel[index.x, index.y];
      Direction outerDirections = GetCellOuterDirections(cellModel, fieldModel);
      cellObject.Set(cellModel.Directions |= cellModel.Gates, outerDirections, cellModel.Gates);
    }

    private Direction GetCellOuterDirections(CellModel cellModel, CellModel[,] fieldModel) {
      Direction result = Direction.None;
      Array directions = Enum.GetValues(typeof(Direction));
      for (int i = 1; i < directions.Length; i++)
      {
        Direction direction = (Direction) directions.GetValue(i);
        if (FieldModelUtil.GetNeighbourCell(cellModel, direction, fieldModel) == null)
        {
          if ((cellModel.Gates & direction) != direction) {
            result |= direction;
          }
        }
      }
      return result;
    }
  }
}