using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Walls
{
  public class WallInnerDecoration : WallDecoration
  {
    [SerializeField]
    protected GameObject _lineVertical;
    [SerializeField]
    protected GameObject _lineHorizontal;
    [SerializeField]
    protected GameObject _cornerOuter;
    [SerializeField]
    protected GameObject _cornerInner;

    override public void Set(Direction directions)
    {
      Direction dir = directions & _side;

      bool isCornerOuter = dir == _side;
      bool isCornerInner = dir == Direction.None;

      _cornerOuter.SetActive(isCornerOuter);
      _cornerInner.SetActive(isCornerInner);

      if (isCornerOuter || isCornerInner) {
        _lineVertical.SetActive(false);
        _lineHorizontal.SetActive(false);
      } else {
        bool isVertical = ((dir & (Direction.Up | Direction.Down)) != Direction.None);
        _lineVertical.SetActive(isVertical);
        _lineHorizontal.SetActive(!isVertical);
      }
    }
  }
}