using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field
{
  abstract public class CellsView : MonoBehaviour
  {
    protected Vector2 _fieldSize = Vector2.zero;

    public Vector2 Size => _fieldSize;

    abstract public void SetData(CellModel[,] fieldModel);
  }

  abstract public class CellsView<T> : CellsView where T: CellView
  {
    [SerializeField]
    protected T _cellPrefab;

    protected Vector2 _cellSize = Vector2.zero;
    protected T[,] _cells = null;

    override public void SetData(CellModel[,] fieldModel) {
      int fieldWidth = fieldModel.GetLength(0);
      int fieldHeight = fieldModel.GetLength(1);

      _cellSize = _cellPrefab.Size;
      _fieldSize = new Vector2(_cellSize.x * (fieldWidth + 1), _cellSize.y * (fieldHeight + 1));

      _cells = new T[fieldWidth, fieldHeight];

      CellModel cellModel;
      T cellObject;

      for (int i = 0; i < fieldWidth; i++)
      {
        for (int j = 0; j < fieldHeight; j++)
        {
          cellModel = fieldModel[i, j];
          if (CanProcessCell(cellModel)) {
            cellObject = CreateCell(cellModel.Index, fieldWidth, fieldHeight);
            SetCellData(cellObject, cellModel.Index, fieldModel);
            _cells[i, j] = cellObject;
          }
        }
      }
    }

    abstract protected void SetCellData(T cellObject, Vector2Int index, CellModel[,] cellsData);

    virtual protected bool CanProcessCell(CellModel cellModel)
    {
      return cellModel != null;
    }

    private T CreateCell(Vector2Int index, int fieldWidth, int fieldHeight)
    {
      T cellObject = Instantiate<T>(_cellPrefab);
      cellObject.name = $"{_cellPrefab.name} ({index.x}:{index.y})";
      cellObject.transform.SetParent(transform);
      cellObject.transform.localScale = Vector3.one;

      float posX = _cellSize.x * (index.x + 1) - _fieldSize.x * 0.5f;
      float posY = _cellSize.y * (index.y + 1) - _fieldSize.y * 0.5f;
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
          cell?.Destroy();
        }
        _cells = null;
      }
    }
  }
}