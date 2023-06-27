using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Cells
{
  public class Cells : CellsView<Cell>
  {
    override protected void SetCellData(Cell cellObject, int indexX, int indexY, Vertex[,] cellsData)
    {
      cellObject.Set(cellsData[indexX, indexY].directions);
    }
  }
}