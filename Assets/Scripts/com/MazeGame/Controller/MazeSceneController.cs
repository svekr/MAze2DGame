using UnityEngine;
using com.Managers.Input;
using com.Managers.Levels.Controller;
using com.MazeGame.Model;
using com.Managers.SceneManagement;
using com.Managers.TaskStarter;
using com.Managers.UserData.Controller;
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

    private bool _isFirstScene = true;

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
      _isFirstScene = Main.Managers.MazeLevelsManager == null;
      InitializeManagers();
    }

    override protected void DestroyHandler()
    {
      _mazeGameView?.Release();
      Main.Managers.InputManager.OnInputMovement -= _mazeGameController.MovePlayer;
    }

    override protected void SceneReadyHandler()
    {
      if (_mazeGameController != null)
      {
        Main.Managers.InputManager.OnInputMovement -= _mazeGameController.MovePlayer;
        Main.Managers.InputManager.OnInputMovement += _mazeGameController.MovePlayer;
        _mazeGameController.SetPaused(false);
      }
    }

    private void InitializeManagers()
    {
      TaskStarter managersStarter = new TaskStarter(Logger);
      managersStarter.AppendTask(new UserDataManagerInitializer(Logger));
      managersStarter.AppendTask(new MazeLevelsManagerInitializer(_gameSettings?.Levels));
      managersStarter.AppendTask(new InputManagerInitializer());
      managersStarter.Start(InitializeGame);
    }

    private void InitializeGame()
    {
      int levelId = Main.Managers.UserDataManager.Level;
      Main.Managers.UserDataManager.IsLastLevelWin = false;
      LevelModel level = Main.Managers.MazeLevelsManager.GetLevel(levelId);
      if (level == null)
      {
        Logger.LogError($"Level {levelId} not found");
        return;
      }
      _mazeGameController ??= new MazeGameController(Logger);
      _mazeGameController.PlayLevel(level, OnEndLevel);
      _mazeGameView.Initialize(Logger, _mazeGameController);
      if (_isFirstScene)
      {
        SceneReadyHandler();
      }
    }

    private void OnEndLevel(bool isWin)
    {
      if (isWin)
      {
        Main.Managers.UserDataManager.LevelComplete(_mazeGameController.TimeElapsed, _mazeGameController.DistancePassed);
        Main.Managers.UserDataManager.IsLastLevelWin = true;
      }
    }

    private void Update()
    {
      float deltaTime = Time.deltaTime;
      Main.Managers.InputManager.OnTimeUpdate(deltaTime);
      _mazeGameController?.OnTimeUpdate(deltaTime);
    }
  }
}