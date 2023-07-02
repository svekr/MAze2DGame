using System;
using com.MazeGame.Model;

namespace com.MazeGame.Controller {
  public class MazeGameController: IMazeGameController
  {
    public event Action<long> OnTimeElapsedChanged;
    public event Action<long> OnTimeRemainChanged;
    public event Action<int> OnDistancePassedChanged;
    public event Action<bool> OnPausedChanged;
    public event Action OnLevelFailed;

    private readonly ILogger _logger;
    private readonly string _name;

    private Action<bool> _endLevelHandler;
    private bool _paused = true;
    private float _updateTime = 1f;

    public string Name => _name;

    public LevelModel LevelModel { get; private set; } = null;

    public long TimeElapsed { get; private set; } = 0;

    public long TimeRemain { get; private set; } = 0;

    public int DistancePassed { get; private set; } = 0;

    public bool HasTimeLimit => LevelModel?.Duration > 0;

    public bool Paused
    {
      get => _paused;
      set => SetPaused(value);
    }

    public MazeGameController(ILogger logger)
    {
      _logger = logger;
      _name = this.GetType().Name;
    }

    public void PlayLevel(LevelModel level, Action<bool> onEndLevel)
    {
      _logger?.Log($"{Name}.PlayLevel(levelId = {level.Id})");
      LevelModel = level;
      _endLevelHandler = onEndLevel;
      Reset();
    }

    public void Reset()
    {
      _logger?.Log($"{Name}.Reset()");
      Paused = true;
      TimeElapsed = 0;
      DistancePassed = 0;
      TimeRemain = LevelModel.Duration;
    }

    public void SetPaused(bool value)
    {
      if (value == _paused)
      {
        return;
      }
      _paused = value;
      _logger?.Log($"{Name}.SetPaused({value})");
      OnPausedChanged?.Invoke(_paused);
    }

    public void OnTimeUpdate(float deltaTime)
    {
      if (!Paused) {
        _updateTime -= deltaTime;
        if (_updateTime < 0f) {
          _updateTime = 1f;
          UpdateElapsedTime();
          UpdateRemainTime();
        }
      }
    }

    private void UpdateElapsedTime()
    {
      TimeElapsed++;
      OnTimeElapsedChanged?.Invoke(TimeElapsed);
    }

    private void UpdateRemainTime()
    {
      if (!HasTimeLimit)
      {
        return;
      }
      TimeRemain--;
      OnTimeRemainChanged?.Invoke(TimeRemain);
      if (TimeRemain == 0)
      {
        Paused = true;
        OnLevelFailed?.Invoke();
      }
    }

    private void UpdateDistancePassed()
    {
      DistancePassed++;
      OnDistancePassedChanged?.Invoke(DistancePassed);
    }
  }
}
