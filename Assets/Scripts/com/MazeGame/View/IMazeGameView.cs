using com.MazeGame.Controller;

namespace com.MazeGame.View
{
  public interface IMazeGameView
  {
    void Initialize(ILogger logger, IMazeGameController controller);

    void Release();

    void UpdatePlayerPosition();
  }
}