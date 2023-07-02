using System;
using com.MazeGame.Model;

namespace com.MazeGame.Controller
{
  public interface IMazeGameController
  {
    event Action<long> OnTimeElapsedChanged;
    event Action<long> OnTimeRemainChanged;
    event Action<int> OnDistancePassedChanged;

    LevelModel LevelModel { get; }

    long TimeElapsed { get; }

    long TimeRemain { get; }

    int DistancePassed { get; }

    bool Paused { get; set; }

    void PlayLevel(LevelModel level, Action<bool> onComplete);

    void OnTimeUpdate(float deltaTime);
  }
}