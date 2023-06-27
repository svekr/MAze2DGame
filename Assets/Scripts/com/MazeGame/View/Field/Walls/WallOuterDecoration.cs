using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Walls
{
  public class WallOuterDecoration : WallDecoration
  {
    override public void Set(Direction directions)
    {
      gameObject.SetActive((directions & _side) == _side);
    }
  }
}