using com.MazeGame.Controller;

namespace com.MazeGame.View
{
  public interface IMazeGameView
  {
    void Initialize(IMazeGameController controller);

    void UpdatePlayerPosition();
  }
}