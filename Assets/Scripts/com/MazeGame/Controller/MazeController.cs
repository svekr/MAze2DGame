using System;
using UnityEngine;
using com.MazeGame.Model;
using com.MazeGame.View.Field.Cells;
using com.MazeGame.View.Field.Walls;
using com.Settings;

namespace com.MazeGame.Controller {
  public class MazeController : MonoBehaviour
  {
    [SerializeField]
    private LevelSettings _level;
    [SerializeField]
    private RectTransform _field;
    [SerializeField]
    private Cells _cells;
    [SerializeField]
    private Walls _walls;

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
      _cells.SetData(maze);
      _walls.SetData(maze);
    }

    private void ScaleField()
    {
      Vector2 parentSize = _field.parent.GetComponent<RectTransform>().rect.size;
      Vector2 fieldSize = _cells.Size;
      float scaleX = parentSize.x / fieldSize.x;
      float scaleY = parentSize.y / fieldSize.y;
      _field.localScale = Vector3.one * Math.Min(scaleX, scaleY);
    }
  }
}
