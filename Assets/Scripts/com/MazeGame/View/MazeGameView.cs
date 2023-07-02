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
      _mazeView.Initialize(controller.LevelModel.FieldModel);
      _uiView.Initialize(logger, controller);
    }

    public void Release()
    {
      _uiView.Release();
    }

    public void UpdatePlayerPosition()
    {

    }

    private void StartListenController()
    {
    }

    private void StopListenController()
    {

    }
  }
}