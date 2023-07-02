using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Gates
{
  public class Gates : CellsView<GatesCell>
  {
    override protected void SetCellData(GatesCell cellObject, Vector2Int index, CellModel[,] fieldModel)
    {
      cellObject.Set(fieldModel[index.x, index.y].Gates);
    }

    override protected bool CanProcessCell(CellModel cellModel)
    {
      return cellModel != null && cellModel.Gates != Direction.None;
    }
  }
}