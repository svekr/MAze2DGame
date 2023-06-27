using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field
{
  abstract public class CellsView<T> : MonoBehaviour where T: DestroyableBehaviour
  {
    [SerializeField]
    protected T _cellPrefab;

    protected Vector2 _cellSize = Vector2.zero;
    protected Vector2 _fieldSize = Vector2.zero;
    protected T[,] _cells = null;

    public Vector2 Size => _fieldSize;

    public void SetData(Vertex[,] cellsData) {
      int fieldWidth = cellsData.GetLength(0);
      int fieldHeight = cellsData.GetLength(1);

      _cellSize = _cellPrefab.GetComponent<RectTransform>().rect.size;
      _fieldSize = new Vector2(_cellSize.x * fieldWidth, _cellSize.y * fieldHeight);

      _cells = new T[fieldWidth, fieldHeight];
      T cell;

      for (int i = 0; i < fieldWidth; i++)
      {
        for (int j = 0; j < fieldHeight; j++)
        {
          if (cellsData[i, j] != null) {
            cell = CreateCell(i, j, fieldWidth, fieldHeight);
            SetCellData(cell, i, j, cellsData);
            _cells[i, j] = cell;
          }
        }
      }
    }

    abstract protected void SetCellData(T cellObject, int indexX, int indexY, Vertex[,] cellsData);

    private T CreateCell(int indexX, int indexY, int fieldWidth, int fieldHeight)
    {
      T cellObject = Instantiate<T>(_cellPrefab);
      cellObject.name = $"{_cellPrefab.name} ({indexX}:{indexY})";
      cellObject.transform.SetParent(transform);
      cellObject.transform.localScale = Vector3.one;

      float posX = _cellSize.x * indexX - (_fieldSize.x - _cellSize.x) * 0.5f;
      float posY = _cellSize.y * indexY - (_fieldSize.y - _cellSize.y) * 0.5f;
      cellObject.transform.localPosition = new Vector2(posX, posY);

      return cellObject;
    }

    private void OnDestroy()
    {
      DestroyCells();
    }

    private void DestroyCells()
    {
      if (_cells != null)
      {
        foreach (T cell in _cells)
        {
          cell.Destroy();
        }
        _cells = null;
      }
    }
  }
}