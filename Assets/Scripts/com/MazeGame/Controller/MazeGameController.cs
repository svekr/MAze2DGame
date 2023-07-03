using System;
using UnityEngine;
using com.MazeGame.Model;
using Utils;

namespace com.MazeGame.Controller {
  public class MazeGameController: IMazeGameController
  {
    public event Action<long> OnTimeElapsedChanged;
    public event Action<long> OnTimeRemainChanged;
    public event Action<int> OnDistancePassedChanged;
    public event Action<bool> OnPausedChanged;
    public event Action OnPlayerPositionChanged;
    public event Action OnLevelFailed;
    public event Action OnLevelWin;

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
      InitPlayerPosition(LevelModel);
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

    public void MovePlayer(Direction direction)
    {
      if (Paused)
      {
        return;
      }
      CellModel cell = GetCurrentCell();
      if ((cell.Directions | cell.Gates).HasFlag(direction))
      {
        Vector2Int newPosition = LevelModel.PlayerPosition + FieldModelUtil.GetMovement(direction);
        _logger?.Log($"{Name}.MovePlayer {direction} from {LevelModel.PlayerPosition} to {newPosition}");
        LevelModel.PlayerPosition = newPosition;
        UpdateDistancePassed();
        OnPlayerPositionChanged?.Invoke();
      }
      if (cell.Gates.HasFlag(direction))
      {
        _logger?.Log($"{Name}.MovePlayer to gates. Level complete");
        Paused = true;
        _endLevelHandler?.Invoke(true);
        OnLevelWin?.Invoke();
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
        _endLevelHandler?.Invoke(false);
        OnLevelFailed?.Invoke();
      }
    }

    private void UpdateDistancePassed()
    {
      DistancePassed++;
      OnDistancePassedChanged?.Invoke(DistancePassed);
    }

    private void InitPlayerPosition(LevelModel level)
    {
      int x = level.FieldModel.GetLength(0) / 2;
      int y = level.FieldModel.GetLength(1) / 2;
      level.PlayerPosition = new UnityEngine.Vector2Int(x, y);
    }

    private CellModel GetCurrentCell()
    {
      return LevelModel.FieldModel[LevelModel.PlayerPosition.x, LevelModel.PlayerPosition.y];
    }
  }
}
