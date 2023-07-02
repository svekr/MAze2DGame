using UnityEngine;
using com.Managers.Levels.Controller;
using com.Managers.SceneManagement;
using com.Managers.TaskStarter;
using com.MazeGame.Model;
using com.MazeGame.View;
using com.Settings;

namespace com.MazeGame.Controller
{
  public class MazeSceneController: SceneController
  {
    [SerializeField]
    private GameSettings _gameSettings;
    [SerializeField]
    private MazeGameView _mazeGameView;

    private IMazeGameController _mazeGameController;

    override protected void InitLogger()
    {
      if (Main.Logger == null)
      {
        Main.Logger = new UnityLogger();
      }
      Logger = Main.Logger;
    }

    override protected void AwakeHandler()
    {
      InitializeManagers();
    }

    private void InitializeManagers()
    {
      TaskStarter managersStarter = new TaskStarter(Logger);
      managersStarter.AppendTask(new MazeLevelsManagerInitializer(_gameSettings?.Levels));
      managersStarter.Start(InitializeGame);
    }

    private void InitializeGame()
    {
      int levelId = 1;
      LevelModel level = Main.Managers.MazeLevelsManager.GetLevel(levelId);
      if (level == null)
      {
        Logger?.LogError($"Level {levelId} not found");
        return;
      }
      _mazeGameController ??= new MazeGameController();
      _mazeGameController.PlayLevel(level, null);
      _mazeGameView.Initialize(_mazeGameController);
    }

    private void Update()
    {
      _mazeGameController?.OnTimeUpdate(Time.deltaTime);
    }
  }
}