using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Cells
{
  public class Cells : CellsView<Cell>
  {
    override protected void SetCellData(Cell cellObject, Vector2Int index, CellModel[,] fieldModel)
    {
      cellObject.Set(fieldModel[index.x, index.y].Directions);
    }
  }
}