using System;
using UnityEngine;
using com.MazeGame.View;
using com.MazeGame.Model;
using com.Settings;

namespace com.MazeGame.Controller {
  public class MazeController : MonoBehaviour
  {
    [SerializeField]
    private LevelSettings _level;
    [SerializeField]
    private RectTransform _field;
    [SerializeField]
    private Cell _cellPrefab;

    private MazeModelCreator _mazemodelCreator;

    void Start()
    {
      CreateField();
      ScaleField();
    }

    private void CreateField()
    {
      if (_mazemodelCreator == null) {
        _mazemodelCreator = new MazeModelCreator();
      }
      Vertex[,] maze = _mazemodelCreator.CreateMaze(_level.FieldWidth, _level.FieldHeight, _level.GatesCount);
      for (int i = 0; i < _level.FieldWidth; i++)
      {
        for (int j = 0; j < _level.FieldHeight; j++)
        {
          CreateCell(i, j, maze[i, j]);
        }
      }
    }

    private void ScaleField()
    {
      Vector2 cellSize = _field.GetChild(0).GetComponent<RectTransform>().rect.size;
      Vector2 parentSize = _field.parent.GetComponent<RectTransform>().rect.size;
      Vector2 fieldSize = new Vector2(cellSize.x * _level.FieldWidth, cellSize.y * _level.FieldHeight);
      float scaleX = parentSize.x / fieldSize.x;
      float scaleY = parentSize.y / fieldSize.y;
      _field.localScale = Vector3.one * Math.Min(scaleX, scaleY);
    }

    private void CreateCell(int x, int y, Vertex vertex)
    {
      if (vertex == null) {
        return;
      }
      Cell cellObject = Instantiate<Cell>(_cellPrefab);
      cellObject.name = $"Cell_{x}:{y}";
      cellObject.transform.SetParent(_field);
      cellObject.transform.localScale = Vector3.one;
      RectTransform rt = cellObject.GetComponent<RectTransform>();
      float posX = rt.rect.width * x - (rt.rect.width * _level.FieldWidth - rt.rect.width) * 0.5f;
      float posY = rt.rect.height * y - (rt.rect.height * _level.FieldHeight - rt.rect.height) * 0.5f;
      rt.anchoredPosition = new Vector2(posX, posY);
      cellObject.Set(vertex.directions);
    }
  }
}
