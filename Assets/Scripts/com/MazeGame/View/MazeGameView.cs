using UnityEngine;
using com.MazeGame.Controller;
using com.MazeGame.View.Field;

namespace com.MazeGame.View
{
  public class MazeGameView : MonoBehaviour, IMazeGameView
  {
    [SerializeField]
    private MazeView _mazeView;

    public void Initialize(IMazeGameController controller)
    {
      _mazeView.Initialize(controller.LevelModel.FieldModel);
    }

    public void UpdatePlayerPosition()
    {

    }
  }
}