using System;
using com.MazeGame.Model;
using UnityEngine;

namespace com.Managers.Input
{
  abstract public class SwipeController
  {
    public event Action<Direction> OnSwipe;

    private readonly ILogger _logger;
    protected readonly float _swipeThreshold = 50f;
    protected readonly float _timeThreshold = 0.9f;

    protected Vector2 _startPosition;
    protected Vector2 _endPosition;
    protected DateTime _startTime;
    protected DateTime _endTime;

    protected DateTime CurrentTime => DateTime.Now;

    public SwipeController(ILogger logger)
    {
      _logger = logger;
    }

    abstract public void OnTimeUpdate(float deltaTime);

    protected void CheckSwipe() {
      float duration = (float) _endTime.Subtract(_startTime).TotalSeconds;
      if (duration > _timeThreshold)
      {
        return;
      }
      float deltaX = _startPosition.x - _endPosition.x;
      float deltaY = _startPosition.y - _endPosition.y;
      float absDeltaX = Mathf.Abs(deltaX);
      float absDeltaY = Mathf.Abs(deltaY);
      if (absDeltaX > _swipeThreshold && absDeltaX > absDeltaY) {
        OnSwipeDetected(deltaX < 0 ? Direction.Left : Direction.Right);
      }
      else if (absDeltaY > _swipeThreshold) {
        OnSwipeDetected(deltaY > 0 ? Direction.Up : Direction.Down);
      }
      _endPosition = _startPosition;
    }

    private void OnSwipeDetected(Direction direction)
    {
      _logger?.Log($"{this.GetType().Name}.OnSwipe({direction})");
      OnSwipe?.Invoke(direction);
    }
  }
}