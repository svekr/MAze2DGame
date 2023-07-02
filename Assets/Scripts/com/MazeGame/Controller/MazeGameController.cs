using System;
using com.MazeGame.Model;

namespace com.MazeGame.Controller {
  public class MazeGameController: IMazeGameController
  {
    public event Action<long> OnTimeElapsedChanged;
    public event Action<long> OnTimeRemainChanged;
    public event Action<int> OnDistancePassedChanged;

    private Action<bool> _endGameHandler;
    private bool _paused = true;
    private float _updateTime = 1f;

    public LevelModel LevelModel { get; private set; } = null;

    public long TimeElapsed { get; private set; } = 0;

    public long TimeRemain { get; private set; } = 0;

    public int DistancePassed { get; private set; } = 0;

    public bool Paused
    {
      get => _paused;
      set => _paused = value;
    }

    public void PlayLevel(LevelModel level, Action<bool> onComplete)
    {
      Paused = true;
      LevelModel = level;
      _endGameHandler = onComplete;
    }

    public void OnTimeUpdate(float deltaTime)
    {
      if (!Paused) {
        _updateTime -= deltaTime;
        if (_updateTime < 0f) {
          CheckTime();
          _updateTime = 1f;
        }
      }
    }

    private void CheckTime()
    {

    }
  }
}
