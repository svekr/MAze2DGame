using System;
using UnityEngine;

namespace com.Managers.Input
{
  public class KeyboardController: IKeyboardController
  {
    public event Action<KeyCode> OnKeyDown;

    private readonly ILogger _logger;
    private readonly KeyCode[] _keyCodes;

    public KeyboardController(ILogger logger, KeyCode[] keyCodes)
    {
      _logger = logger;
      _keyCodes = keyCodes;
    }

    public void OnTimeUpdate(float deltaTime)
    {
      foreach (KeyCode keyCode in _keyCodes)
      {
        if (UnityEngine.Input.GetKeyDown(keyCode))
        {
          KeyDownHandler(keyCode);
        }
      }
    }

    private void KeyDownHandler(KeyCode keyCode)
    {
      _logger?.Log($"Keyboard.KeyDown {keyCode}");
      OnKeyDown?.Invoke(keyCode);
    }
  }
}