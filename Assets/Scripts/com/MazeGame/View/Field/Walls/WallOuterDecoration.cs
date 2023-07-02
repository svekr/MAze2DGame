using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Walls
{
  public class WallOuterDecoration : WallDecoration
  {
    private void Awake()
    {
      gameObject.SetActive(false);
    }

    override public void Set(Direction directions)
    {
      if (directions.HasFlag(_side))
      {
        gameObject.SetActive(true);
      }
    }
  }
}