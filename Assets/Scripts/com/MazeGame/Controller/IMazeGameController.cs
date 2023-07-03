using System;
using com.MazeGame.Model;

namespace com.MazeGame.Controller
{
  public interface IMazeGameController
  {
    event Action<long> OnTimeElapsedChanged;
    event Action<long> OnTimeRemainChanged;
    event Action<int> OnDistancePassedChanged;
    event Action<bool> OnPausedChanged;
    event Action OnPlayerPositionChanged;
    event Action OnLevelFailed;
    event Action OnLevelWin;

    LevelModel LevelModel { get; }

    long TimeElapsed { get; }

    long TimeRemain { get; }

    int DistancePassed { get; }

    bool HasTimeLimit { get; }

    bool Paused { get; set; }

    void PlayLevel(LevelModel level, Action<bool> onEndLevel);

    void Reset();

    void SetPaused(bool value);

    void OnTimeUpdate(float deltaTime);

    void MovePlayer(Direction direction);
  }
}