using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Gates
{
  public class GatesCell : CellView
  {
    [SerializeField]
    GameObject _gate;
    public void Set(Direction directions)
    {
      int rotation = 0;
      switch (directions)
      {
        case Direction.Up:
          rotation = 0;
          break;
        case Direction.Right:
          rotation = 90;
          break;
        case Direction.Down:
          rotation = 180;
          break;
        case Direction.Left:
          rotation = 270;
          break;
      }
      _gate.transform.localEulerAngles = Vector3.back * rotation;
    }
  }
}