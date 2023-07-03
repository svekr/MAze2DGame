using System;
using UnityEngine;
using com.MazeGame.Model;

namespace com.Managers.Input
{
  public class InputManager
  {
    public event Action<Direction> OnInputMovement;

    private readonly ILogger _logger;
    private readonly SwipeController _swipeController;
    private readonly IKeyboardController _keyboardController;

    public SwipeController SwipeController => _swipeController;

    public IKeyboardController KeyboardController => _keyboardController;

    public bool IsEnable { get; set; } = true;

    public InputManager(ILogger logger, SwipeController swipeController, IKeyboardController keyboardController)
    {
      _logger = logger;
      _swipeController = swipeController;
      _swipeController.OnSwipe += InvokeMovement;
      _keyboardController = keyboardController;
      _keyboardController.OnKeyDown += KeyDownHandler;
    }

    public void OnTimeUpdate(float deltaTime)
    {
      if (IsEnable)
      {
        _swipeController.OnTimeUpdate(deltaTime);
        _keyboardController.OnTimeUpdate(deltaTime);
      }
    }

    private void InvokeMovement(Direction direction)
    {
      _logger.Log($"InputManager.OnInputMovement({direction})");
      OnInputMovement?.Invoke(direction);
    }

    private void KeyDownHandler(KeyCode keyCode)
    {
      switch (keyCode)
      {
        case KeyCode.UpArrow:
          InvokeMovement(Direction.Up);
          break;
        case KeyCode.RightArrow:
          InvokeMovement(Direction.Right);
          break;
        case KeyCode.DownArrow:
          InvokeMovement(Direction.Down);
          break;
        case KeyCode.LeftArrow:
          InvokeMovement(Direction.Left);
          break;
      }
    }
  }
}