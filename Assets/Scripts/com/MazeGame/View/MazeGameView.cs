using UnityEngine;
using com.MazeGame.Controller;
using com.MazeGame.View.Field;
using com.MazeGame.View.UI;

namespace com.MazeGame.View
{
  public class MazeGameView : MonoBehaviour, IMazeGameView
  {
    [SerializeField]
    private FieldView _mazeView;
    [SerializeField]
    private MazeUI _uiView;

    private ILogger _logger;
    private IMazeGameController _controller;

    public void Initialize(ILogger logger, IMazeGameController controller)
    {
      _logger = logger;
      _controller = controller;
      _mazeView.Initialize(controller.LevelModel);
      _uiView.Initialize(logger, controller);
      StartListenController();
    }

    public void Release()
    {
      StopListenController();
      _uiView.Release();
    }

    public void UpdatePlayerPosition()
    {
      _mazeView.UpdatePlayerPosition(_controller.LevelModel);
    }

    private void StartListenController()
    {
      _controller.OnPlayerPositionChanged += UpdatePlayerPosition;
    }

    private void StopListenController()
    {
      _controller.OnPlayerPositionChanged -= UpdatePlayerPosition;
    }
  }
}