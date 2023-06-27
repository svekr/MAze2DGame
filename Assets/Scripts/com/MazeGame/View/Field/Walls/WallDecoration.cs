using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Walls
{
  abstract public class WallDecoration : DestroyableBehaviour
  {
    [SerializeField]
    protected Direction _side;

    abstract public void Set(Direction directions);
  }
}